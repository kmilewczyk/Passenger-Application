using AutoMapper.Internal;
using Microsoft.Extensions.Caching.Memory;
using Passenger.Core.Domain;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Services;

class VehicleProvider : IVehicleProvider
{
    private readonly IMemoryCache _cache;
    private readonly static string CacheKey = "vehicles";

    private static readonly IDictionary<string, IEnumerable<VehicleDetails>> availableVehicles =
        new Dictionary<string, IEnumerable<VehicleDetails>>()
        {
            ["Audi"] = new List<VehicleDetails>()
            {
                new VehicleDetails("RS8", 5)
            },
            ["BMW"] = new List<VehicleDetails>()
            {
                new VehicleDetails("i8", 3),
                new VehicleDetails("E36", 5)
            },
            ["Ford"] = new List<VehicleDetails>()
            {
                new VehicleDetails("Fiesta", 5)
            },
            ["Skoda"] = new List<VehicleDetails>()
            {
                new VehicleDetails("Fabia", 5),
                new VehicleDetails("Rapid", 5)
            },
            ["Volkswagen"] = new List<VehicleDetails>()
            {
                new VehicleDetails("Passat", 5)
            },
        };

    public VehicleProvider(IMemoryCache cache)
    {
        _cache = cache;
    }

    public async Task<IEnumerable<VehicleDto>> BrowseAsync()
    {
        var vehicles = _cache.Get<IEnumerable<VehicleDto>>(CacheKey);
        
        // Avoid possible double enumeration
        var vehicleDtos = vehicles as VehicleDto[] ?? vehicles.ToArray();
        if (vehicles == null)
        {
            vehicles = await GetAllAsync();
            _cache.Set(CacheKey, vehicleDtos);
        }

        return vehicleDtos;
    }

    public async Task<VehicleDto> GetAsync(string brand, string name)
    {
        var vehicles = availableVehicles.GetOrDefault(brand) ??
                       throw new Exception($"Vehicle brand: '{brand}' is not available.");

        var vehicle = vehicles.SingleOrDefault(x => x.Name == name) ??
                       throw new Exception($"Vehicle '{name}' for brand '{brand}' is not available.");

        return await Task.FromResult(new VehicleDto(brand, name, vehicle.Seats));
    }

    public async Task<IEnumerable<VehicleDto>> GetAllAsync()
        => await Task.FromResult(
            availableVehicles.GroupBy(x => x.Key)
                .SelectMany(g => g.SelectMany(
                    (v => v.Value.Select(c => new VehicleDto
                        (
                            Brand: v.Key,
                            Name: c.Name,
                            Seats: c.Seats
                        ))
                    ))
                )
        );

    private class VehicleDetails
    {
        public VehicleDetails(string name, int seats)
        {
            Seats = seats;
            Name = name;
        }

        public string Name { get; set; }
        public int Seats { get; set; }
    }
}