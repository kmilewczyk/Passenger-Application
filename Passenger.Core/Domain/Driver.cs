namespace Passenger.Core.Domain;

public class Driver
{
    private List<Route> _routes = new List<Route>();

    public Driver(User user)
    {
        UserId = user.Id;
        Name = user.Username;
    }

    public Guid UserId { get; protected set; }
    public string Name { get; protected set; }
    public Vehicle? Vehicle { get; protected set; }

    public IEnumerable<Route> Routes
    {
        get => _routes;
        protected set => _routes = value.ToList();
    }

    public IEnumerable<DailyRoute> DailyRoutes { get; protected set; } = new List<DailyRoute>();
    public DateTime UpdatedAt { get; protected set; }

    public void SetVehicle(Vehicle vehicle)
    {
        Vehicle = vehicle;
        UpdatedAt = DateTime.UtcNow;
    }

    public void DeleteRoute(string name)
    {
        var route = Routes.SingleOrDefault(x => x.Name == name) ??
                    throw new Exception($"Route with name '{name}' doesn't exists for driver '{Name}'");
        _routes.Remove(route);
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddRoute(string name, Node start, Node end)
    {
        var route = Routes.SingleOrDefault(x => x.Name == name);
        if (route is not null)
        {
            throw new Exception($"Route with name: '{name}' already exists for driver: {name}.");
        }

        _routes.Add(new Route(name, start, end));
        UpdatedAt = DateTime.UtcNow;
    }
}