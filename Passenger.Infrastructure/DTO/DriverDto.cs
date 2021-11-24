using AutoMapper;
using Passenger.Core.Domain;

namespace Passenger.Infrastructure.DTO;

[AutoMap(typeof(Core.Domain.Driver))]
public record DriverDto(Guid UsedId, VehicleDto? Vehicle, IEnumerable<RouteDto>? Routes,
    IEnumerable<Core.Domain.DailyRoute>? DailyRoutes);