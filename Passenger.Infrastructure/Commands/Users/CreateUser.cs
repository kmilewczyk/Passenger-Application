namespace Passenger.Infrastructure.Commands.Users;

public record CreateUser(string Email, string Password, string Username) : ICommand;