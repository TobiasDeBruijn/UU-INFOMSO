using mso.game.validators;

namespace mso.game; 

public class GameManager {
    private readonly IGameRuleValidator _validator;
    public Board Board { get; }
    public PlayerKey PlayerKeyAtSet { get; private set; }
    private readonly List<Player> _players;

    private GameManager(IGameRuleValidator validator, Board board, PlayerKey playerKeyAtSet) {
        _validator = validator;
        Board = board;
        PlayerKeyAtSet = playerKeyAtSet;
        _players = new List<Player> { new(PlayerKey.Player1), new(PlayerKey.Player2) };
    }

    public Player GetPlayerData(PlayerKey key) {
        return _players.First(e => e.Key == key);
    }

    public void SetPlayerData(Player pdata) {
        Player oldPdata = GetPlayerData(pdata.Key);
        _players.Remove(oldPdata);
        
        _players.Add(pdata);
    }

    public static GameManager CreateForMankela(int numPits) {
        return new GameManager(new MankelaValidator(), Board.WithNumPits(numPits, true), PlayerKey.Player1);
    }

    public static GameManager CreateForWari(int numPits) {
        return new GameManager(new WariValidator(), Board.WithNumPits(numPits, false), PlayerKey.Player1);
    }
    
    public bool ApplyMove(Move move) {
        ValidationResult result = _validator.IsMoveValid(this, move);
        if (!result.IsValid) {
            return false;
        }

        Board.MoveHistory.Add(move);
        
        // Modify the pits on the board
        IEnumerable<Pit> pits = Board.Pits[PlayerKey.Player1].Concat(Board.Pits[PlayerKey.Player2]);
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
            PlayerKeyAtSet = PlayerKeyAtSet.GetOther();
        }

        return true;
    }

    Move CreateMove(int startingPit) {
        
    }
}