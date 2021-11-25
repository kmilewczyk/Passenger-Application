using Passenger.Core.Domain;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Services;

public interface IUserService : IService
{
    Task RegisterAsync(Guid userId, string email, string username, string password, UserRole role);
    Task<UserDto?> GetAsync(string email);
    Task<IEnumerable<UserDto>> GetAll();
    Task LoginAsync(string email, string password);
}