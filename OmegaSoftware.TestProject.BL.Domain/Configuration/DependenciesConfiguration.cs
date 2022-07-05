using Microsoft.Extensions.DependencyInjection;
using OmegaSoftware.TestProject.BL.Domain.Interfaces.Services;
using OmegaSoftware.TestProject.BL.Domain.Services;
using OmegaSoftware.TestProject.DAL.Models;
using Quartz;
using Quartz.Impl;

namespace OmegaSoftware.TestProject.BL.Domain.Configuration
{
    public static class DependenciesConfiguration
    {
        public static IServiceCollection RegisterDomainService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IEmailSenderService, EmailSenderService>();
            serviceCollection.AddScoped<IApiSenderService<WheatherSubscription, string>, WheatherApiSenderService>();
            serviceCollection.AddScoped<IApiSenderService<GoogleTranslateSubscription, string>, GoogleTranslateApiSenderService>();
            serviceCollection.AddScoped<IApiSenderService<FootballSubscription, string>, FootballApiSenderService>();
            serviceCollection.AddScoped<IQuartzJobService<WheatherSubscription>, QuartzWheatherJobService>();
            serviceCollection.AddScoped<IQuartzJobService<FootballSubscription>, QuartzFootballJobService>();
            serviceCollection.AddScoped<IQuartzJobService<GoogleTranslateSubscription>, QuartzGoogleTranslateJobService>();
            serviceCollection.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            return serviceCollection;
        }   
    }
}
