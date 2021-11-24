using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Abstractions;

namespace Passenger.Tests.EndToEnd.Controllers;

public class AccountControllerTests : ControllerTestsBase
{
    public AccountControllerTests(WebApplicationFactory<Program> factory, ITestOutputHelper testOutputHelper) : base(
        factory, testOutputHelper)
    {
    }

    [Fact]
    public async Task GivenValidCurrentAndNewPasswrdItShouldBeChanged()
    {
        var command = new ChangeUserPassword(CurrentPassword: "secret", NewPassword: "secret2");

        var payload = CreatePayload(command);
        
        var response = await Client.PutAsync("api/Account/password", payload);
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}