using Microsoft.EntityFrameworkCore;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.EF;
using Passenger.Infrastructure.Exceptions;

namespace Passenger.Infrastructure.Repositories;

public class MssqlUserRepository : IUserRepository, ISqlRepository
{
    private readonly PassengerContext _context;

    public MssqlUserRepository(PassengerContext context)
    {
        _context = context;
    }

    public async Task<User?> GetAsync(string email)
        => await _context.Users.SingleOrDefaultAsync(x => x.Email == email);

    public async Task<User?> GetAsync(Guid id)
        => await _context.Users.SingleOrDefaultAsync(x => x.Id == id);

    public async Task Add(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task Remove(Guid id)
    {
        var user = await GetAsync(id) ??
                   throw new ServiceException(
                       Exceptions.ErrorCodes.UserNotFound,
                       $"Cannot remove user with id {id}. They don't exist."
                   );
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task Update(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<User>> BrowseAll()
        => await _context.Users.ToListAsync();
}