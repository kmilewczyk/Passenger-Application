using System.Globalization;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.CompilerServices;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Users;
using Passenger.Infrastructure.DTO;
using Passenger.Infrastructure.Services;

namespace Passenger.Api.Controllers;

public class UsersController : ApiControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService, ICommandDispatcher commandDispatcher) : base(commandDispatcher)
    {
        _userService = userService;
    }
    
    [HttpGet]
    [Authorize(Policy = "admin")]
    public async Task<IActionResult> Get(string? email)
    {
        if (email is null)
        {
            return new JsonResult(await _userService.GetAll());
        }
        
        var user = await _userService.GetAsync(email);

        if (user == null)
        {
            return NotFound();
        }

        return new JsonResult(user);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateUser command)
    {
        await CommandDispatcher.DispatchAsync(command);
        
        return Created($"api/users/{command.Email}", null);
    }

}