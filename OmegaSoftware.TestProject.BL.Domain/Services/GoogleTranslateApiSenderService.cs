using Microsoft.Extensions.Options;
using OmegaSoftware.TestProject.BL.Domain.Configuration;
using OmegaSoftware.TestProject.BL.Domain.Interfaces.Services;
using OmegaSoftware.TestProject.Configuration;
using OmegaSoftware.TestProject.DAL.Models;
using System.Net;

namespace OmegaSoftware.TestProject.BL.Domain.Services
{
    public class GoogleTranslateApiSenderService : IApiSenderService<GoogleTranslateSubscription, string>
    {
        private readonly RapidApiConfig _rapidApiConfig;

        public GoogleTranslateApiSenderService(IOptionsMonitor<RapidApiConfig> optionsMonitor)
        {
            _rapidApiConfig = optionsMonitor.CurrentValue;
        }

        public string SendOnApi(List<string> values)
        {
            try
            {
                var webRequest = WebRequest.Create(ApplicationConfiguration.RapidApiGoogleUrl);

                if (webRequest != null)
                {
                    string responce;

                    webRequest.Method = ApplicationConfiguration.Metod;
                    webRequest.Timeout = ApplicationConfiguration.Timeout;
                    webRequest.ContentType = ApplicationConfiguration.ContentType;
                    webRequest.Headers.Add(ApplicationConfiguration.AcceptEncodingHeader, ApplicationConfiguration.AcceptEncoding);
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
