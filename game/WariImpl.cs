using mso.reader;

namespace mso.game;

public class WariImpl : AbstractGame, IGame {
    public WariImpl(IReader reader) : base(reader) {}

    public int Step(Board board, int currentPlayer) {
        int selectedPit = Reader.ReadInt(
            $"Player {currentPlayer}, choose a pit (1-{board.GameplaySize() / 2}): ",
            1,
            board.GameplaySize() / 2,
            v => board[v] != 0
        ) - 1;

        if (currentPlayer == 2) {
            selectedPit += board.GameplaySize() / 2;
        }
        
        int stones = board[selectedPit];
        board[selectedPit] = 0;

        int currentIndex = selectedPit + 1;
        while (stones > 0) {
            currentIndex = currentPlayer switch {
                1 when currentIndex == board.GameplaySize() - 1 => 0,
                2 when currentIndex == board.GameplaySize() / 2 + 1 => board.GameplaySize() / 2 + 1,
                _ => currentIndex
            };

            if (currentIndex >= board.GameplaySize()) {
                currentIndex = 0;
            }
            
            board[currentIndex]++;
            currentIndex++;
            stones--;
        }

        // Pit in which the previous stone was set
        int lastPitIndex = currentIndex - 1;
        switch (currentPlayer) {
            case 1 when lastPitIndex <= board.GameplaySize() / 2 - 1 && board[lastPitIndex] == 1: {
                int oppositePitIndex = 2 * board.GameplaySize() - 1 - lastPitIndex;
                if (oppositePitIndex >= board.GameplaySize()) {
                    oppositePitIndex -= board.GameplaySize();
                }
            
                board[board.GameplaySize() / 2 - 1] += board[lastPitIndex] + board[oppositePitIndex];
                board[lastPitIndex] = board[oppositePitIndex] = 0;
                break;
            }
            case 2 when lastPitIndex >= board.GameplaySize() / 2 + 1 &&
                        lastPitIndex <= board.GameplaySize() - 2 && board[lastPitIndex] == 1: {
              
                int oppositePitIndex = board.GameplaySize() / 2 - 1 - (lastPitIndex - board.GameplaySize() / 2);
                board[board.GameplaySize() + 1] += board[lastPitIndex] + board[oppositePitIndex];
                board[lastPitIndex] = board[oppositePitIndex] = 0;
                break;
            }
        }

        // Switch players
        return 3 - currentPlayer;
    }

    public bool IsFinished(Board board) {
        bool player1Empty = true;
        bool player2Empty = true;

        for (int i = 0; i < board.Size() / 3; i++)
            if (board[i] != 0) {
                player1Empty = false;
                break;
            }

        for (int i = 2 * board.Size() / 3; i >= board.Size() / 3 + 1; i--)
            if (board[i] != 0) {
                player2Empty = false;
                break;
            }

        return player1Empty || player2Empty;
    }
}