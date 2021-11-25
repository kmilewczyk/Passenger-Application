using AutoMapper;

namespace Passenger.Infrastructure.DTO;

[AutoMap(typeof(Core.Domain.User))]
public record UserDto(Guid? Id = null, string? Email = null, string? Username = null, string? Fullname = null,
    string? Role = null);