using System.ComponentModel.DataAnnotations;

namespace OmegaSoftware.TestProject.BL.App.DTOs.Request
{
    public class SubscriptionRequest
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string CronExpression { get; set; }
        public DateTime DateStart { get; set; }
        public string ApiParams { get; set; }
        public DateTime LastRunTime { get; set; }
        [Required]
        public string ApiName { get; set; }
        public string UserName { get; set; }
    }
}
