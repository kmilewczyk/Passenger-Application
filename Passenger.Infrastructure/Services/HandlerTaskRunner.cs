namespace Passenger.Infrastructure.Services;

public class HandlerTaskRunner : IHandlerTaskRunner
{
    private readonly Handler _handler;
    private readonly Func<Task> _validate;
    private readonly ISet<IHandlerTask> _handlerTasks;

    public HandlerTaskRunner(Handler handler, Func<Task> validate, ISet<IHandlerTask> handlerTasks)
    {
        _handler = handler;
        _validate = validate;
        _handlerTasks = handlerTasks;
    }

    public IHandlerTask Run(Func<Task> run)
    {
        var handlerTask = new HandlerTask(_handler, run, _validate);
        _handlerTasks.Add(handlerTask);

        return handlerTask;
    }
}