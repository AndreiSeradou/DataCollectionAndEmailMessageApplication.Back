using System.ComponentModel.DataAnnotations;

namespace OmegaSoftware.TestProject.BL.Domain.Models.DTOs
{
    public class GoogleTranslateSubscriptionDTOs
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
