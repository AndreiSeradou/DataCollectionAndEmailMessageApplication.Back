using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSoftware.TestProject.BL.App.DTOs.Responce
{
    public class ApiResponce
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string ApiKey { get; set; }
        public string ApiHost { get; set; }
        public string ApiKeyHeader { get; set; }
        public string ApiHostHeader { get; set; }
    }
}
