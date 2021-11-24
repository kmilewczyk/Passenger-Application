using AutoMapper;

namespace Passenger.Infrastructure.DTO;

[AutoMap(typeof(Core.Domain.DailyRoute))]
public record DailyRouteDto(Guid Id, RouteDto RouteDto, IEnumerable<PassengerNodeDto> PassengerNodes);