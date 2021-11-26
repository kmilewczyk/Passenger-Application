using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Services;

public interface IDriverService : IService
{
    Task<DriverDetailsDto?> GetAsync(Guid userId);
    Task CreateAsync(Guid userId);
    Task SetVehicle(Guid userId, string brand, string name);
    Task<IEnumerable<DriverDto>> BrowseAsync();
    Task DeleteAsync(Guid userId);
}