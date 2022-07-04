using OmegaSoftware.TestProject.BL.Domain.Interfaces.Services;
using OmegaSoftware.TestProject.BL.Domain.Models.DTOs;
using OmegaSoftware.TestProject.Configuration;
using System.Net;

namespace OmegaSoftware.TestProject.BL.Domain.Services.DomainService
{
    public class GoogleTranslateApiSenderService : IApiSenderService<GoogleTranslateSubscriptionBLModel, string>
    {
        public string SendOnApi(List<string> values)
        {
            try
            {
                var webRequest = WebRequest.Create(ApplicationConfiguration.RapidApiGoogleUrl);

                if (webRequest != null)
                {
                    webRequest.Method = "GET";
                    webRequest.Timeout = 12000;
                    webRequest.ContentType = "application/json";
                    webRequest.Headers.Add("Accept-Encoding", "application/gzip");
                    webRequest.Headers.Add("X-RapidAPI-Key", ApplicationConfiguration.RapidApiKey);
                    webRequest.Headers.Add("X-RapidAPI-Host", "weatherapi-com.p.rapidapi.com");

                    using (Stream s = webRequest.GetResponse().GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(s))
                        {
                            var responce = sr.ReadToEnd();

                            return responce;
                        }
                    }
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
