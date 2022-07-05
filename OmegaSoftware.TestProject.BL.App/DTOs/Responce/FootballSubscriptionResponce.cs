namespace OmegaSoftware.TestProject.BL.App.DTOs.Responce
{
    public class FootballSubscriptionResponce
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CronParams { get; set; }
        public DateTime LastRunTime { get; set; }
        public int UserName { get; set; }
    }
}
