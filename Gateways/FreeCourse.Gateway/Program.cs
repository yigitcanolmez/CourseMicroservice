using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

//builder.Configuration
//    .SetBasePath(builder.Environment.ContentRootPath.ToLower())
//    .AddJsonFile("appsettings.json", true, true)
//    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName.ToLower()}.json", true, true)
//    .AddJsonFile($"configuration.{builder.Environment.EnvironmentName.ToLower()}.json")
//    .AddEnvironmentVariables();

builder.Configuration.AddJsonFile($"configuration.{builder.Environment.EnvironmentName.ToLower()}.json")
    .AddEnvironmentVariables();
builder.Services.AddOcelot();



var app = builder.Build();

await app.UseOcelot();

app.MapGet("/", () => "Hello World!");

app.Run();
