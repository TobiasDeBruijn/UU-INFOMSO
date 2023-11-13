namespace mso.game; 

public class Move {
    public readonly PlayerKey PlayerKey;
    public readonly List<StoneMovement> StoneMovements;

    public Move(PlayerKey playerKey, List<StoneMovement> stoneMovements) {
        PlayerKey = playerKey;
        StoneMovements = stoneMovements;
    }
}