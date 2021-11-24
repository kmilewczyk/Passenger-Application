using AutoMapper;

namespace Passenger.Infrastructure.DTO;

[AutoMap(typeof(Core.Domain.User))]
public record UserDto(Guid Id, string? Email, string? Username, string? Fullname);