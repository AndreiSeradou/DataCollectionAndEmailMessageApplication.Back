﻿
using DataCollectionAndEmailMessageApplication.BL.Interfaces.Services;
using DataCollectionAndEmailMessageApplication.BL.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectionAndEmailMessageApplication.BL.Services.DomainService
{
    public class WheatherApiSenderService : IApiSenderService<WheatherSubscriptionBLModel, string>
    {
        public string SendOnApi(List<string> values)
        {
            try
            {
                var webRequest = WebRequest.Create($"https://weatherapi-com.p.rapidapi.com/future.json?q={values[0]}&dt={values[1]}");

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
