using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Services;

public interface IUserService : IService
{
    Task Register(string email, string username, string password);
    Task<UserDto?> GetAsync(string email);
    Task<IEnumerable<UserDto>> GetAll();
    Task LoginAsync(string email, string password);
}