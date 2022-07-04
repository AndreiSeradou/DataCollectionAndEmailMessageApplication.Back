using System.ComponentModel.DataAnnotations;

namespace OmegaSoftware.TestProject.Web.Models.DTOs
{
    public class WheatherSubscriptionPLModel
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
