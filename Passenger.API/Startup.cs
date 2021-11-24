using System.Net.Mime;
using Autofac;
using Autofac.Core;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.IoC;
using Passenger.Infrastructure.IoC.Modules;
using Passenger.Infrastructure.Mappers;
using Passenger.Infrastructure.Repositories;
using Passenger.Infrastructure.Services;

namespace Passenger.Api;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddSingleton(AutoMapperConfig.Initialize());

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
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
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.UseStaticFiles();
        app.UseRouting();

        // app.UseEndpoints()
    }
}