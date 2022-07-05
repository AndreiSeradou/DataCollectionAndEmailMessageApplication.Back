using System.ComponentModel.DataAnnotations;

namespace OmegaSoftware.TestProject.BL.App.DTOs.Request
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
