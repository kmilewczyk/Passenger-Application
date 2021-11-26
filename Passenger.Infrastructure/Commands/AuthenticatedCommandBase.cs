namespace Passenger.Infrastructure.Commands;

public abstract class AuthenticatedCommandBase : IAuthenticatedCommand
{
    public Guid UserId { get; set; }
}