namespace mso.game;

public interface IGame {
    public int Step(Board board, int currentPlayer);

    public bool IsFinished(Board board);
}