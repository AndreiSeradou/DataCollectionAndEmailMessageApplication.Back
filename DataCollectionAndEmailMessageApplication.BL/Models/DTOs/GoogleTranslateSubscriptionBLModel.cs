using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectionAndEmailMessageApplication.BL.Models.DTOs
{
    public class GoogleTranslateSubscriptionBLModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CronParams { get; set; }
        public DateTime LastRunTime { get; set; }
        public string UserName { get; set; }
    }
}
