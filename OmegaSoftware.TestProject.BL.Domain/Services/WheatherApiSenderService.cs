using Microsoft.Extensions.Options;
using OmegaSoftware.TestProject.BL.Domain.Configuration;
using OmegaSoftware.TestProject.BL.Domain.Interfaces.Services;
using OmegaSoftware.TestProject.Configuration;
using OmegaSoftware.TestProject.DAL.Models;
using System.Net;

namespace OmegaSoftware.TestProject.BL.Domain.Services
{
    public class WheatherApiSenderService : IApiSenderService<WheatherSubscription, string>
    {
        private readonly RapidApiConfig _rapidApiConfig;

        public WheatherApiSenderService(IOptionsMonitor<RapidApiConfig> optionsMonitor)
        {
            _rapidApiConfig = optionsMonitor.CurrentValue;
        }

        public string SendOnApi(List<string> values)
        {
            try
            {
                var webRequest = WebRequest.Create(string.Format(ApplicationConfiguration.RapidApiWheatherUrl, values[0], values[1]));

                if (webRequest != null)
                {
                    string responce;

                    webRequest.Method = ApplicationConfiguration.Metod;
                    webRequest.Timeout = ApplicationConfiguration.Timeout;
                    webRequest.Headers.Add(ApplicationConfiguration.RapidApiKeyHeader, _rapidApiConfig.RapidApiKey);
                    webRequest.Headers.Add(ApplicationConfiguration.RapidApiHostHeader, ApplicationConfiguration.RapidAPIHost);

                    using (Stream s = webRequest.GetResponse().GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(s))
                        {
                            responce = sr.ReadToEnd();
                        }
                    }

                    return responce;
                }

                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
