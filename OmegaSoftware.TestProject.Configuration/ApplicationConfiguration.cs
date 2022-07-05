﻿
namespace OmegaSoftware.TestProject.Configuration
{
    public static class ApplicationConfiguration
    {
        public const int Port = 25;
        public const string CustomClaimId = "Id";
        public const string CustomClaimName = "Name";
        public const string InvalidModel = "Invalid";
        public const string AdminRole = "Admin";
        public const string UserRole = "User";
        public const string ErrorName = "Name already in use";
        public const string ErrorEmail = "Email already in use";
        public const string ErrorLogin = "Invalid login request";
        public const string ErrorPayload = "Invalid payload";
        public const string JwtConfig = "JwtConfig";
        public const string JwtSecret = "JwtConfig:Secret";
        public const string Cors = "Open";
        public const string Policy = "DepartmentPolicy";
        public const string PolicyClaim = "department";
        public const string QuartzEmail = "andrey03072000@gmail.com";
        public const string MailSmtp = "smtp.gmail.com";
        public const string MailSubject = "Information alert";
        public const string EmailMessage = "Infarmation about your subscription";
        public const string FileName = "result.csv";
        public const string FileFormat = "text/csv";
        public const string ErrorSend = "Not Send";
        public const string WheatherParam1 = "Citi";
        public const string WheatherParam2 = "Date";
        public const string JobMainParam = "Email";
        public const string RapidApiGoogleUrl = "https://google-translate1.p.rapidapi.com/language/translate/v2/languages";
        public const string RapidApiWheatherUrl = "https://weatherapi-com.p.rapidapi.com/future.json?q={0}&dt={1}";
        public const string RapidApiFootballUrl = "https://api-football-v1.p.rapidapi.com/v3/leagues";
    }
}
