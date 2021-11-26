using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Mappers;

public static class AutoMapperConfig
{
    public static IMapper Initialize()
        => new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(new []
            {
                "Passenger.Infrastructure",
                "Passenger.Core"
            });
            cfg.CreateMap<Driver, DriverDetailsDto>();
        }).CreateMapper();
}