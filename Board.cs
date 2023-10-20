namespace mso; 

public class Board {
    public readonly Dictionary<Player, List<Pit>> Pits;
    public readonly List<Move> MoveHistory;

    private Board(Dictionary<Player, List<Pit>> pits) {
        Pits = pits;
        MoveHistory = new List<Move>();
    }

    public static Board WithNumPits(int playPits, bool hasHomePits) {
        Dictionary<Player, List<Pit>> pits = new Dictionary<Player, List<Pit>>();
        for (int i = 0; i < playPits / 2; i++) {
            pits[Player.Player1].Add(Pit.PlayPit(new List<Stone>(), Player.Player1));   
            pits[Player.Player2].Add(Pit.PlayPit(new List<Stone>(), Player.Player2));
        }
        
        if (hasHomePits) {
            pits[Player.Player1].Add(Pit.HomePit(new List<Stone>(), Player.Player1));
            pits[Player.Player2].Add(Pit.HomePit(new List<Stone>(), Player.Player2));
        }

        return new Board(pits);
    }
}