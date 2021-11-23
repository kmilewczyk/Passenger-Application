using AutoMapper;

namespace Passenger.Infrastructure.DTO;

[AutoMap(typeof(Core.Domain.DailyRoute))]
public record DailyRoute(Guid Id, Route Route, IEnumerable<PassengerNode> PassengerNodes);