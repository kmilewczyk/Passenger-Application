using AutoMapper;

namespace Passenger.Infrastructure.DTO;

[AutoMap(typeof(Core.Domain.Vehicle))]
public record Vehicle(string Brand, string Name, int Seats);