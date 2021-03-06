using Passenger.Infrastructure.Commands.Drivers.Models;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Commands.Drivers;

public class CreateDriver : AuthenticatedCommandBase
{
    public DriverVehicle Vehicle { get; set; }
}