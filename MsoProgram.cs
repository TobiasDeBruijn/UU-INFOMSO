using mso.game;
using mso.printer;

namespace mso;

public static class MsoProgram {
    public static void Main() {

        int selectedGame = Reader.ReadInt(
        "Enter the number of the game you want to play. 1: Mankela, 2: Wari: ",
            1,
            3,
        null
        );

        int pitStartCount = Reader.ReadInt(
            "Enter the number of stones in the starting pits: ",
            4,
            null,
            null
        );

        int boardSize = Reader.ReadInt(
            "Enter the size of the board (even number, at least 4): ",
            4,
            null,
            (v) => v % 2 == 0
        );
        
        Board board = new Board(
            boardSize,
            pitStartCount,
            selectedGame == 1
        );

        IGame engine = selectedGame == 1 ? new MankelaImpl() : new WariImpl();
        IPrinter printer = new ConsolePrinter();
        
        int currentPlayer = 1; // 1 for player 1, 2 for player 2
        
        bool gameFinished = false;
        while (!gameFinished) {
            printer.Print(board);
            currentPlayer = engine.Step(board, currentPlayer);
            gameFinished = engine.IsFinished(board);
        }

        printer.Print(board);
        Console.WriteLine("Game Over!");
    }
}
