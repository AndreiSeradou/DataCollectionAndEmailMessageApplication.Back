using Microsoft.Extensions.DependencyInjection;
using OmegaSoftware.TestProject.BL.App.Interfaces.Services;
using OmegaSoftware.TestProject.BL.App.Services;
using OmegaSoftware.TestProject.BL.Domain.Models.DTOs;

namespace OmegaSoftware.TestProject.BL.App.Configuration
{
    public static class DependenciesConfiguration
    {
        public static IServiceCollection RegisterAppService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<ISubscriptionService<WheatherSubscriptionDTOs>, WheatherSubscriptionService>();
            serviceCollection.AddScoped<ISubscriptionService<FootballSubscriptionDTOs>, FootballSubscriptionService>();
            serviceCollection.AddScoped<ISubscriptionService<GoogleTranslateSubscriptionDTOs>, GoogleTranslateSubscriptionService>();

            return serviceCollection;
        }
    }
}
