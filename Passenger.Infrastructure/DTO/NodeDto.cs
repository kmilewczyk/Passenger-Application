using AutoMapper;

namespace Passenger.Infrastructure.DTO;

[AutoMap(typeof(Core.Domain.Node))]
public record NodeDto(string Address, double Longitude, double Latitude);