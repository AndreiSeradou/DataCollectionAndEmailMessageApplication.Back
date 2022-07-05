using System.ComponentModel.DataAnnotations;

namespace OmegaSoftware.TestProject.BL.App.DTOs.Request
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
