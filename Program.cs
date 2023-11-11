using mso.game;
using mso.ui;

namespace mso; 

public class Program {

    public static void Main(string[] args) {
        CommandLine? cmd = CommandLine.Parse(args.ToList());
        if (cmd == null) {
            Console.Error.WriteLine("Invalid syntax. Syntax: mso <mankela | wari> <number of pits>");
            return;
        }

        GameManager gm = cmd.Variant switch {
            GameVariant.Mankela => GameManager.CreateForMankela(cmd.NumPits),
            GameVariant.Wari => GameManager.CreateForWari(cmd.NumPits),
            _ => throw new Exception("Unreachable")
        };

        UiManager um = new UiManager(gm, cmd.Variant);
        um.Run();
    }
}