using AutoMapper;

namespace Passenger.Infrastructure.DTO;

[AutoMap(typeof(Core.Domain.PassengerNode))]
public record PassengerNode(Node Node, Passenger Passenger);