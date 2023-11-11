namespace mso.game.validators; 

public interface IGameRuleValidator {
    ValidationResult IsMoveValid(GameManager gm, Move move);
}