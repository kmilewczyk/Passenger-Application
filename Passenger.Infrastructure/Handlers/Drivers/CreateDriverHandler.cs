using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Drivers;
using Passenger.Infrastructure.Services;

namespace Passenger.Infrastructure.Handlers.Drivers;

public class CreateDriverHandler : ICommandHandler<CreateDriver>
{
    private readonly IDriverService _driverService;

    public CreateDriverHandler(IDriverService driverService)
    {
        _driverService = driverService;
    }

    public Task HandleAsync(CreateDriver command)
    {
        // TODO this
        return Task.CompletedTask;
    }
}