using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Passenger.Api.Extensions;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Drivers;

namespace Passenger.Api.Controllers;

[Route("drivers/routes")]
public class DriverRoutesController : ApiControllerBase
{
    public DriverRoutesController(ICommandDispatcher commandDispatcher) : base(commandDispatcher)
    {
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateDriverRoute command)
        => await DispatchAsync(command).Return(NoContent());

    [Authorize]
    [HttpDelete("{name}")]
    public async Task<IActionResult> Delete(string name)
        => await DispatchAsync(new DeleteDriverRoute() {Name = name}).Return(NoContent());
}