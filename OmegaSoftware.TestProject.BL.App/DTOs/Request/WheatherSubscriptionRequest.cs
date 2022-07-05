using System.ComponentModel.DataAnnotations;

namespace OmegaSoftware.TestProject.BL.App.DTOs.Request
{
    public class WheatherSubscriptionRequest
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string CronParams { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Date { get; set; }
        public DateTime LastRunTime { get; set; }
        public string UserName { get; set; }
    }
}
