using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;

namespace Passenger.Infrastructure.Services;

class DriverRouteService : IDriverRouteService
{
    private readonly IDriverRepository _driverRepository;
    private readonly IMapper _mapper;
    private readonly IRouteManager _routeManager;

    public DriverRouteService(IDriverRepository driverRepository, IMapper mapper, IRouteManager routeManager)
    {
        _driverRepository = driverRepository;
        _mapper = mapper;
        _routeManager = routeManager;
    }
    
    public async Task AddAsync(Guid userId, string name, double startLatitude, double startLongitude, double endLatitude,
        double endLongitude)
    {
        var driver = await _driverRepository.GetAsync(userId);
        if (driver == null)
        {
            throw new Exception($"Driver with user id: '{userId}' was not found");
        }

        var startAddress = await _routeManager.GetAddressAsync(startLatitude, startLongitude);
        var endAddress = await _routeManager.GetAddressAsync(endLatitude, endLongitude);
        var distance = _routeManager.CalculateDistance(startLatitude, startLongitude, endLatitude, endLongitude);

        var startNode = new Node(startAddress, startLongitude, startLatitude);
        var endNode = new Node(endAddress, endLongitude, endLatitude);

        driver.AddRoute(name, startNode, endNode, distance);
        await _driverRepository.UpdateAsync(driver);
    }

    public async Task DeleteAsync(Guid userId, string name)
    {
        var driver = await _driverRepository.GetAsync(userId);
        if (driver == null)
        {
            throw new Exception($"Driver with user id: '{userId}' was not found");
        }

        driver.DeleteRoute(name);
        await _driverRepository.UpdateAsync(driver);
    }
}