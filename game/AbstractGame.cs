using mso.reader;

namespace mso.game;

public class AbstractGame {
    protected readonly IReader Reader;

    protected AbstractGame(IReader reader) {
        Reader = reader;
    }
}