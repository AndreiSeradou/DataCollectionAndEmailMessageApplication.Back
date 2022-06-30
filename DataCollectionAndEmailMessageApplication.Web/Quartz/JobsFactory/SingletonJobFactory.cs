using Quartz;
using Quartz.Spi;

namespace DataCollectionAndEmailMessageApplication.Web.Quartz.JobsFactory
{
    public class SingletonJobFactory 
    {
        private readonly IServiceProvider _serviceProvider;

        public SingletonJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

       

        
    }
}
