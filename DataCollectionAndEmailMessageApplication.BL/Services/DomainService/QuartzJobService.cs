using DataCollectionAndEmailMessageApplication.BL.Interfaces.Services;
using DataCollectionAndEmailMessageApplication.BL.Models.DTOs;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectionAndEmailMessageApplication.BL.Services.DomainService
{
    public class QuartzJobService : IQuartzJobService
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private IScheduler _scheduler { get; set; }

        public QuartzJobService(ISchedulerFactory schedulerFactory)
        {
            _schedulerFactory = schedulerFactory;
        }

        public async Task CreateJobAsync(MyJob myJob)
        {
            _scheduler = await _schedulerFactory.GetScheduler();
            // 2, открыть планировщик
            await _scheduler.Start();
            // 3, создаем триггер
            var trigger = TriggerBuilder.Create().WithIdentity($"{my.Type.FullName}.trigger").WithCronSchedule(my.Expression).WithDescription(my.Expression).Build();



            TriggerBuilder.Create()
                                                         .WithSimpleSchedule(x => x.WithIntervalInSeconds(2).RepeatForever()) // выполняется каждые две секунды
                            .Build();
            // 4. Создать задачу
            var jobDetail = JobBuilder.Create<MyJob>()
                            .WithIdentity("job", "group")
                            .Build();
            // 5, Привязать триггеры и задачи к планировщику
            await _scheduler.ScheduleJob(jobDetail, trigger);
        }

        public void DeleteJob(MyJob myJob)
        {
            throw new NotImplementedException();
        }

        public void UpdateJob(MyJob myJob)
        {
            throw new NotImplementedException();
        }
    }
}
