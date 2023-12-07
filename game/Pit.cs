namespace mso.game; 

public class Pit : IDeepClone<Pit> {
    private static readonly Random Random = new Random();
    
    public List<Stone> Stones;
    private readonly string _identifier;

    private Pit(List<Stone> stones) {
        this.Stones = stones;
        _identifier = RandomString(16);
    }

    private Pit(List<Stone> stones, string identifier) {
        Stones = stones;
        _identifier = identifier;
    }

    public Pit DeepClone() {
        return new Pit(
            Stones.ConvertAll(s => s.DeepClone()),
            _identifier
        );
    }

    public override bool Equals(object? obj) {
        if (obj is Pit pit) {
            return pit._identifier.Equals(this._identifier);
        }

        return false;
    }
    
    private static string RandomString(int length) {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[Random.Next(s.Length)]).ToArray());
    }
    
    public static Pit CreateEmpty() {
        return new Pit(new List<Stone>());
    }

    public bool IsEmpty() {
        return Stones.Count == 0;
    }
}