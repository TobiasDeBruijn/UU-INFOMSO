namespace mso.game.validators; 

public interface IGameImplementor {
    Board InitializeBoard(int numPits, int numStonesPerPit);
    
    (bool, Board?) ApplyMove(Board currentBoard, Move move);
}