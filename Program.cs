using Microsoft.AspNetCore.HttpOverrides;
// using Serilog;
using NTKServer;
using NTKServer.Internal;
using System.Runtime.InteropServices;
using NLog;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);
var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
builder.Logging.ClearProviders();
builder.Host.UseNLog();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
var configuration = builder.Configuration;


string? writeConnectionString = configuration.GetValue<string>("ConnectionStrings:WriteConnectionString");
string? readConnectionString = configuration.GetValue<string>("ConnectionStrings:ReadConnectionString");
DapperMysql.Init(writeConnectionString, readConnectionString);

builder.Services.AddSession(options =>
{
    options.Cookie.Name = "Session";
    options.IdleTimeout = TimeSpan.FromMinutes(60);
});

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddSingleton<SessionMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
	app.Urls.Add("http://localhost:5000");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
{
    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });
}

app.UseMiddleware<SessionMiddleware>();

Init.Run();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=home}/{action=Index}");

app.Run();
