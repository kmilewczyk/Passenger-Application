using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Services;

namespace Passenger.Api.Controllers;

[ApiController]
[Route("api/[controller]")] // [controller] is an inheriting class
public abstract class ApiControllerBase : ControllerBase
{
    
    protected readonly ICommandDispatcher CommandDispatcher;


    protected ApiControllerBase(ICommandDispatcher commandDispatcher)
    {
        CommandDispatcher = commandDispatcher;
    }
}