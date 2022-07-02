namespace DataCollectionAndEmailMessageApplication.Web.Models.DTOs
{
    public class AuthResponce
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
