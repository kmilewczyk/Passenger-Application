using Passenger.Core.Domain;

namespace Passenger.Core.Repositories;

public interface IUserRepository : IRepository
{
    Task<User?> GetAsync(string email);
    Task<User?> GetAsync(Guid id);
    Task Add(User user);
    Task Remove(Guid id);
    Task Update(User user);
    Task<IEnumerable<User>> BrowseAll();
}