namespace Passenger.Infrastructure.DTO;

public record JwtDto(string Token, long Expiry);