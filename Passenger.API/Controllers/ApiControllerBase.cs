using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Services;

namespace Passenger.Api.Controllers;

[ApiController]
[Route("api/[controller]")] // [controller] is an inheriting class
public abstract class ApiControllerBase : ControllerBase
{
    
    private readonly ICommandDispatcher _commandDispatcher;
    private Guid UserId => User?.Identity?.IsAuthenticated == true ? Guid.Parse(User!.Identity?.Name!) : Guid.Empty;


    protected ApiControllerBase(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }
    
    protected async Task DispatchAsync<T>(T command) where T : ICommand
    {
        if (command is IAuthenticatedCommand authenticatedCommand)
        {
            authenticatedCommand.UserId = UserId;
        }

        await _commandDispatcher.DispatchAsync(command);
    }
}