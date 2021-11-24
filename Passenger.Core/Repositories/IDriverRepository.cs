using Passenger.Core.Domain;

namespace Passenger.Core.Repositories;

public interface IDriverRepository : IRepository
{
    public void Add(Driver driver);
    public Driver Get(Guid userId);
    public IEnumerable<Driver> GetAll();
    public void Update(Driver driver);
}