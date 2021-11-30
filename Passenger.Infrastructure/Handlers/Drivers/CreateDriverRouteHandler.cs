using System.Runtime.Serialization;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Drivers;
using Passenger.Infrastructure.Exceptions;
using Passenger.Infrastructure.Services;

namespace Passenger.Infrastructure.Handlers.Drivers;

public class CreateDriverRouteHandler : ICommandHandler<CreateDriverRoute>
{
    private readonly IDriverRouteService _driverRouteService;

    public CreateDriverRouteHandler(IDriverRouteService driverRouteService)
    {
        _driverRouteService = driverRouteService;
    }

    public async Task HandleAsync(CreateDriverRoute command)
    {
        if (command.Name is null)
        {
            throw new ServiceException(Exceptions.ErrorCodes.NullCommandParameter, "Name parameter is required");
        }
        
        await _driverRouteService.AddAsync(command.UserId, command.Name,
            command.StartLatitude, command.EndLatitude,
            command.StartLongitude, command.EndLongitude
        );
    }
}