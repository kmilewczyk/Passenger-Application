using Passenger.Core.Domain;
using Passenger.Core.Repositories;

namespace Passenger.Infrastructure.Extensions;

public static class RepositoryExtensions
{
    public static async Task<Driver> GetOrFailAsync(this IDriverRepository repository, Guid userId)
        => await repository.GetAsync(userId) ??
           throw new Exception($"Driver with user id: '{userId}' was not found");

    public static async Task<User> GetOrFailAsync(this IUserRepository repository, Guid userId)
        => await repository.GetAsync(userId) ??
           throw new Exception($"User with id: '{userId}' was not found");
}