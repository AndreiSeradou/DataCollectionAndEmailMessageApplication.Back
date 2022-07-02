namespace DataCollectionAndEmailMessageApplication.Web.Models.DTOs
{
    public class GoogleTranslateSubscriptionPLModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CronParams { get; set; }
        public int UserName { get; set; }
    }
}
