using DataCollectionAndEmailMessageApplication.BL.Interfaces.Services;
using DataCollectionAndEmailMessageApplication.BL.Mapping;
using DataCollectionAndEmailMessageApplication.BL.Models.DTOs;
using DataCollectionAndEmailMessageApplication.BL.Services.AppService;
using DataCollectionAndEmailMessageApplication.BL.Services.DomainService;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;

namespace DataCollectionAndEmailMessageApplication.BL.Configuration
{
    public static class DependenciesConfiguration
    {
        public static IServiceCollection RegisterService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IEmailSenderService, EmailSenderService>();
            serviceCollection.AddScoped<IApiSenderService, ApiSenderService>();
            serviceCollection.AddScoped<IQuartzJobService, QuartzJobService>();
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<ISubscriptionService<WheatherSubscriptionBLModel>, WheatherSubscriptionService>();
            serviceCollection.AddScoped<ISubscriptionService<FootballSubscriptionBLModel>, FootballSubscriptionService>();
            serviceCollection.AddScoped<ISubscriptionService<GoogleTranslateSubscriptionBLModel>, GoogleTranslateSubscriptionService>();
            serviceCollection.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

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
