using System.Globalization;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.CompilerServices;
using Passenger.Infrastructure.Commands.Users;
using Passenger.Infrastructure.DTO;
using Passenger.Infrastructure.Services;

namespace Passenger.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> Get(string? email)
    {
        if (email is null)
        {
            return new JsonResult(await _userService.GetAll());
        }
        
        var user = await _userService.Get(email);

        if (user == null)
        {
            return NotFound();
        }

        return new JsonResult(user);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateUser request)
    {
        await _userService.Register(request.Email, request.Username, request.Password);
        return Created($"api/users/{request.Email}", null);
    }
}