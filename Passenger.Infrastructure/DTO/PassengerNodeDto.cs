using AutoMapper;

namespace Passenger.Infrastructure.DTO;

[AutoMap(typeof(Core.Domain.PassengerNode))]
public record PassengerNodeDto(NodeDto NodeDto, PassengerDto PassengerDto);