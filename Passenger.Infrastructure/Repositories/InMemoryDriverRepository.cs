using Passenger.Core.Domain;
using Passenger.Core.Repositories;

namespace Passenger.Infrastructure.Repositories;

public class InMemoryDriverRepository : IDriverRepository
{
    public static ISet<Driver> _drivers = new HashSet<Driver>();

    public void Add(Driver driver) => _drivers.Add(driver);

    public Driver Get(Guid userId) => _drivers.Single(driver => driver.UserId == userId);

    public IEnumerable<Driver> GetAll() => _drivers;

    public void Update(Driver driver) => throw new NotImplementedException();
}