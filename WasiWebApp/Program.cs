using WasiWebApp.Models;

var builder = WebApplication.CreateBuilder(args).UseWasiConnectionListener();
var config = builder.Configuration.Get<AppSettings>();

// Add services to the container.
builder.Services.Configure<AppSettings>(builder.Configuration);
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UsePathBase("/oauth-homework");
app.UseBundledStaticFiles();
app.UseRouting();

app.MapGet("/", () => $"Hello World!");
app.MapGet("/weatherforecast", () =>
{
    var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
    var forecast = Enumerable.Range(1, 5).Select(index => new
    {
        Date = DateTime.Now.AddDays(index),
        TempC = Random.Shared.Next(-20, 55),
        Summary = summaries[Random.Shared.Next(summaries.Length)]
    }).ToArray();
    return forecast;
});

app.Run();
