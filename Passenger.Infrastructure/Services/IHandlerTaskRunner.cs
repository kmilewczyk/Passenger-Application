namespace Passenger.Infrastructure.Services;

public interface IHandlerTaskRunner
{
    IHandlerTask Run(Func<Task> run);
}