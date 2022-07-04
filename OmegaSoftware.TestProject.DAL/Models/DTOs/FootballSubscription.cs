namespace OmegaSoftware.TestProject.DAL.Models.DTOs
{
    public class FootballSubscription
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CronParams { get; set; }
        public DateTime LastRunTime { get; set; }
        public string UserName { get; set; }
    }
}
