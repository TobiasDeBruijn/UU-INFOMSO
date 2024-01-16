namespace temp.impl;

public class MankelaImpl : IGame {
    public int Step(Board board, int currentPlayer) {
        int selectedPit, relSelectedPit;
        do {
            Console.Write($"Player {currentPlayer}, choose a pit (1-{board.GameplaySize() / 2}): ");
            if (int.TryParse(Console.ReadLine()!, out relSelectedPit)) {
                relSelectedPit -= 1;
            } else {
                relSelectedPit = -1;
            }
            
            // Translate to the correct player
            if(currentPlayer == 2) {
                selectedPit = relSelectedPit + board.GameplaySize() / 2 + 1; 
            } else {
                selectedPit = relSelectedPit;
            }

        } while (relSelectedPit < 0 || relSelectedPit > board.GameplaySize() / 2  || board[selectedPit] == 0);

        int stones = board[selectedPit];
        board[selectedPit] = 0;

        int currentIndex = selectedPit + 1;
        while (stones > 0) {
            // Wrap around
            if(currentIndex >= board.Size()) {
                currentIndex = 0;
            }

            // Skip home pits of the other player
            if((currentPlayer == 1 && currentIndex == board.GameplaySize() + 1) || (currentPlayer == 2 && currentIndex == board.GameplaySize() / 2)) {
                currentIndex++;
                continue;
            }

            bool isInPlayerRange = currentPlayer == 1
                ? currentIndex < board.GameplaySize() / 2
                : currentIndex > board.GameplaySize() / 2 && currentIndex < board.Size() - 1;
            
            if (board[currentIndex] == 0 && isInPlayerRange && stones == 1) {
                int otherIndex = currentPlayer == 1
                    ? board.GameplaySize() - currentIndex
                    : board.GameplaySize() / 2  - (currentIndex - board.GameplaySize() / 2);

                Console.WriteLine(otherIndex);
                
                board[currentPlayer == 1 ? board.GameplaySize() / 2 : board.Size() - 1] = board[otherIndex];
                board[otherIndex] = 0;

                break;
            }
            
            board[currentIndex]++;
            currentIndex++;
            stones--;
        }

        // Check if the last stone landed in the player's Mancala and give them another turn
        if ((currentPlayer == 1 && currentIndex - 1 == board.GameplaySize() / 2) || (currentPlayer == 2 && currentIndex - 1 == board.GameplaySize() + 1)) {
            Console.WriteLine("You get another turn!");
        } else {
            currentPlayer = 3 - currentPlayer; // Switch player
        }

        return currentPlayer;
    }

    public bool IsFinished(Board board) {
        bool player1Empty = true;
        bool player2Empty = true;

        for (int i = 0; i < board.Size() / 2 - 1; i++) {
            if (board[i] != 0) {
                player1Empty = false;
                break;
            }
        }

        for (int i = board.GameplaySize() - 1; i >= board.Size() / 2; i--) {
            if (board[i] != 0) {
                player2Empty = false;
                break;
            }
        }

        return player1Empty || player2Empty;
    }
}