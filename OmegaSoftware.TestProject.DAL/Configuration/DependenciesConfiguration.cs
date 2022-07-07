using OmegaSoftware.TestProject.DAL.Interfaces.Repositories;
using OmegaSoftware.TestProject.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace OmegaSoftware.TestProject.DAL.Configuration
{
    public static class DependenciesConfiguration
    {
        public static IServiceCollection RegisterRepository(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            serviceCollection.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            serviceCollection.AddScoped<IApiRepository, ApiRepository>();

            return serviceCollection;
        }
    }
}
