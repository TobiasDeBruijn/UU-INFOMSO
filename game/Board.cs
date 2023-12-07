using System.Security.Cryptography.X509Certificates;

namespace mso.game;

public class BoardRow : IDeepClone<BoardRow> {
    public PlayerKey RowOwner;
    public List<Pit> Pit;

    public BoardRow(List<Pit> pits, PlayerKey owner) {
        this.Pit = pits;
        this.RowOwner = owner;
    }

    public bool HasPit(Pit p) {
        return Pit.Any(f => f.Equals(p));
    }

    public int GetPitIndex(Pit p) {
        return Pit.FindIndex(f => f.Equals(p));
    }

    public Pit? GetPitAt(int index) {
        return index > Pit.Count ? null : Pit[index];
    }

    public int GetNumPits() {
        return Pit.Count;
    }

    public BoardRow DeepClone() {
        return new BoardRow(
            Pit.ConvertAll(f => f.DeepClone()),
            RowOwner
        );
    }
}

/// <summary>
/// The board of the game.
///
/// The board is layer out as follows:
/// +---+---+---+---+---+
/// |   | B | B | B |   |
/// | A +---+---+---+ D |
/// |   | C | C | C |   |
/// +---+---+---+---+---+
/// Where:
/// - A: The home pit of player 1 (If enabled)
/// - B: The pits of player 2
/// - C: The pits of player 1
/// - D: The home pit of player 2 (If enabled)
///
/// The board supports a varying number of pits in the 'rows'. Furthermore, home pits are optional.
/// </summary>
public class Board : IDeepClone<Board> {
    // The row belonging to player 1.
    // In the diagram above: C
    public BoardRow Row1;
    // The row belonging to player 2.
    // In the diagram above: B
    public BoardRow Row2;
    public Dictionary<PlayerKey, Pit>? HomePits;
    
    private Board(BoardRow r1, BoardRow r2, Dictionary<PlayerKey, Pit>? homePits) {
        this.Row1 = r1;
        this.Row2 = r2;
        this.HomePits = homePits;
    }


    private static Pit _InitializePit(Pit current, int numStones, PlayerKey stoneOwner) {
        List<Stone> stones = new List<Stone>();
        for (int i = 0; i < numStones; i++) {
            stones.Add(new Stone(stoneOwner));
        }

        current.Stones = stones;
        return current;
    }
    
    public void InitializePitsEqually(int stonesPerPit, bool includeHomePits) {
        Row1.Pit = Row1.Pit.ConvertAll(pit => _InitializePit(pit, stonesPerPit, Row1.RowOwner));
        Row2.Pit = Row2.Pit.ConvertAll(pit => _InitializePit(pit, stonesPerPit, Row2.RowOwner));

        if (includeHomePits && HomePits != null) {
            // Fill each homepits to have the number of stones it should haves
            HomePits = HomePits.ToDictionary(
                keyValuePair => keyValuePair.Key, 
                keyValuePair => _InitializePit(keyValuePair.Value, stonesPerPit, keyValuePair.Key)
            );
        }
    }
    
    public static Board WithNumPits(int pitsPerRow, bool hasHomePits) {
        Dictionary<PlayerKey, List<Pit>> pits = new() {
            { PlayerKey.Player1, new List<Pit>() },
            { PlayerKey.Player2, new List<Pit>() },
        };

        List<Pit> r1 = new();
        List<Pit> r2 = new();
        
        for (int i = 0; i < pitsPerRow; i++) {
            pits[PlayerKey.Player1].Add(Pit.CreateEmpty());   
            pits[PlayerKey.Player2].Add(Pit.CreateEmpty());
        }

        Dictionary<PlayerKey, Pit>? homePits = null;
        if (hasHomePits) {
            homePits = new Dictionary<PlayerKey, Pit> {
                { PlayerKey.Player1, Pit.CreateEmpty() },
                { PlayerKey.Player2, Pit.CreateEmpty() },
            };
        }
        
        return new Board(
            new BoardRow(r1, PlayerKey.Player1), 
            new BoardRow(r2, PlayerKey.Player2), 
            homePits
        );
    }
    
    public BoardRow? GetAssociatedRow(Pit p) {
        if (Row1.HasPit(p)) {
            return Row1;
        }

        return Row2.HasPit(p) ? Row2 : null;
    }

    public Pit? GetOppositeCell(Pit p) {
        if (Row1.HasPit(p)) {
            return Row2.GetPitAt(Row1.GetPitIndex(p));
        }

        return Row2.HasPit(p) ? Row1.GetPitAt(Row2.GetPitIndex(p)) : null;
    }

    public Board DeepClone() {
        return new Board(
            Row1.DeepClone(),
            Row2.DeepClone(),
            HomePits?.ToDictionary(
                k => k.Key, 
                v => v.Value.DeepClone()
            )
        );
    }
}