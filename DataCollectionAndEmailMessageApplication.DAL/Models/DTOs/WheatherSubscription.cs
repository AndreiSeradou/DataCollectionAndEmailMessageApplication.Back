using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectionAndEmailMessageApplication.DAL.Models.DTOs
{
    public class WheatherSubscription
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CronParams { get; set; }
        public string Param1 { get; set; }
        public string Param2 { get; set; }
        public int UserId { get; set; }
    }
}
