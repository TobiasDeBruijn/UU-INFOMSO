namespace mso; 

public class Move {
    public Player Player;
    public readonly List<StoneMovement> StoneMovements;

    public Move(Player player, List<StoneMovement> stoneMovements) {
        Player = player;
        StoneMovements = stoneMovements;
    }
}