using Microsoft.Extensions.DependencyInjection;
using OmegaSoftware.TestProject.BL.Domain.Interfaces.Services;
using OmegaSoftware.TestProject.BL.Domain.Mapping;
using OmegaSoftware.TestProject.BL.Domain.Models.DTOs;
using OmegaSoftware.TestProject.BL.Domain.Services;
using Quartz;
using Quartz.Impl;

namespace OmegaSoftware.TestProject.BL.Domain.Configuration
{
    public static class DependenciesConfiguration
    {
        public static IServiceCollection RegisterDomainService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IEmailSenderService, EmailSenderService>();
            serviceCollection.AddScoped<IApiSenderService<WheatherSubscriptionDTOs, string>, WheatherApiSenderService>();
            serviceCollection.AddScoped<IApiSenderService<GoogleTranslateSubscriptionDTOs, string>, GoogleTranslateApiSenderService>();
            serviceCollection.AddScoped<IApiSenderService<FootballSubscriptionDTOs, string>, FootballApiSenderService>();
            serviceCollection.AddScoped<IQuartzJobService<WheatherSubscriptionDTOs>, QuartzWheatherJobService>();
            serviceCollection.AddScoped<IQuartzJobService<FootballSubscriptionDTOs>, QuartzFootballJobService>();
            serviceCollection.AddScoped<IQuartzJobService<GoogleTranslateSubscriptionDTOs>, QuartzGoogleTranslateJobService>();
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
