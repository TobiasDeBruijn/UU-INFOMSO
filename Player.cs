namespace mso; 

public enum Player {
    Player1,
    Player2
}

internal static class PlayerExt {
    public static Player GetOther(this Player player) {
        return player switch {
            Player.Player1 => Player.Player2,
            Player.Player2 => Player.Player1,
            _ => throw new ArgumentOutOfRangeException(nameof(player), player, null)
        };
    }
}