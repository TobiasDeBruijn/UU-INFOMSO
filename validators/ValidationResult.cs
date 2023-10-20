namespace mso.validators; 

public record ValidationResult {
    public readonly bool IsValid;
    public readonly bool WillPlayerStayAtSet;
    public readonly List<Stone> GainedStones;
    
    private ValidationResult(bool isValid, bool willPlayerStayAtSet, List<Stone> gainedStones) {
        IsValid = isValid;
        WillPlayerStayAtSet = willPlayerStayAtSet;
        GainedStones = gainedStones;
    }

    public static ValidationResult Invalid() => new(false, false, new List<Stone>());
    public static ValidationResult Valid(bool willRemainAtSet, List<Stone> gainedStone) => new(true, willRemainAtSet, gainedStone);
}