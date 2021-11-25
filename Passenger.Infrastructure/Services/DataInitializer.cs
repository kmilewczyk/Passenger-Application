using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using Passenger.Core.Domain;

namespace Passenger.Infrastructure.Services;

public class DataInitializer : IDataInitializer
{
    private readonly IDriverService _driverService;
    private readonly IUserService _userService;
    private readonly ILogger<DataInitializer> _logger;
    private static readonly object InitializerInvokedLock = new object();
    private static bool InitializerInvoked { get; set; } = false;

    public DataInitializer(IDriverService driverService, IUserService userService, ILogger<DataInitializer> logger)
    {
        _driverService = driverService;
        _userService = userService;
        _logger = logger;
    }

    public async Task SeedAsync()
    {
        // This is a result of a fun fact that tests recreate webapplication, but the datastore is static inside Userrepository.
        // Below is just a ducktape to remedy this.
        lock (InitializerInvokedLock)
        {
            if (InitializerInvoked)
            {
                return;
            }

            InitializerInvoked = true;
        }
        
        _logger.LogTrace("Initializing data...");

        var tasks = new List<Task>();
        for (int i = 1; i <= 10; i++)
        {
            var userId = Guid.NewGuid();
            var username = $"user{i}";
            tasks.Add(_userService.RegisterAsync(userId, $"{username}@email.com", username, "secret", UserRole.User));
            tasks.Add(_driverService.CreateAsync(userId));
            tasks.Add(_driverService.SetVehicle(userId, "BMW", "i8", 5));
        }

        for (int i = 1; i <= 3; i++)
        {
            var adminId = Guid.NewGuid();
            var username = $"admin{i}";
            tasks.Add(_userService.RegisterAsync(adminId, $"{username}@email.com", username, "secret", UserRole.Admin));
        }

        await Task.WhenAll(tasks);

        _logger.LogTrace("Data was initialized.");
    }
}