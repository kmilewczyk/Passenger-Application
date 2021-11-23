namespace Passenger.Core.Domain;

public class DailyRoute
{
    public DailyRoute(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; protected set; }
    public Route Route { get; protected set; }
    public IEnumerable<PassengerNode> PassengerNodes { get; protected set; }
}