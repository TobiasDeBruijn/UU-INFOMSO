using mso.validators;

namespace mso; 

public class GameManager {
    private readonly IGameRuleValidator _validator;
    private readonly Board _board;
    private Player _playerAtSet;
    
    private GameManager(IGameRuleValidator validator, Board board, Player playerAtSet) {
        _validator = validator;
        _board = board;
        _playerAtSet = playerAtSet;
    }

    public static GameManager CreateForMankela(int numPits) {
        return new GameManager(new MankelaValidator(), Board.WithNumPits(numPits, true), Player.Player1);
    }
    
    public bool ApplyMove(Move move) {
        ValidationResult result = _validator.IsMoveValid(move);
        if (!result.IsValid) {
            return false;
        }

        _board.MoveHistory.Add(move);
        
        // Modify the pits on the board
        IEnumerable<Pit> pits = _board.Pits[Player.Player1].Concat(_board.Pits[Player.Player2]);
        pits.ToList().ForEach(pit => {
            StoneMovement? maybeOrigin = move.StoneMovements.Find(m => m.OriginPit == pit);
            StoneMovement? maybeDest = move.StoneMovements.Find(m => m.DestinationPit == pit);

            if (maybeOrigin != null) {
                // Stone is moved away from pit
                pit.Stones.Remove(maybeOrigin.Stone);
            } else if (maybeDest != null) {
                // Stone is moved to pit
                pit.Stones.Add(maybeDest.Stone);
            }
        });
        
        // Update the player at set
        if (!result.WillPlayerStayAtSet) {
            _playerAtSet = _playerAtSet.GetOther();
        }
            

        return true;
    }
}