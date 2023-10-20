namespace mso; 

public record StoneMovement(Pit OriginPit, Pit DestinationPit, Stone Stone) {
    public readonly Pit OriginPit = OriginPit;
    public readonly Pit DestinationPit = DestinationPit;
    public readonly Stone Stone = Stone;
}