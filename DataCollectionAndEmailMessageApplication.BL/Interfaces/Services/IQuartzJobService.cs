using DataCollectionAndEmailMessageApplication.BL.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectionAndEmailMessageApplication.BL.Interfaces.Services
{
    public interface IQuartzJobService<T> where T : class
    {
        Task CreateJobAsync(string email, T model);
        Task UpdateJobAsync(string email, T model);
        void DeleteJob(T model);
    }
}
