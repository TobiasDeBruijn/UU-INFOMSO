namespace mso.game.validators; 

public class MakelaImplementor : IGameImplementor {
    public Board InitializeBoard(int numPits, int numStonesPerPit) {
        Board b = Board.WithNumPits(numPits, true);
        b.InitializePitsEqually(numStonesPerPit, false);
        return b;
    }

    public (bool, Board?) ApplyMove(Board currentBoard, Move move) {
        BoardRow? pitRow = currentBoard.GetAssociatedRow(move.StartingPit);
        if (pitRow == null) return (false, null);
        
        // Compute the new board
        Board newBoard = currentBoard.DeepClone();
        
        int indexOfPit = pitRow.GetPitIndex(move.StartingPit);
        int pitsInRowLeft = pitRow.GetNumPits() - 1 - indexOfPit;
        
        

        // for (int i = 0; i < move.StoneMovements.Count; i++) {
        //     StoneMovement m = move.StoneMovements[i];
        //
        //     if (i + 1 == move.StoneMovements.Count) {
        //         // Last stone in the move
        //
        //         if (m.DestinationPit.Owner == move.PlayerKey) {
        //             return ValidationResult.Valid(true, new List<Stone>());
        //         }
        //
        //         if (m.DestinationPit.Stones.Count != 0) {
        //             ValidationResult r = ValidationResult.Valid(true, m.DestinationPit.Stones);
        //             m.DestinationPit.Stones.Clear();
        //             return r;
        //         }
        //
        //         if (m.DestinationPit.Stones.Count == 0 && m.DestinationPit.Owner == move.PlayerKey.GetOther()) {
        //             return ValidationResult.Valid(false, new List<Stone>());
        //         }
        //     }
        // }
        //
        // return ValidationResult.Invalid();
    }
}