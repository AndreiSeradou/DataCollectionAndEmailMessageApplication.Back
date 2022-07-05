using System.ComponentModel.DataAnnotations;

namespace OmegaSoftware.TestProject.Web.Models
{
    public class UserRegistrationRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
