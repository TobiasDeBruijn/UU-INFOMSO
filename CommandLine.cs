namespace mso;

public enum GameVariant {
    Mankela,
    Wari,
}

public class CommandLine {
    public GameVariant Variant;
    public int NumPits;

    private CommandLine(GameVariant variant, int numPits) {
        Variant = variant;
        NumPits = numPits;
    }
    
    public static CommandLine? Parse(List<string> args) {
        if (args.Count < 2) {
            return null;
        }

        GameVariant v;
        
        switch (args[0].ToLower()) {
            case "mankela": {
                v = GameVariant.Mankela;
                break;
            }
            case "wari": {
                v = GameVariant.Wari;
                break;
            }
            default: return null;
        }

        if (!int.TryParse(args[1], out int numPits)) {
            return null;
        }

        return new CommandLine(v, numPits);
    }
    
}