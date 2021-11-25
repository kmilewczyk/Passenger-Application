namespace Passenger.Infrastructure.Services;

public interface IDataInitializer
{
    Task SeedAsync();
}