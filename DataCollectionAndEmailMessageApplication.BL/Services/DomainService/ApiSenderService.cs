
using DataCollectionAndEmailMessageApplication.BL.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectionAndEmailMessageApplication.BL.Services.DomainService
{
    public class ApiSenderService : IApiSenderService
    {
        public string SendOnFootballApi()
        {
            try
            {
                var webRequest = WebRequest.Create("https://api-football-v1.p.rapidapi.com/v3/leagues");

                if (webRequest != null)
                {
                    webRequest.Method = "GET";
                    webRequest.Timeout = 12000;
                    webRequest.Headers.Add("X-RapidAPI-Key", "SIGN-UP-FOR-KEY");
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

        public string SendOnGoogleTranslateApi()
        {
            try
            {
                var webRequest = WebRequest.Create("https://google-translate1.p.rapidapi.com/language/translate/v2/languages");

                if (webRequest != null)
                {
                    webRequest.Method = "GET";
                    webRequest.Timeout = 12000;
                    webRequest.ContentType = "application/json";
                    webRequest.Headers.Add("Accept-Encoding", "application/gzip");
                    webRequest.Headers.Add("X-RapidAPI-Key", "SIGN-UP-FOR-KEY");
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

        public string SendOnWheatherApi(string city, string date)
        {
            try
            {
                var webRequest = WebRequest.Create($"https://weatherapi-com.p.rapidapi.com/future.json?q={city}&dt={date}");

                if (webRequest != null)
                {
                    webRequest.Method = "GET";
                    webRequest.Timeout = 12000;
                    webRequest.ContentType = "application/json";
                    webRequest.Headers.Add("X-RapidAPI-Key", "SIGN-UP-FOR-KEY");
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
