using System.ComponentModel.DataAnnotations;

namespace DataCollectionAndEmailMessageApplication.Web.Models.DTOs
{
    public class UserLoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
