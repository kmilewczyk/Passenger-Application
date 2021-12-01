using Autofac;
using Microsoft.Extensions.Configuration;
using Passenger.Infrastructure.IoC.Modules;
using Passenger.Infrastructure.Settings;

namespace Passenger.Infrastructure.IoC;

public class ContainerModule : Autofac.Module
{
    private readonly IConfiguration _configuration;

    public ContainerModule(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterModule<CommandModule>();
        builder.RegisterModule(new SettingModule(_configuration));
        builder.RegisterModule<RepositoryModule>();
        // Overwrites RepositoryModule
        builder.RegisterModule<MongoModule>();
        // Overwrites MongoModule
        builder.RegisterModule<SqlModule>();
        builder.RegisterModule<ServiceModule>();
    }
}