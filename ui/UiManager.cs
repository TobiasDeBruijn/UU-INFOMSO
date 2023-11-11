using System.Formats.Asn1;
using System.Text;
using mso.game;

namespace mso.ui; 

public class UiManager {
    private readonly GameManager _gameManager;
    private readonly GameVariant _variant;

    public UiManager(GameManager gameManager, GameVariant variant) {
        _gameManager = gameManager;
        _variant = variant;
    }

    public void Run() {
        for (;;) {
            Step();
        }
    }

    private void Step() {
        Console.Clear();
        PrintGameInformation();
        Console.WriteLine();
        PrintBoard();
        Console.WriteLine();

        string? setHoleS = Console.ReadLine();
        if (setHoleS == null || !int.TryParse(setHoleS, out int setHole)) {
            Console.Error.WriteLine("Invalid integer");
            Environment.Exit(1);
        }
        
        
    }

    private void PrintBoard() {
        List<Pit> allPits = _gameManager.Board.Pits.SelectMany(x => x.Value).ToList();
        int pitCount = allPits.Count;
        if (allPits.Any(e => e.IsHomePit)) {
            pitCount -= 2;
        }

        int sideLength = pitCount / 2;

        IEnumerable<string> line1 = allPits
            .Where(e => !e.IsHomePit)
            .Take(..sideLength)
            .Select(StringifyPit);
        
        IEnumerable<string> line2 = allPits
            .Where(e => !e.IsHomePit)
            .Take(sideLength..pitCount)
            .Select(StringifyPit);

        string gl1 = string.Join(Environment.NewLine, CombineLines(line1));
        string gl2 = string.Join(Environment.NewLine, CombineLines(line2));
        
        Console.Write(gl1);
        Console.WriteLine();
        Console.Write(gl2);
    }

    private IEnumerable<string> CombineLines(IEnumerable<string> input) {
        List<StringBuilder> lines = new();
        foreach (string s in input) {
            List<string> sLines = s.ReplaceLineEndings().Split(Environment.NewLine).ToList();
            for (int i = 0; i < sLines.Count; i++) {
                string l = sLines[i];
                
                StringBuilder o = lines.Count > i ? lines[i] : new StringBuilder();
                o.Append(l);
                
                if (lines.Count > i) {
                    lines[i] = o;
                } else {
                    lines.Add(o);
                }
            }
        }

        return lines.Select(e => e.ToString());
    }

    private string StringifyPit(Pit p) {
        p.Stones.Add(new Stone(PlayerKey.Player1));
        
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"+{string.Join("", p.Stones.Select(_ => "-"))}+ ");
        if (p.IsHomePit) {
            sb.AppendLine($"|{string.Join("", p.Stones.Select(_ => " "))}| ");
        }
        sb.AppendLine($"|{string.Join("", p.Stones.Select(_ => "*"))}| ");
        if (p.IsHomePit) {
            sb.AppendLine($"|{string.Join("", p.Stones.Select(_ => " "))}| ");
        }
        sb.AppendLine($"+{string.Join("", p.Stones.Select(_ => "-"))}+ ");

        return sb.ToString();
    }

    private void PrintGameInformation() {
        Console.WriteLine($"Player at set: {_gameManager.PlayerKeyAtSet}");
        Console.WriteLine($"Game: {_variant}");

    }
}
