using Passenger.Core.Domain;
using Passenger.Core.Repositories;

namespace Passenger.Infrastructure.Repositories;

public class InMemoryDriverRepository : IDriverRepository
{
    public static ISet<Driver> _drivers = new HashSet<Driver>();

    public Task AddAsync(Driver driver)
    {
        _drivers.Add(driver);
        
        return Task.CompletedTask;
    }

    public Task<Driver?> GetAsync(Guid userId) => Task.FromResult(_drivers.SingleOrDefault(driver => driver.UserId == userId));

    public Task<IEnumerable<Driver>> GetAllAsync() => Task.FromResult<IEnumerable<Driver>>(_drivers);

    public Task UpdateAsync(Driver driver)
    {
        _drivers.Remove(driver);
        _drivers.Add(driver);
        
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Driver driver)
    {
        _drivers.Remove(driver);
        return Task.CompletedTask;
    }
}