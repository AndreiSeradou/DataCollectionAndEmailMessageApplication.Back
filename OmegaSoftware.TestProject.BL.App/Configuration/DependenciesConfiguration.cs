using Microsoft.Extensions.DependencyInjection;
using OmegaSoftware.TestProject.BL.App.Interfaces.Services;
using OmegaSoftware.TestProject.BL.App.Services.AppService;
using OmegaSoftware.TestProject.BL.Domain.Models.DTOs;

namespace OmegaSoftware.TestProject.BL.App.Configuration
{
    public static class DependenciesConfiguration
    {
        public static IServiceCollection RegisterAppService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<ISubscriptionService<WheatherSubscriptionBLModel>, WheatherSubscriptionService>();
            serviceCollection.AddScoped<ISubscriptionService<FootballSubscriptionBLModel>, FootballSubscriptionService>();
            serviceCollection.AddScoped<ISubscriptionService<GoogleTranslateSubscriptionBLModel>, GoogleTranslateSubscriptionService>();

            return serviceCollection;
        }
    }
}
