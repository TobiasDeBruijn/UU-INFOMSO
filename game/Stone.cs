namespace mso.game; 

public class Stone : IDeepClone<Stone> {
    public PlayerKey Owner;

    public Stone(PlayerKey owner) {
        this.Owner = owner;
    }

    public Stone DeepClone() {
        return new Stone(Owner);
    }
}