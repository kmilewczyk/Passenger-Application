using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Services;

namespace Passenger.Api.Controllers;

public class VehicleController : ApiControllerBase
{
    private readonly IVehicleProvider _vehicleProvider;

    public VehicleController(ICommandDispatcher commandDispatcher, IVehicleProvider vehicleProvider) : base(
        commandDispatcher)
    {
        _vehicleProvider = vehicleProvider;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var vehicles = await _vehicleProvider.BrowseAsync();

        return new JsonResult(vehicles);
    }
}