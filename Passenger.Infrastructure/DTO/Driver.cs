using AutoMapper;
using Passenger.Core.Domain;

namespace Passenger.Infrastructure.DTO;

[AutoMap(typeof(Core.Domain.Driver))]
public record Driver(Guid UsedId, Vehicle? Vehicle, IEnumerable<Route>? Routes,
    IEnumerable<Core.Domain.DailyRoute>? DailyRoutes);