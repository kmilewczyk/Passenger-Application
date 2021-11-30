using Passenger.Core.Domain;

namespace Passenger.Infrastructure.Exceptions;

public class ServiceException : PassengerException
{
    public string Code { get; init; }

    protected ServiceException()
    {
    }

    public ServiceException(string code)
    {
        Code = code;
    }

    public ServiceException(string message, params object[] args)
        : base(string.Empty, message, args)
    {
    }

    public ServiceException(string code, string message, params object[] args)
        : base(null, string.Empty, message, args)
    {
    }
    
    public ServiceException(Exception? innerException, string message, params object[] args)
        : base(innerException, string.Empty, message, args)
    {
    }

    public ServiceException(Exception? innerException, string code, string message, params object[] args)
        : base(innerException, code, message, args)
    {
    }
}