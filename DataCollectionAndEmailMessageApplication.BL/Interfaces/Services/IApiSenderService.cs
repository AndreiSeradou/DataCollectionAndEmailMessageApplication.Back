using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectionAndEmailMessageApplication.BL.Interfaces.Services
{
    public interface IApiSenderService
    {
        string SendOnWheatherApi(string city, string date);
        string SendOnGoogleTranslateApi();
        string SendOnFootballApi();
    }
}
        