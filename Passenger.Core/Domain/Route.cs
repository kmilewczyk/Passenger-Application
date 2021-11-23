namespace Passenger.Core.Domain;

public record Route(Guid Id, Node StartNode, Node EndNode);