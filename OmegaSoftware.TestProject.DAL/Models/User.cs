using System.Data.Linq.Mapping;

namespace OmegaSoftware.TestProject.DAL.Models
{
    [Table(Name = "Users")]
    public class User
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column(Name = "Name")]
        public string Name { get; set; }
        [Column(Name = "Email")]
        public string Email { get; set; }
        [Column(Name = "Password")]
        public string Password { get; set; }
        [Column(Name = "Role")]
        public string Role { get; set; }
    }
}
