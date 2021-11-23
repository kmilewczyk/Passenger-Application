using AutoMapper;

namespace Passenger.Infrastructure.DTO;

[AutoMap(typeof(Core.Domain.Passenger))]
public record Passenger(Guid UserId, Node Adress);