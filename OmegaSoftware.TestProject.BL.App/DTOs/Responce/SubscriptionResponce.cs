namespace OmegaSoftware.TestProject.BL.App.DTOs.Responce
{
    public class SubscriptionResponce
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CronExpression { get; set; }
        public DateTime DateStart { get; set; }
        public string ApiParams { get; set; }
        public DateTime LastRunTime { get; set; }
        public string ApiName { get; set; }
        public string UserName { get; set; }
    }
}
