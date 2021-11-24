using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task Register(string email, string username, string password)
    {
        var user = _userRepository.Get(email).Result;
        if (user != null)
        {
            throw new Exception($"User with email {email} already exists.");
        }

        var salt = Guid.NewGuid().ToString(("N"));
        user = new Core.Domain.User(email, username, password, salt);
        await _userRepository.Add(user);
    }

    public async Task<UserDto?> Get(string email)
        => _mapper.Map<Core.Domain.User?, UserDto?>(await _userRepository.Get(email));

    public async Task<IEnumerable<UserDto>> GetAll()
        => (await _userRepository.GetAll()).Select(user => _mapper.Map<Core.Domain.User, UserDto>(user)).ToList();
}