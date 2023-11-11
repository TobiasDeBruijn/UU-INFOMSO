namespace mso.game; 

public abstract class Move {
    public readonly PlayerKey PlayerKey;
    public readonly List<StoneMovement> StoneMovements;

    protected Move(PlayerKey playerKey, List<StoneMovement> stoneMovements) {
        PlayerKey = playerKey;
        StoneMovements = stoneMovements;
    }
}