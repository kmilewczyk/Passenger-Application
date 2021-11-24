using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Mappers;

public static class AutoMapperConfig
{
    public static IMapper Initialize()
        => new MapperConfiguration(cfg =>
        {
            Console.WriteLine(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
            
            cfg.AddMaps(new []
            {
                "Passenger.Infrastructure",
                "Passenger.Core"
            });
        }).CreateMapper();
}