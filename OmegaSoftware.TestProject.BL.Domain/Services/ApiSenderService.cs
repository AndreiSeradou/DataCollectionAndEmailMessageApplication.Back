using Microsoft.Extensions.Options;
using OmegaSoftware.TestProject.BL.Domain.Configuration;
using OmegaSoftware.TestProject.BL.Domain.Interfaces.Services;
using OmegaSoftware.TestProject.Configuration;
using System.Net;

namespace OmegaSoftware.TestProject.BL.Domain.Services
{
    public class ApiSenderService : IApiSenderService
    {
        public string SendOnApi(string url, string apiParams, string apiKeyHeader, string apiKey, string apiHostHeader, string apiHost)
        {
            try
            {
                var webRequest = WebRequest.Create(url + apiParams);

                if (webRequest != null)
                {
                    string responce;

                    webRequest.Method = ApplicationConfiguration.Metod;
                    webRequest.Timeout = ApplicationConfiguration.Timeout;
                    webRequest.Headers.Add(apiKeyHeader, apiKey);
                    webRequest.Headers.Add(apiHostHeader, apiHost);

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
