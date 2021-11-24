using Passenger.Core.Domain;

namespace Passenger.Core.Repositories;

public interface IUserRepository : IRepository
{
    Task<User?> Get(string email);
    Task<User?> Get(Guid id);
    Task Add(User user);
    Task Remove(Guid id);
    Task Update(User user);
    Task<IEnumerable<User>> GetAll();
}