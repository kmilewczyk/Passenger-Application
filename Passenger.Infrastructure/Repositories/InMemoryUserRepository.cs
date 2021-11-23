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

    public User Get(string email)
        => _users.Single(x => string.Equals(x.Email, email, StringComparison.InvariantCultureIgnoreCase));

    public User Get(Guid id)
        => _users.Single(x => x.Id == id);

    public void Add(User user)
    {
        _users.Add(user);
    }

    public void Remove(Guid id)
        => _users.Remove(Get(id));

    public void Update(User user)
    {
    }

    public IEnumerable<User> GetAll()
        => _users;
}