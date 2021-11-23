using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Services;

public interface IUserService
{
    Task Register(string email, string username, string password);
    Task<User?> Get(string email);
    Task<IEnumerable<User>> GetAll();
}