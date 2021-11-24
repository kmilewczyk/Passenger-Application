using Passenger.Infrastructure.Commands;

namespace Passenger.Infrastructure.Handlers.Users;

public class ChangeUserPasswordHandler : ICommandHandler<ChangeUserPassword>
{
    public async Task HandleAsync(ChangeUserPassword command)
    {
        await Task.CompletedTask;
    }
}