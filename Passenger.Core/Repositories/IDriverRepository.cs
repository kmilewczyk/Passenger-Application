using Passenger.Core.Domain;

namespace Passenger.Core.Repositories;

public interface IDriverRepository : IRepository
{
    public Task AddAsync(Driver driver);
    public Task<Driver?> GetAsync(Guid userId);
    public Task<IEnumerable<Driver>> GetAllAsync();
    public Task UpdateAsync(Driver driver);
}