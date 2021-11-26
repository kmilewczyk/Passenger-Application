using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Commands.Drivers;

public class CreateDriver : AuthenticatedCommandBase
{
    public VehicleDto Vehicle { get; set; }
    public sealed record DriverVehicle(string Brand, string Name);
}