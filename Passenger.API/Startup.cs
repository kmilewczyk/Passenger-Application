using System.Net.Mime;
using System.Text;
using Autofac;
using Autofac.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog.Web;
using Passenger.Api.Extensions;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.Extensions;
using Passenger.Infrastructure.IoC;
using Passenger.Infrastructure.IoC.Modules;
using Passenger.Infrastructure.Mappers;
using Passenger.Infrastructure.Repositories;
using Passenger.Infrastructure.Services;
using Passenger.Infrastructure.Settings;

namespace Passenger.Api;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureLogging(ILoggingBuilder logging)
    {
        logging.ClearProviders();
        logging.AddNLog("nlog.config");
        logging.AddNLogWeb();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddMemoryCache();

        services.AddSingleton(AutoMapperConfig.Initialize());

        services.AddAuthorization(x => x.AddPolicy("admin", p => p.RequireRole("admin")));
        AddAuthentication(services, Configuration);

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    private static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSettings<JwtSettings>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidIssuer = jwtSettings.ValidIssuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.IssuerSigningKey)),
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidateIssuer = true,
            };
        });
    }

    public void ConfigureContainer(ContainerBuilder builder)
    {
        builder.RegisterModule(new ContainerModule(Configuration));
    }

    /// <summary>
    /// Configure WebApplication after building
    /// </summary>
    /// <param name="app">WebApplication passed after build.</param>
    /// <param name="environment">WebApplication Environment</param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            app.UseHsts();
        }
        
        var generalSettings = Configuration.GetSettings<GeneralSettings>();
        if (generalSettings.SeedData)
        {
            app.ApplicationServices.GetService<IDataInitializer>()!.SeedAsync().Wait();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        // app.UseRouting(); // It's not necessary per MS Docs. Also there is some conflict with UseAuthentication.
        
        app.UseApiExceptionHandler();

        app.UseAuthentication();
        app.UseAuthorization();

        // app.UseEndpoints();
    }
}