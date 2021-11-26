namespace Passenger.Core.Domain;

public record Route(string Name, Node StartNode, Node EndNode, double Distance);