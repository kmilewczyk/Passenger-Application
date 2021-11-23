using AutoMapper;
using Passenger.Core.Domain;

namespace Passenger.Infrastructure.DTO;

[AutoMap(typeof(Core.Domain.Route))]
public record Route(Guid Id, Core.Domain.Node StartNode, Core.Domain.Node EndNode);