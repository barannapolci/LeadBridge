using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using LeadBridge.Core.Application.Interfaces;
using LeadBridge.Core.Application.Interfaces.Repositories;
using LeadBridge.Core.Application.Interfaces.Security;
using LeadBridge.Core.Application.Interfaces.Services;
using LeadBridge.Core.Application.Services;
using LeadBridge.Core.Infrastructure.Postgres;
using LeadBridge.Core.Infrastructure.Postgres.Repositories;
using LeadBridge.Core.Infrastructure.Postgres.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console(formatProvider: CultureInfo.InvariantCulture)
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services));
    builder.Services.AddControllers();
    builder.Services.AddOpenApi();

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

    /*DI*/
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IAccountService, AccountService>();
    builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
    builder.Services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

    builder.Services
    .AddOptions<JwtSettings>()
    .Bind(builder.Configuration.GetSection(JwtSettings.SectionName))
    .ValidateDataAnnotations()
    .ValidateOnStart();

    builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

    builder.Services
        .AddOptions<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme)
        .Configure<IOptions<JwtSettings>>((bearer, jwtOptions) =>
        {
            var jwt = jwtOptions.Value;

            bearer.MapInboundClaims = false;
            bearer.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwt.Issuer,
                ValidateAudience = true,
                ValidAudience = jwt.Audience,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecretKey)),
                ClockSkew = TimeSpan.FromSeconds(30),
                NameClaimType = JwtRegisteredClaimNames.Sub,
                RoleClaimType = "role",
            };
        });

    builder.Services.AddAuthorization();

    WebApplication app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
        app.MapScalarApiReference();
    }

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}