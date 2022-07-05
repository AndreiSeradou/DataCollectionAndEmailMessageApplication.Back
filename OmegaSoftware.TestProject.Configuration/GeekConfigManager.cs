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

        public string Secret
        {
            get
            {
                return this._configuration["JwtConfig:Secret"];
            }
        }

        public string Port
        {
            get
            {
                return this._configuration["AppSeettings:Port"];
            }
        }

        public string CustomClaimId
        {
            get
            {
                return this._configuration["AppSeettings:CustomClaimId"];
            }
        }

        public string CustomClaimName
        {
            get
            {
                return this._configuration["AppSeettings:CustomClaimName"];
            }
        }

        public string InvalidModel
        {
            get
            {
                return this._configuration["AppSeettings:InvalidModel"];
            }
        }

        public string AdminRole
        {
            get
            {
                return this._configuration["AppSeettings:EmailID"];
            }
        }

        public string UserRole
        {
            get
            {
                return this._configuration["AppSeettings:UserRole"];
            }
        }

        public string ErrorName
        {
            get
            {
                return this._configuration["AppSeettings:ErrorName"];
            }
        }

        public string ErrorEmail
        {
            get
            {
                return this._configuration["AppSeettings:ErrorEmail"];
            }
        }

        public string ErrorLogin
        {
            get
            {
                return this._configuration["AppSeettings:ErrorLogin"];
            }
        }

        public string ErrorPayload
        {
            get
            {
                return this._configuration["AppSeettings:ErrorPayload"];
            }
        }

        public string JwtConfig
        {
            get
            {
                return this._configuration["AppSeettings:JwtConfig"];
            }
        }

        public string JwtSecret
        {
            get
            {
                return this._configuration["AppSeettings:JwtSecret"];
            }
        }

        public string Cors
        {
            get
            {
                return this._configuration["AppSeettings:Cors"];
            }
        }

        public string Policy
        {
            get
            {
                return this._configuration["AppSeettings:Policy"];
            }
        }

        public string PolicyClaim
        {
            get
            {
                return this._configuration["AppSeettings:PolicyClaim"];
            }
        }

        public string QuartzEmail
        {
            get
            {
                return this._configuration["AppSeettings:QuartzEmail"];
            }
        }

        public string QuartzPassword
        {
            get
            {
                return this._configuration["AppSeettings:QuartzPassword"];
            }
        }

        public string MailSmtp
        {
            get
            {
                return this._configuration["AppSeettings:MailSmtp"];
            }
        }

        public string MailSubject
        {
            get
            {
                return this._configuration["AppSeettings:MailSubject"];
            }
        }

        public string EmailMessage
        {
            get
            {
                return this._configuration["AppSeettings:EmailMessage"];
            }
        }

        public string FileName
        {
            get
            {
                return this._configuration["AppSeettings:FileName"];
            }
        }

        public string FileFormat
        {
            get
            {
                return this._configuration["AppSeettings:FileFormat"];
            }
        }

        public string ErrorSend
        {
            get
            {
                return this._configuration["AppSeettings:ErrorSend"];
            }
        }

        public string WheatherParam1
        {
            get
            {
                return this._configuration["AppSeettings:WheatherParam1"];
            }
        }

        public string WheatherParam2
        {
            get
            {
                return this._configuration["AppSeettings:WheatherParam2"];
            }
        }

        public string JobMainParam
        {
            get
            {
                return this._configuration["AppSeettings:JobMainParam"];
            }
        }

        public string RapidApiKey
        {
            get
            {
                return this._configuration["AppSeettings:RapidApiKey"];
            }
        }

        public string RapidApiGoogleUrl
        {
            get
            {
                return this._configuration["AppSeettings:RapidApiGoogleUrl"];
            }
        }

        public string RapidApiWheatherUrl
        {
            get
            {
                return this._configuration["AppSeettings:RapidApiWheatherUrl"];
            }
        }

        public string RapidApiFootballUrl
        {
            get
            {
                return this._configuration["AppSeettings:RapidApiFootballUrl"];
            }
        }

        public IConfigurationSection GetConfigurationSection(string key)
        {
            return this._configuration.GetSection(key);
        }
    }
}
