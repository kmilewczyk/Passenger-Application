using Passenger.Infrastructure.Commands;

public record ChangeUserPassword(string CurrentPassword, string NewPassword) : ICommand;