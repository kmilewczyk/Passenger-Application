using System;
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
        var encrypterMock = new Mock<IEncrypter>();

        var userService = new UserService(userRepositoryMock.Object, encrypterMock.Object, mapperMock.Object);
        await userService.RegisterAsync(new Guid(), "user@email.com", "user", "secret", UserRole.User);
        
        userRepositoryMock.Verify(x => x.Add(It.IsAny<User>()), Times.Once);
    }
}