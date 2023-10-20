namespace mso.validators; 

public interface IGameRuleValidator {
    ValidationResult IsMoveValid(Move move);
}