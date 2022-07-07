using Microsoft.Extensions.DependencyInjection;
using OmegaSoftware.TestProject.BL.App.DTOs.Responce;
using OmegaSoftware.TestProject.BL.App.Interfaces.Services;
using OmegaSoftware.TestProject.BL.App.Mapping;
using OmegaSoftware.TestProject.BL.App.Services;

namespace OmegaSoftware.TestProject.BL.App.Configuration
{
    public static class DependenciesConfiguration
    {
        public static IServiceCollection RegisterAppService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<ISubscriptionService,SubscriptionService>();

            return serviceCollection;
        }

        public static IServiceCollection RegisterBLMappingConfig(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAutoMapper(
                c => c.AddProfile<MappingBLConfiguration>(),
                typeof(MappingBLConfiguration));

            return serviceCollection;
        }
    }
}
