using AutoMapper;

namespace Passenger.Infrastructure.DTO;

[AutoMap(typeof(Core.Domain.Passenger))]
public record PassengerDto(Guid UserId, NodeDto Adress);