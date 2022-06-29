using Configuration;
using DataCollectionAndEmailMessageApplication.Web.Mapping;
using DataCollectionAndEmailMessageApplication.Web.Quartz.HostedService;
using DataCollectionAndEmailMessageApplication.Web.Quartz.Jobs;
using DataCollectionAndEmailMessageApplication.Web.Quartz.JobsFactory;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace DataCollectionAndEmailMessageApplication.Web.Configuration
{
    public static class DependenciesConfiguration
    {
        public static IServiceCollection RegisterPLMappingConfig(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddHostedService<QuartzHostedService>();
            serviceCollection.AddSingleton<IJobFactory, SingletonJobFactory>();
            serviceCollection.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            serviceCollection.AddSingleton<JobReminders>();
            serviceCollection.AddSingleton(new MyJob(type: typeof(JobReminders), expression: ApplicationConfiguration.Expression));
            serviceCollection.AddAutoMapper(
                c => c.AddProfile<MappingPLConfiguration>(),
                typeof(MappingPLConfiguration));

            return serviceCollection;
        }
    }
}
