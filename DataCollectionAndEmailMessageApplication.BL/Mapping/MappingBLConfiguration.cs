using AutoMapper;
using DataCollectionAndEmailMessageApplication.BL.Models.DTOs;
using DataCollectionAndEmailMessageApplication.DAL.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectionAndEmailMessageApplication.BL.Mapping
{
    public class MappingBLConfiguration : Profile
    {
        public MappingBLConfiguration()
        {
            CreateMap<WheatherSubscriptionBLModel, WheatherSubscription>().ReverseMap();
        }
    }
}
