namespace OmegaSoftware.TestProject.DAL.Models.DTOs
{
    public class WheatherSubscription
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CronParams { get; set; }
        public string City { get; set; }
        public string Date { get; set; }
        public DateTime LastRunTime { get; set; }
        public string UserName { get; set; }
    }
}
