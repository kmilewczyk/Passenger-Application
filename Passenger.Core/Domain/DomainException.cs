namespace Passenger.Core.Domain;

public class DomainException : PassengerException
{
    public string Code { get; init; }

    protected DomainException()
    {
    }

    protected DomainException(string code)
    {
        Code = code;
    }

    public DomainException(string message, params object[] args)
        : base(string.Empty, message, args)
    {
    }

    public DomainException(string code, string message, params object[] args)
        : base(null, string.Empty, message, args)
    {
    }

    public DomainException(Exception? innerException, string message, params object[] args)
        : base(innerException, string.Empty, message, args)
    {
    }

    public DomainException(Exception? innerException, string code, string message, params object[] args)
        : base(innerException, code, message, args)
    {
    }
}