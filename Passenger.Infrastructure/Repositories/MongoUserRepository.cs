using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;

namespace Passenger.Infrastructure.Repositories;

public class MongoUserRepository : IUserRepository, IMongoRepository
{
    private readonly IMongoDatabase _database;

    public MongoUserRepository(IMongoDatabase database)
    {
        _database = database;
    }

    public async Task<User?> GetAsync(string email)
        => await Users.AsQueryable().FirstOrDefaultAsync(x => x.Email == email);

    public async Task<User?> GetAsync(Guid id)
        => await Users.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

    public async Task Add(User user)
        => await Users.InsertOneAsync(user);

    public async Task Remove(Guid id)
        => await Users.DeleteOneAsync(x => x.Id == id);

    public async Task Update(User user)
        => await Users.ReplaceOneAsync(x => x.Id == user.Id, user);

    public async Task<IEnumerable<User>> BrowseAll()
        => await Users.AsQueryable().ToListAsync();

    private IMongoCollection<User> Users => _database.GetCollection<User>("Users");
}