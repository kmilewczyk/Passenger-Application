using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Services;

public interface IDriverService : IService
{
    Task<DriverDto> GetAsync(Guid userId);
    Task CreateAsync(Guid userId);
    Task SetVehicle(Guid userId, string brand, string name, int seats);
    Task<IEnumerable<DriverDto>> BrowseAsync();
}