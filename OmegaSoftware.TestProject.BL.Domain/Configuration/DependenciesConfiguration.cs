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
            serviceCollection.AddSingleton<IEmailSenderService, EmailSenderService>();
            serviceCollection.AddScoped<IApiSenderService, ApiSenderService>();
            serviceCollection.AddSingleton<IQuartzJobService, QuartzJobService>();
            serviceCollection.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            return serviceCollection;
        }   
    }
}
