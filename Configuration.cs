using FamilySync.Core;
using FamilySync.Core.Persistence;
using FamilySync.Registry.Persistence;
using FamilySync.Registry.Services;

namespace FamilySync.Registry;

public class Configuration : ServiceConfiguration
{
    public override void Configure(IApplicationBuilder app)
    {
    }

    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IFamilyService, FamilyService>();
        services.AddTransient<IFamilyMemberService, FamilyMemberService>();
        services.AddMySqlContext<RegistryContext>("registry", Configuration);
    }
}