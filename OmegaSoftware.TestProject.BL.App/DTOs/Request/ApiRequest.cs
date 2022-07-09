using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSoftware.TestProject.BL.App.DTOs.Request
{
    public class ApiRequest
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public string ApiKey { get; set; }
        [Required]
        public string ApiHost { get; set; }
        [Required]
        public string ApiKeyHeader { get; set; }
        [Required]
        public string ApiHostHeader { get; set; }
    }
}
