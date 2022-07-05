using System.ComponentModel.DataAnnotations;

namespace OmegaSoftware.TestProject.BL.App.DTOs.Request
{
    public class FootballSubscriptionRequest
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string CronParams { get; set; }
        public DateTime LastRunTime { get; set; }
        public int UserName { get; set; }
    }
}
