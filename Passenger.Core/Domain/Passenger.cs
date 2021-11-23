namespace Passenger.Core.Domain;

public class Passenger
{
    public Passenger(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; protected set; }
    public Node Address { get; protected set; }
}