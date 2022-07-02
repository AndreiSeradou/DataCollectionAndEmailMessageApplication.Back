using DataCollectionAndEmailMessageApplication.BL.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectionAndEmailMessageApplication.BL.Models.DTOs
{
    public class WheatherSubscriptionBLModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CronParams { get; set; }
        public string City { get; set; }
        public string Date { get; set; }
        public string UserName { get; set; }
    }
}
