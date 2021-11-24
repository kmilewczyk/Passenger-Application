using System.Runtime.Versioning;

namespace Passenger.Core.Domain;

public sealed class DailyRoute
{
    private ISet<PassengerNode> _passengerNodes = new HashSet<PassengerNode>();

    public DailyRoute(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; protected set; }
    public Route Route { get; protected set; }
    public IEnumerable<PassengerNode> PassengerNodes => _passengerNodes;

    public void AddPassengerNode(Passenger passenger, Node node)
    {
        var passengerNode = GetPassengerNode(passenger);
        if (passengerNode != null)
        {
            throw new InvalidOperationException($"Node already exists for passenger: {passenger.UserId}");
        }

        _passengerNodes.Add(new PassengerNode(node, passenger));
    }

    public void RemovePassengerNode(Passenger passenger)
    {
        var passengerNode = GetPassengerNode(passenger);
        if (passengerNode is null)
        {
            return;
        }

        _passengerNodes.Remove(passengerNode);
    }

    private PassengerNode? GetPassengerNode(Passenger passenger)
        => _passengerNodes.SingleOrDefault(x => Equals(x.Passenger, passenger));
}