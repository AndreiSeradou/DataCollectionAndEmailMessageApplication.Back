﻿using Microsoft.Extensions.Options;
using OmegaSoftware.TestProject.BL.Domain.Configuration;
using OmegaSoftware.TestProject.BL.Domain.Interfaces.Services;
using OmegaSoftware.TestProject.Configuration;
using OmegaSoftware.TestProject.DAL.Models;
using System.Net;

namespace OmegaSoftware.TestProject.BL.Domain.Services
{
    public class FootballApiSenderService : IApiSenderService<FootballSubscription, string>
    {
        private readonly RapidApiConfig _rapidApiConfig;

        public FootballApiSenderService(IOptionsMonitor<RapidApiConfig> optionsMonitor)
        {
            _rapidApiConfig = optionsMonitor.CurrentValue;
        }

        public string SendOnApi(List<string> values)
        {
            try
            {
                var webRequest = WebRequest.Create(ApplicationConfiguration.RapidApiFootballUrl);

                if (webRequest != null)
                {
                    string responce;

                    webRequest.Method = "GET";
                    webRequest.Timeout = 12000;
                    webRequest.Headers.Add("X-RapidAPI-Key", _rapidApiConfig.RapidApiKey);
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
