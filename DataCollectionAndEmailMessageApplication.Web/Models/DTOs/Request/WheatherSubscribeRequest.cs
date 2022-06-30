namespace DataCollectionAndEmailMessageApplication.Web.Models.DTOs.Request
{
    public class WheatherSubscribeRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CronParams { get; set; }
        public string City { get; set; }
        public string Date { get; set; }
        public int UserId { get; set; }
    }
}
