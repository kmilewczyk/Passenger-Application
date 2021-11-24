namespace Passenger.Core.Domain;

public class Passenger
{
    public Passenger(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; protected set; }

    protected bool Equals(Passenger other)
    {
        return UserId.Equals(other.UserId);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Passenger) obj);
    }

    public override int GetHashCode()
    {
        return UserId.GetHashCode();
    }

    public Node Address { get; protected set; }
}