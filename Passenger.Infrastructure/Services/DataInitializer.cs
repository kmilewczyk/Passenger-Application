using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using Passenger.Core.Domain;

namespace Passenger.Infrastructure.Services;

public class DataInitializer : IDataInitializer
{
    private readonly IDriverService _driverService;
    private readonly IUserService _userService;
    private readonly IDriverRouteService _driverRouteService;
    private readonly ILogger<DataInitializer> _logger;
    private static readonly object InitializerInvokedLock = new object();
    private static bool InitializerInvoked { get; set; } = false;

    public DataInitializer(IDriverService driverService, IUserService userService,
        IDriverRouteService driverRouteService, ILogger<DataInitializer> logger)
    {
        _driverService = driverService;
        _userService = userService;
        _driverRouteService = driverRouteService;
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

        var users = await _userService.GetAll();
        if (users.Any())
        {
            return;
        }

        _logger.LogTrace("Initializing data...");

        var tasks = new List<Task>();
        for (int i = 1; i <= 10; i++)
        {
            var userId = Guid.NewGuid();
            var username = $"user{i}";
            
            _logger.LogTrace($"Adding user: {username}");
            await _userService.RegisterAsync(userId, $"{username}@email.com", username, "secret", UserRole.User);
            
            _logger.LogTrace($"Adding driver for user {username}");
            await _driverService.CreateAsync(userId);
            await _driverService.SetVehicle(userId, "BMW", "i8");
            
            _logger.LogTrace($"Adding route for: {username}");
            await _driverRouteService.AddAsync(userId, "Default Route", 1, 1, 2, 2);
            await _driverRouteService.AddAsync(userId, "Job Route", 3, 4, 7, 8);
        }

        for (int i = 1; i <= 3; i++)
        {
            var adminId = Guid.NewGuid();
            var username = $"admin{i}";
            _logger.LogTrace($"Adding admin: {username}");
            tasks.Add(_userService.RegisterAsync(adminId, $"{username}@email.com", username, "secret", UserRole.Admin));
        }

        await Task.WhenAll(tasks);

        _logger.LogTrace("Data was initialized.");
    }
}