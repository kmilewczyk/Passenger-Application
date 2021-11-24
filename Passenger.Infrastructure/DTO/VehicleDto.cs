using AutoMapper;

namespace Passenger.Infrastructure.DTO;

[AutoMap(typeof(Core.Domain.Vehicle))]
public record VehicleDto(string Brand, string Name, int Seats);