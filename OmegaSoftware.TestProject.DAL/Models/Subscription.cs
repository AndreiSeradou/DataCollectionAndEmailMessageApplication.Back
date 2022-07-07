using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSoftware.TestProject.DAL.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CronExpression { get; set; }
        public DateTime DateStart { get; set; }
        public string ApiParams { get; set; }
        public DateTime LastRunTime { get; set; }
        public string ApiName { get; set; }
        public string UserName { get; set; }
    }
}
