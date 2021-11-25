namespace Passenger.Core.Domain;

public class Driver
{
    public Driver(User user)
    {
        UserId = user.Id;
        Name = user.Username;
    }

    public Guid UserId { get; protected set; }
    public string Name { get; protected set; }
    public Vehicle? Vehicle { get; protected set; }
    public IEnumerable<Route>? Routes { get; protected set; }
    public IEnumerable<DailyRoute>? DailyRoutes { get; protected set; }
    public DateTime UpdatedAt { get; protected set; }

    public void SetVehicle(string brand, string name, int seats)
    {
        Vehicle = new Vehicle(brand, name, seats);
        UpdatedAt = DateTime.UtcNow;
    }
}