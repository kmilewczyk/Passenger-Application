using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Drivers;

namespace Passenger.Api.Controllers;

public class DriverController : ApiControllerBase
{
    public DriverController(ICommandDispatcher commandDispatcher) : base(commandDispatcher)
    { 
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateDriver command)
    {
        await CommandDispatcher.DispatchAsync(command);
        
        return Created($"api/drivers?userid={command.UserId}", null);
    } 
}