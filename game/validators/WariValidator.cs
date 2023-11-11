namespace mso.game.validators; 

public class WariValidator : IGameRuleValidator {
    public ValidationResult IsMoveValid(GameManager gm, Move move) {
        for (int i = 0; i < move.StoneMovements.Count; i++) {
            StoneMovement m = move.StoneMovements[i];

            if (i + 1 == move.StoneMovements.Count) {
                // Last stone movement

                if (m.DestinationPit.Owner == move.PlayerKey.GetOther() && m.DestinationPit.Stones.Count < 3) {
                    Player pdata = gm.GetPlayerData(move.PlayerKey.GetOther());
                    
                    pdata.OwnedStones.AddRange(m.DestinationPit.Stones);
                    pdata.OwnedStones.Add(m.Stone);

                    gm.SetPlayerData(pdata);
                    
                    List<Stone> gained = new();
                    gained.AddRange(m.DestinationPit.Stones);
                    gained.Add(m.Stone);
                    
                    m.DestinationPit.Stones.Clear();

                    return ValidationResult.Valid(false, gained);
                }

                return ValidationResult.Valid(false, new List<Stone>());
            }
        }
        
        return ValidationResult.Invalid();
    }
}