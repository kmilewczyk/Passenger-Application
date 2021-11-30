using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Drivers;
using Passenger.Infrastructure.Exceptions;
using Passenger.Infrastructure.Services;

namespace Passenger.Infrastructure.Handlers.Drivers;

public class UpdateDriverHandler : ICommandHandler<UpdateDriver>
{
    private readonly IDriverService _driverService;

    public UpdateDriverHandler(IDriverService driverService)
    {
        _driverService = driverService;
    }

    public async Task HandleAsync(UpdateDriver command)
    {
        var vehicle = command.Vehicle;
        if (vehicle == null)
        {
            throw new ServiceException(ErrorCodes.NullCommandParameter, "Vehicle field is empty");
        }
        await _driverService.SetVehicle(command.UserId, vehicle.Brand, vehicle.Name);
    }
}