using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Services;

namespace Passenger.Api.Controllers;

public class AccountController : ApiControllerBase
{
    private readonly IJwtHandler _jwtHandler;

    public AccountController(ICommandDispatcher commandDispatcher, IJwtHandler jwtHandler) : base(commandDispatcher)
    {
        _jwtHandler = jwtHandler;
    }

    [HttpGet]
    [Route("token")]
    public async Task<IActionResult> Get()
    {
        return new JsonResult(_jwtHandler.CreateToken("user1@email.com", "admin"));
    }

    [HttpPut]
    [Route("password")]
    public async Task<IActionResult> Post([FromBody] ChangeUserPassword command)
    {
        await CommandDispatcher.DispatchAsync(command);

        return NoContent();
    }
}