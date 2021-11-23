using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper.Internal;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.OpenApi.Expressions;
using Passenger.Infrastructure.Commands.Users;
using Passenger.Infrastructure.DTO;
using Xunit;
using Xunit.Abstractions;

namespace Passenger.Tests.EndToEnd.Controllers;

public class UserControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly HttpClient _client;
    private static readonly JsonSerializerOptions Options = new(){ PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public UserControllerTests(WebApplicationFactory<Program> factory, ITestOutputHelper testOutputHelper)
    {
        _client = factory.CreateClient();
        _testOutputHelper = testOutputHelper;
    }

    [Theory]
    [InlineData("user1@email.com", HttpStatusCode.OK, "user1@email.com")]
    [InlineData("admin1233@email.com", HttpStatusCode.NotFound, null)]
    public async Task GivenValidEmailTestResponse(string email, HttpStatusCode statusCode, string emailResponse)
    {

        var response = await _client.GetAsync($"api/users?email={email}");
        response.StatusCode.Should().Be(statusCode);

        var user = await response.Content.FromJson<User>(Options);

        user!.Email.Should().Be(emailResponse);
    }
    
    [Fact]
    public async Task GivenValidEmailUserShouldExist()
    {

        const string email = "user1@email.com";
        var response = await _client.GetAsync($"api/users?email={email}");
        response.EnsureSuccessStatusCode();

        var user = await response.Content.FromJson<User>(Options);

        user!.Email.Should().Be(email);
    }

    [Fact]
    public async Task EmptyGetRequestShouldReturnArray()
    {
        var response = await _client.GetAsync($"api/users");
        response.EnsureSuccessStatusCode();

        var users = await response.Content.FromJson<List<User>>(Options);
        users.Should().BeAssignableTo<IEnumerable<User>>();
    }

    [Fact]
    public async Task GivenUniqueEmailUserShouldBeCreated()
    {
        var request = new CreateUser
        {
            Email = "test@email.com",
            Username = "test",
            Password = "secret"
        };

        var payload = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("api/users", payload);
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location.Should().NotBeNull();
        response.Headers.Location!.ToString().Should().Be($"api/users/{request.Email}");

        var user = await GetUser(request.Email);
        user.Email.Should().Be(request.Email);
    }

    private async Task<User> GetUser(string email)
    {
        var response = await _client.GetAsync($"api/users?email={email}");

        return await response.Content.FromJson<User>(Options);
    }
}