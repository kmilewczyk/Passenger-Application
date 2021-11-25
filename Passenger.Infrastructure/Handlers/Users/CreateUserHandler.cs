using Passenger.Core.Domain;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Users;
using Passenger.Infrastructure.Services;

namespace Passenger.Infrastructure.Handlers.Users;

public class CreateUserHandler : ICommandHandler<CreateUser>
{
    private readonly IUserService _userService;
    private readonly ICommandDispatcher _commandDispatcher;


    public CreateUserHandler(IUserService userService, ICommandDispatcher commandDispatcher)
    {
        _userService = userService;
        _commandDispatcher = commandDispatcher;
    }

    public async Task HandleAsync(CreateUser command)
    {
        await _userService.RegisterAsync(new Guid(), command.Email, command.Username, command.Password, UserRole.User);
    }
}