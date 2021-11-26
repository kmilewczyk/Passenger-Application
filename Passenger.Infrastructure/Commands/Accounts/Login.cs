using Passenger.Infrastructure.Commands;

namespace Passenger.Infrastructure.Handlers.Users;

public record Login(Guid TokenId, string Email, string Password) : ICommand;