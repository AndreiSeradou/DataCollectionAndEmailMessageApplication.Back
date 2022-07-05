using Microsoft.Extensions.Configuration;

namespace OmegaSoftware.TestProject.Configuration
{
    public interface IGeekConfigManager
    {
        string SqLiteConnectionString { get; }
        string QuartzPassword { get; }
        string RapidApiKey { get; }

        IConfigurationSection GetConfigurationSection(string key);
    }
}
