using Microsoft.Extensions.DependencyInjection;
using OmegaSoftware.TestProject.DAL.Interfaces.Repositories;
using OmegaSoftware.TestProject.DAL.Models.DTOs;
using OmegaSoftware.TestProject.DAL.Repositories;

namespace OmegaSoftware.TestProject.DAL.Configuration
{
    public static class DependenciesConfiguration
    {
        public static IServiceCollection RegisterRepository(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            serviceCollection.AddScoped<ISubscriptionRepository<WheatherSubscription>, WheatherSubscriptionRepository>();
            serviceCollection.AddScoped<ISubscriptionRepository<FootballSubscription>, FootballSubscriptionRepository>();
            serviceCollection.AddScoped<ISubscriptionRepository<GoogleTranslateSubscription>, GoogleTranslateSubscriptionRepository>();

            return serviceCollection;
        }
    }
}
