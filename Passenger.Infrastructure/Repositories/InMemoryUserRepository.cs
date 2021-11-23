using Passenger.Core.Domain;
using Passenger.Core.Repositories;

namespace Passenger.Infrastructure.Repositories;

public class InMemoryUserRepository : IUserRepository
{
    private static ISet<User> _users = new HashSet<User>()
    {
        new User("user1@email.com", "user1", "secret", "salt"),
        new User("user2@email.com", "user2", "secret", "salt"),
        new User("user3@email.com", "user3", "secret", "salt"),
    };

    public Task<User?> Get(string email)
        => Task.FromResult(_users.SingleOrDefault(x => string.Equals(x.Email, email, StringComparison.InvariantCultureIgnoreCase)));

    public Task<User?> Get(Guid id)
        => Task.FromResult(_users.SingleOrDefault(x => x.Id == id));

    public Task Add(User user)
    {
        _users.Add(user);
        return Task.CompletedTask;
    }

    public Task Remove(Guid id)
    {
        var user = Get(id).Result;
        if (user is not null)
        {
            _users.Remove(user);
        }
        else
        {
            throw new Exception($"User with id {id} doesn't exists");
        }

        return Task.CompletedTask;
    }

    public Task Update(User user)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetAll()
        => Task.FromResult<IEnumerable<User>>(_users);
}