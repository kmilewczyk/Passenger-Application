using AutoMapper;

namespace Passenger.Infrastructure.DTO;

[AutoMap(typeof(Core.Domain.User))]
public record User(Guid Id, string? Email, string? Username, string? Fullname);