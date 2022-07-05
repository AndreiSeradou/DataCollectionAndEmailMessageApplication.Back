using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSoftware.TestProject.Configuration
{
    public class GeekConfigManager : IGeekConfigManager
    {
        private readonly IConfiguration _configuration;
        public GeekConfigManager(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public string SqLiteConnectionString
        {
            get
            {
                return this._configuration["ConnectionStrings:SqLiteConnectionString"];
            }
        }

        public string QuartzPassword
        {
            get
            {
                return this._configuration["AppSeettings:QuartzPassword"];
            }
        }   

        public string RapidApiKey
        {
            get
            {
                return this._configuration["AppSeettings:RapidApiKey"];
            }
        }

        public IConfigurationSection GetConfigurationSection(string key)
        {
            return this._configuration.GetSection(key);
        }
    }
}
