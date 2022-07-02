using DataCollectionAndEmailMessageApplication.DAL.Interfaces.Repositories;
using DataCollectionAndEmailMessageApplication.DAL.Models.DTOs;
using DataCollectionAndEmailMessageApplication.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DataCollectionAndEmailMessageApplication.DAL.Configuration
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
