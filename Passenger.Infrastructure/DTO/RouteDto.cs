using AutoMapper;
using Passenger.Core.Domain;

namespace Passenger.Infrastructure.DTO;

[AutoMap(typeof(Core.Domain.Route))]
public record RouteDto(string Name, Core.Domain.Node StartNode, Core.Domain.Node EndNode);