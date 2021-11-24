using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;

namespace Passenger.Api.Controllers;

public class AccountController : ApiControllerBase
{
    public AccountController(ICommandDispatcher commandDispatcher) : base(commandDispatcher)
    {
    }
    
    
    [HttpPut]
    [Route("password")]
    public async Task<IActionResult> Post([FromBody] ChangeUserPassword command)
    {
        await CommandDispatcher.DispatchAsync(command);

        return NoContent();
    }
}