using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Passenger.Infrastructure.DTO;
using Xunit;
using Xunit.Abstractions;

namespace Passenger.Tests.EndToEnd.Controllers;

public abstract class ControllerTestsBase : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly ITestOutputHelper _testOutputHelper;
    protected readonly HttpClient Client;
    protected static readonly JsonSerializerOptions Options = new() {PropertyNamingPolicy = JsonNamingPolicy.CamelCase};

    protected ControllerTestsBase(WebApplicationFactory<Program> factory, ITestOutputHelper testOutputHelper)
    {
        Client = factory.CreateClient();
        _testOutputHelper = testOutputHelper;
    }

    protected async Task<User> GetUser(string email)
    {
        var response = await Client.GetAsync($"api/users?email={email}");

        return await response.Content.FromJson<User>(Options);
    }

    protected void WriteLine(string line)
    {
        _testOutputHelper.WriteLine(line);
    }

    protected static StringContent CreatePayload<T>(T request)
        => new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
}