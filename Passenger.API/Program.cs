using System.Net.Mime;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Passenger.Api;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.Extensions;
using Passenger.Infrastructure.Mappers;
using Passenger.Infrastructure.Repositories;
using Passenger.Infrastructure.Services;
using Passenger.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);

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