namespace temp;

public class Printer {
    /// <summary>
    /// Print the board to stdout
    /// </summary>
    /// <param name="board">The board to print</param>
    public static void Print(Board board) {
        Console.WriteLine(string.Join(", ", board.Select(f => f.ToString())));
        
        // top half
        Console.Write(new string(' ', 2));
        if (board.HasHomePits()) {
            for (int i = board.GameplaySize(); i > board.GameplaySize() / 2; i--) {
                Console.Write($"{board[i]} ");
            }    
        } else {
            for (int i = board.GameplaySize(); i > board.GameplaySize() / 2; i--) {
                Console.Write($"{board[i - 1]} ");
            }
        }
        Console.WriteLine();

        if (board.HasHomePits()) {
            Console.Write($"{board[board.Size() - 1]}");
            Console.Write(new string(' ', board.Size() - 1));
            Console.Write($"{board[board.GameplaySize() / 2]}");
            Console.WriteLine();
        }
        
        // Bottom half
        Console.Write(new string(' ', 2));
        for (int i = 0; i < board.GameplaySize() / 2; i++) {
            Console.Write($"{board[i]} ");
        }
        
        Console.WriteLine();
        Console.WriteLine();
    }
}