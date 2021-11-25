using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Users;
using Passenger.Infrastructure.Extensions;
using Passenger.Infrastructure.Handlers.Users;

namespace Passenger.Api.Controllers;

public class LoginController : ApiControllerBase
{
    private readonly IMemoryCache _cache;
    
    public LoginController(ICommandDispatcher commandDispatcher, IMemoryCache cache) : base(commandDispatcher)
    {
        _cache = cache;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] LoginForm form)
    {
        Login command = new(new Guid(), form.Email, form.Password);
        await CommandDispatcher.DispatchAsync(command);
        var jwt = _cache.GetJwt(command.TokenId);

        return new JsonResult(jwt);
    }
}