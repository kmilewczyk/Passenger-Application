using AutoMapper;

namespace Passenger.Infrastructure.DTO;

[AutoMap(typeof(Core.Domain.Node))]
public record Node(string Address, double Longitude, double Latitude);