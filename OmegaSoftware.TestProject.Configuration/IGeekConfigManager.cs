using Microsoft.Extensions.Configuration;

namespace OmegaSoftware.TestProject.Configuration
{
    public interface IGeekConfigManager
    {
        string SqLiteConnectionString { get; }
        string Secret { get; }
        string Port { get; }
        string CustomClaimId { get; }
        string CustomClaimName { get; }
        string InvalidModel { get; }
        string AdminRole { get; }
        string UserRole { get; }
        string ErrorName { get; }
        string ErrorEmail { get; }
        string ErrorLogin { get; }
        string ErrorPayload { get; }
        string JwtConfig  { get; }
        string JwtSecret { get; }
        string Cors { get; }
        string Policy { get; }
        string PolicyClaim { get; }
        string QuartzEmail { get; }
        string QuartzPassword { get; }
        string MailSmtp { get; }
        string MailSubject { get; }
        string EmailMessage { get; }
        string FileName { get; }
        string FileFormat { get; }
        string ErrorSend { get; }
        string WheatherParam1 { get; }
        string WheatherParam2 { get; }
        string JobMainParam { get; }
        string RapidApiKey { get; }
        string RapidApiGoogleUrl { get; }
        string RapidApiWheatherUrl { get; }
        string RapidApiFootballUrl { get; }
        IConfigurationSection GetConfigurationSection(string key);
    }
}
