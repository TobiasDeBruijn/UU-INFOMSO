namespace mso.validators; 

public class MankelaValidator : IGameRuleValidator {
    public ValidationResult IsMoveValid(Move move) {
        for (int i = 0; i < move.StoneMovements.Count; i++) {
            StoneMovement m = move.StoneMovements[i];

            if (i + 1 == move.StoneMovements.Count) {
                // Last stone in the move

                if (m.DestinationPit.Owner == move.Player) {
                    return ValidationResult.Valid(true, new List<Stone>());
                }

                if (m.DestinationPit.Stones.Count != 0) {
                    ValidationResult r = ValidationResult.Valid(true, m.DestinationPit.Stones);
                    m.DestinationPit.Stones.Clear();
                    return r;
                }

                if (m.DestinationPit.Stones.Count == 0 && m.DestinationPit.Owner == move.Player.GetOther()) {
                    return ValidationResult.Valid(false, new List<Stone>());
                }
            }
        }

        return ValidationResult.Invalid();
    }
}