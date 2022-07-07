using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OmegaSoftware.TestProject.BL.App.Configuration;
using OmegaSoftware.TestProject.BL.Domain.Configuration;
using OmegaSoftware.TestProject.Configuration;
using OmegaSoftware.TestProject.DAL.Configuration;
using OmegaSoftware.TestProject.Web.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterAppService();
builder.Services.RegisterDomainService();
builder.Services.RegisterRepository();
builder.Services.RegisterBLMappingConfig();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection(ApplicationConfiguration.JwtConfig));
builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection(ApplicationConfiguration.EmailConfig));
builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection(ApplicationConfiguration.ConnectionStrings));
var key = Encoding.ASCII.GetBytes(builder.Configuration[ApplicationConfiguration.JwtSecret]);

var tokenValidationParams = new TokenValidationParameters
{
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(key),
    ValidateIssuer = false,
    ValidateAudience = false,
    ValidateLifetime = true,
    RequireExpirationTime = false,
    ClockSkew = TimeSpan.Zero
};

builder.Services.AddSingleton(tokenValidationParams);

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(jwt => {
    jwt.SaveToken = true;
    jwt.TokenValidationParameters = tokenValidationParams;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(ApplicationConfiguration.Cors, builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(ApplicationConfiguration.Policy,
        policy => policy.RequireClaim(ApplicationConfiguration.PolicyClaim));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseStatusCodePages();

app.UseCors(ApplicationConfiguration.Cors);

app.MapControllers();

app.Run();
