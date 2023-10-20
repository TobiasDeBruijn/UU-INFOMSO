namespace mso; 

public class Pit {
    public readonly List<Stone> Stones;
    public Player? Owner;
    public bool IsHomePit;

    private Pit(List<Stone> stones, Player? owner, bool isHomePit) {
        this.Stones = stones;
        this.Owner = owner;
        IsHomePit = isHomePit;
    }
    
    public static Pit PlayPit(List<Stone> stones, Player owner) {
        return new Pit(stones, owner, false);
    }

    public static Pit HomePit(List<Stone> stones, Player owner) {
        return new Pit(stones, owner, true);
    }
}