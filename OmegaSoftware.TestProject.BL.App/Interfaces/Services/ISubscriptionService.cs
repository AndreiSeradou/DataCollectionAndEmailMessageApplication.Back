using OmegaSoftware.TestProject.BL.App.DTOs.Responce;
using OmegaSoftware.TestProject.BL.App.DTOs.Request;
using OmegaSoftware.TestProject.DAL.Models;

namespace OmegaSoftware.TestProject.BL.App.Interfaces.Services
{
    public interface ISubscriptionService
    {
        ICollection<SubscriptionResponce> GetAll(string userName);
        Task<bool> SubscribeAsync(string userName, string email, SubscriptionRequest model);
        Task<bool> UpdateAsync(string userName, string email, SubscriptionRequest model);
        bool Unsubscribe(string userName, SubscriptionRequest model);
    }
}
