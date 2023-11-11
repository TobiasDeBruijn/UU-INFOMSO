namespace mso.game; 

public class Board {
    public readonly Dictionary<PlayerKey, List<Pit>> Pits;
    public readonly List<Move> MoveHistory;

    private Board(Dictionary<PlayerKey, List<Pit>> pits) {
        Pits = pits;
        MoveHistory = new List<Move>();
    }

    public static Board WithNumPits(int playPits, bool hasHomePits) {
        Dictionary<PlayerKey, List<Pit>> pits = new() {
            { PlayerKey.Player1, new List<Pit>() },
            { PlayerKey.Player2, new List<Pit>() },
        };
        
        for (int i = 0; i < playPits / 2; i++) {
            pits[PlayerKey.Player1].Add(Pit.PlayPit(new List<Stone>(), PlayerKey.Player1));   
            pits[PlayerKey.Player2].Add(Pit.PlayPit(new List<Stone>(), PlayerKey.Player2));
        }
        
        if (hasHomePits) {
            pits[PlayerKey.Player1].Add(Pit.HomePit(new List<Stone>(), PlayerKey.Player1));
            pits[PlayerKey.Player2].Add(Pit.HomePit(new List<Stone>(), PlayerKey.Player2));
        }

        return new Board(pits);
    }
}