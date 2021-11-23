using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Moq;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.Services;
using Xunit;

namespace Passenger.Tests.Services;

public class UserServiceTests
{
    [Fact]
    public async Task RegisterShouldInvokeAddAsyncOnRepository()
    {
        var userRepositoryMock = new Mock<IUserRepository>();
        var mapperMock = new Mock<IMapper>();

        var userService = new UserService(userRepositoryMock.Object, mapperMock.Object);
        await userService.Register("user@email.com", "user", "secret");
        
        userRepositoryMock.Verify(x => x.Add(It.IsAny<User>()), Times.Once);
    }
}