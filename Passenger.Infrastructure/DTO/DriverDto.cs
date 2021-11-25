using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using Passenger.Core.Domain;

namespace Passenger.Infrastructure.DTO;

[AutoMap(typeof(Core.Domain.Driver))]
public record DriverDto(Guid? UserId = null, string? Name = null, VehicleDto? Vehicle = null, IEnumerable<RouteDto>? Routes = null,
    IEnumerable<Core.Domain.DailyRoute>? DailyRoutes = null, DateTime? UpdatedAt = null)
{
}