using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Drivers;
using Passenger.Infrastructure.Exceptions;
using Passenger.Infrastructure.Services;

namespace Passenger.Infrastructure.Handlers.Drivers;

public class DeleteDriverRouteHandler : ICommandHandler<DeleteDriverRoute>
{
    private readonly IDriverRouteService _driverRouteService;

    public DeleteDriverRouteHandler(IDriverRouteService driverRouteService)
    {
        _driverRouteService = driverRouteService;
    }

    public async Task HandleAsync(DeleteDriverRoute command)
    {
        if (command.Name is null)
        {
            throw new ServiceException(Exceptions.ErrorCodes.NullCommandParameter, "Name parameter cannot be null");
        }

        await _driverRouteService.DeleteAsync(command.UserId, command.Name);
    }
}