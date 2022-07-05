using System.Data.Linq.Mapping;

namespace OmegaSoftware.TestProject.DAL.Models
{
    [Table(Name = "GoogleTranslateSubscriptions")]
    public class GoogleTranslateSubscription
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column(Name = "Name")]
        public string Name { get; set; }
        [Column(Name = "Description")]
        public string Description { get; set; }
        [Column(Name = "CronParams")]
        public string CronParams { get; set; }
        [Column(Name = "LastRunTime")]
        public DateTime LastRunTime { get; set; }
        [Column(Name = "UserName")]
        public string UserName { get; set; }
    }
}
