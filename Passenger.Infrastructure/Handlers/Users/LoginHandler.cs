using Microsoft.Extensions.Caching.Memory;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Extensions;
using Passenger.Infrastructure.Services;

namespace Passenger.Infrastructure.Handlers.Users;

public class LoginHandler : ICommandHandler<Login>
{
    private readonly IHandler _handler;
    private readonly IUserService _userService;
    private readonly IJwtHandler _jwtHandler;
    private readonly IMemoryCache _cache;

    public LoginHandler(IHandler handler, IUserService userService, IJwtHandler jwtHandler, IMemoryCache cache)
    {
        _handler = handler;
        _userService = userService;
        _jwtHandler = jwtHandler;
        _cache = cache;
    }

    public async Task HandleAsync(Login command)
        => await _handler
            .Run(async () => await _userService.LoginAsync(command.Email, command.Password))
            .Next()
            .Run(async () =>
            {
                var user = await _userService.GetAsync(command.Email);

                if (user is null)
                {
                    throw new Exception($"User with an '{command.Email}' doesn't exists.");
                }

                var jwt = _jwtHandler.CreateToken((Guid) user.Id!, user.Role!);
                _cache.SetJwt(command.TokenId, jwt);
            })
            .ExecuteAsync();
}