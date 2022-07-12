using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OmegaSoftware.TestProject.BL.App.Configuration;
using OmegaSoftware.TestProject.BL.Domain.Configuration;
using OmegaSoftware.TestProject.Configuration;
using OmegaSoftware.TestProject.DAL.Configuration;
using OmegaSoftware.TestProject.Web.Configuration;
using Swashbuckle.AspNetCore.SwaggerGen;
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
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection(ApplicationConfiguration.JwtConfig));
builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection(ApplicationConfiguration.EmailConfig));
builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection(ApplicationConfiguration.ConnectionStrings));
var key = Encoding.ASCII.GetBytes(builder.Configuration[ApplicationConfiguration.JwtSecret]);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TodoApp", Version = "v1" });
    c.AddSecurityDefinition("BearerAuth", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme.ToLowerInvariant(),
        In = ParameterLocation.Header,
        Name = "Authorization",
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer scheme."
    });

    c.OperationFilter<AuthResponsesOperationFilter>();
});

var tokenValidationParams = new TokenValidationParameters
{
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(key),
    ValidateIssuer = false,
    ValidateAudience = false,
    ValidateLifetime = false,
    RequireExpirationTime = false,
    ClockSkew = TimeSpan.Zero
};

builder.Services.AddSingleton(tokenValidationParams);
builder.Services.AddHttpContextAccessor();

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

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors(ApplicationConfiguration.Cors);

app.UseStatusCodePages();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

public class AuthResponsesOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var attributes = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                            .Union(context.MethodInfo.GetCustomAttributes(true));

        if (attributes.OfType<IAllowAnonymous>().Any())
        {
            return;
        }

        var authAttributes = attributes.OfType<IAuthorizeData>();

        if (authAttributes.Any())
        {
            operation.Responses["401"] = new OpenApiResponse { Description = "Unauthorized" };

            if (authAttributes.Any(att => !String.IsNullOrWhiteSpace(att.Roles) || !String.IsNullOrWhiteSpace(att.Policy)))
            {
                operation.Responses["403"] = new OpenApiResponse { Description = "Forbidden" };
            }

            operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Id = "BearerAuth",
                                    Type = ReferenceType.SecurityScheme
                                }
                            },
                            Array.Empty<string>()
                        }
                    }
                };
        }
    }
}