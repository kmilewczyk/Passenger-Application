using System.Data;
using AutoMapper;
using AutoMapper.Configuration.Conventions;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Services;

class DriverService : IDriverService
{
    private readonly IDriverRepository _driverRepository;
    private readonly IUserRepository _userRepository;
    private readonly IVehicleProvider _vehicleProvider;
    private readonly IMapper _mapper;

    public DriverService(IDriverRepository driverRepository, IMapper mapper, IUserRepository userRepository,
        IVehicleProvider vehicleProvider)
    {
        _driverRepository = driverRepository;
        _mapper = mapper;
        _userRepository = userRepository;
        _vehicleProvider = vehicleProvider;
    }

    public async Task<DriverDetailsDto?> GetAsync(Guid userId)
        => _mapper.Map<Driver?, DriverDetailsDto?>(await _driverRepository.GetAsync(userId));

    public async Task CreateAsync(Guid userId)
    {
        var user = await _userRepository.GetAsync(userId) ??
                   throw new Exception($"User with id {userId} was not found");

        var driver = await _driverRepository.GetAsync(userId);
        if (driver is not null)
        {
            throw new Exception($"User {userId} already is a driver");
        }

        driver = new Driver(user);
        await _driverRepository.AddAsync(driver);
    }

    public async Task SetVehicle(Guid userId, string brand, string name)
    {
        var driver = await _driverRepository.GetAsync(userId) ??
                     throw new Exception("Driver with user id {userId} wasn't found");

        var vehicle = await _vehicleProvider.GetAsync(brand, name);
        driver.SetVehicle(new Vehicle(brand, name, vehicle.Seats));
    }

    public async Task<IEnumerable<DriverDto>> BrowseAsync()
    {
        var drivers = await _driverRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<Driver>, IEnumerable<DriverDto>>(drivers);
    }
}