using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Drivers;
using Passenger.Infrastructure.DTO;
using Passenger.Infrastructure.Services;

namespace Passenger.Api.Controllers;

public class DriverController : ApiControllerBase
{
    private readonly IDriverService _driverService;

    public DriverController(IDriverService driverService, ICommandDispatcher commandDispatcher) : base(commandDispatcher)
    {
        this._driverService = driverService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateDriver command)
    {
        await CommandDispatcher.DispatchAsync(command);
        
        return Created($"api/drivers?userid={command.UserId}", null);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
        => new JsonResult(await _driverService.BrowseAsync());
}