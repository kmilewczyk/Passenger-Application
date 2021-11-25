using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IEncrypter _encrypter;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IEncrypter encrypter, IMapper mapper)
    {
        _userRepository = userRepository;
        _encrypter = encrypter;
        _mapper = mapper;
    }

    public async Task Register(string email, string username, string password)
    {
        var user = _userRepository.Get(email).Result;
        if (user != null)
        {
            throw new Exception($"User with email {email} already exists.");
        }

        var salt = _encrypter.GetSalt(password);
        var hash = _encrypter.GetHash(password, salt);
        user = new Core.Domain.User(email, username, hash , salt);
        
        await _userRepository.Add(user);
    }

    public async Task<UserDto?> GetAsync(string email)
        => _mapper.Map<Core.Domain.User?, UserDto?>(await _userRepository.Get(email));

    public async Task<IEnumerable<UserDto>> GetAll()
        => (await _userRepository.GetAll()).Select(user => _mapper.Map<Core.Domain.User, UserDto>(user)).ToList();

    public async Task LoginAsync(string email, string password)
    {
        var user = await _userRepository.Get(email);
        if (user is null)
        {
            throw new Exception($"User with email '{email}' does not exist");
        }
        
        
        var salt = _encrypter.GetSalt(password);
        var hash = _encrypter.GetHash(password, salt);
        if (user.Password == hash)
        {
            return;
        }

        throw new Exception("Invalid credentials");
    }
}