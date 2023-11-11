namespace mso.game; 

public enum PlayerKey {
    Player1,
    Player2,
}

public class Player {
    public PlayerKey Key { get; }
    public List<Stone> OwnedStones = new();
    
    public Player(PlayerKey key) {
        Key = key;
    }
}

internal static class PlayerExt {
    public static PlayerKey GetOther(this PlayerKey playerKey) {
        return playerKey switch {
            PlayerKey.Player1 => PlayerKey.Player2,
            PlayerKey.Player2 => PlayerKey.Player1,
            _ => throw new ArgumentOutOfRangeException(nameof(playerKey), playerKey, null)
        };
    }
}