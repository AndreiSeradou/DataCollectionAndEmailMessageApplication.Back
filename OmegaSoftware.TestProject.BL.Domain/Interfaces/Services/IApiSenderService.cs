using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSoftware.TestProject.BL.Domain.Interfaces.Services
{
    public interface IApiSenderService
    {
        string SendOnApi(string url, string apiParams, string apiKeyHeader, string apiKey, string apiHostHeader, string apiHost);
    }
}
        