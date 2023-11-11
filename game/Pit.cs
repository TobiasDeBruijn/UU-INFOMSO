namespace mso.game; 

public class Pit {
    public readonly List<Stone> Stones;
    public PlayerKey? Owner;
    public bool IsHomePit;

    private Pit(List<Stone> stones, PlayerKey? owner, bool isHomePit) {
        this.Stones = stones;
        this.Owner = owner;
        IsHomePit = isHomePit;
    }
    
    public static Pit PlayPit(List<Stone> stones, PlayerKey owner) {
        return new Pit(stones, owner, false);
    }

    public static Pit HomePit(List<Stone> stones, PlayerKey owner) {
        return new Pit(stones, owner, true);
    }
}