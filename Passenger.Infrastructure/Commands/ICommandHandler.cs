namespace Passenger.Infrastructure.Commands;

public interface ICommandHandler<T> : ICommand
{
    Task HandleAsync(T command);
}