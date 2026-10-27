using System.Globalization;
using LeadBridge.Core.Application.Interfaces;
using LeadBridge.Core.Application.Interfaces.Repositories;
using LeadBridge.Core.Application.Interfaces.Security;
using LeadBridge.Core.Application.Interfaces.Services;
using LeadBridge.Core.Application.Services;
using LeadBridge.Core.Infrastructure.Postgres;
using LeadBridge.Core.Infrastructure.Postgres.Repositories;
using LeadBridge.Core.Infrastructure.Postgres.Security;
using Microsoft.EntityFrameworkCore;
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

    WebApplication app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
        app.MapScalarApiReference();
    }

    app.UseHttpsRedirection();

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