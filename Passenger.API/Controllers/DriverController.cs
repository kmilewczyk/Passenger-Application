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
        await DispatchAsync(command);
        
        return Created($"api/drivers?userid={command.UserId}", null);
    }

    [HttpGet]
    [Route("{userId}")]
    public async Task<IActionResult> Get(Guid userId)
    {
        var driver = await _driverService.GetAsync(userId);
        if (driver is null)
        {
            return NotFound();
        }

        return new JsonResult(driver);
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
        => new JsonResult(await _driverService.BrowseAsync());
}