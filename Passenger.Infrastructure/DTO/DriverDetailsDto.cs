namespace Passenger.Infrastructure.DTO;

public record DriverDetailsDto(IEnumerable<RouteDto> Routes, IEnumerable<DailyRouteDto>? DailyRoute = null) : DriverDto;