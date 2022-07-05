﻿using OmegaSoftware.TestProject.BL.Domain.Interfaces.Services;
using OmegaSoftware.TestProject.Configuration;
using OmegaSoftware.TestProject.DAL.Models;
using System.Net;

namespace OmegaSoftware.TestProject.BL.Domain.Services
{
    public class WheatherApiSenderService : IApiSenderService<WheatherSubscription, string>
    {
        public string SendOnApi(List<string> values)
        {
            try
            {
                var webRequest = WebRequest.Create(string.Format(ApplicationConfiguration.RapidApiWhetherUrl, values[0], values[1]));

                if (webRequest != null)
                {
                    string responce;

                    webRequest.Method = "GET";
                    webRequest.Timeout = 12000;
                    webRequest.ContentType = "application/json";
                    webRequest.Headers.Add("X-RapidAPI-Key", ApplicationConfiguration.RapidApiKey);
                    webRequest.Headers.Add("X-RapidAPI-Host", "weatherapi-com.p.rapidapi.com");

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
