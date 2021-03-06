using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.DTO;
using Passenger.Infrastructure.Exceptions;
using ErrorCodes = Passenger.Infrastructure.Exceptions.ErrorCodes;

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

    public async Task RegisterAsync(Guid userId, string email, string username, string password, UserRole role)
    {
        var user = _userRepository.GetAsync(email).Result;
        if (user != null)
        {
            throw new ServiceException(ErrorCodes.EmailInUse, $"User with email {email} already exists.");
        }

        var salt = _encrypter.GetSalt(password);
        var hash = _encrypter.GetHash(password, salt);
        user = new User(userId, email, username, hash , salt, role);
        
        await _userRepository.Add(user);
    }

    public async Task<UserDto?> GetAsync(string email)
        => _mapper.Map<UserDto?>(await _userRepository.GetAsync(email));

    public async Task<IEnumerable<UserDto>> GetAll()
        => _mapper.Map<IEnumerable<UserDto>>(await _userRepository.BrowseAll());

    public async Task LoginAsync(string email, string password)
    {
        var user = await _userRepository.GetAsync(email);
        if (user is null)
        {
            throw new ServiceException(ErrorCodes.InvalidCredentials, $"Invalid credentials");
        }
        
        
        var hash = _encrypter.GetHash(password, user.Salt);
        if (user.Password == hash)
        {
            return;
        }

        throw new ServiceException(ErrorCodes.InvalidCredentials, "Invalid credentials");
    }
}