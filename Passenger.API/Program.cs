using Autofac;
using Autofac.Extensions.DependencyInjection;
using Passenger.Api;
using Passenger.Core.Domain;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);

startup.ConfigureLogging(builder.Logging);
startup.ConfigureServices(builder.Services);

// Configure Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(startup.ConfigureContainer);

var app = builder.Build();

startup.Configure(app, app.Environment);

app.MapControllers();

app.Run();

// Add partial class to reference it in integration tests.
public partial class Program { }