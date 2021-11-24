namespace Passenger.Infrastructure.Commands.Drivers;

public sealed record CreateDriver(Guid UserId, CreateDriver.DriverVehicle Vehicle) : ICommand
{
    public sealed record DriverVehicle(string Brand, string Name, int Seats);
}