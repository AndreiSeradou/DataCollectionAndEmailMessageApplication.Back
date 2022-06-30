using DataCollectionAndEmailMessageApplication.Web.Mapping;
using Quartz;
using Quartz.Impl;

namespace DataCollectionAndEmailMessageApplication.Web.Configuration
{
    public static class DependenciesConfiguration
    {
        public static IServiceCollection RegisterPLConfig(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            return serviceCollection;
        }

        public static IServiceCollection RegisterPLMappingConfig(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAutoMapper(
                c => c.AddProfile<MappingPLConfiguration>(),
                typeof(MappingPLConfiguration));

            return serviceCollection;
        }
    }
}
