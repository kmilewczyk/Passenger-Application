using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Services;

public interface IUserService
{
    void Register(string email, string username, string password);
    User Get(string email);
}