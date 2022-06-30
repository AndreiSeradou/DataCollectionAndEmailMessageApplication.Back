using DataCollectionAndEmailMessageApplication.BL.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectionAndEmailMessageApplication.BL.Interfaces.Services
{
    public interface IQuartzJobService
    {
        Task CreateJobAsync(MyJob myJob);
        void UpdateJob(MyJob myJob);
        void DeleteJob(MyJob myJob);
    }
}
