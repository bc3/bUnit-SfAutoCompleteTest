using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using SfAutoCompleteTest.Data;
using SfAutoCompleteTest.Database;
using Syncfusion.Blazor;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSyncfusionBlazor(options => { options.IgnoreScriptIsolation = true; });
builder.Services.AddDbContext<WeatherDbContext>(opt =>
    opt.UseInMemoryDatabase("WeatherForecasts"));
builder.Services.AddControllers()
    .AddOData(opt => opt.Select().Filter().OrderBy().SetMaxTop(100).SkipToken().Expand().Count().AddRouteComponents("odata", GetEdmModel()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.MapControllers();

app.Run();

static IEdmModel GetEdmModel()
{
    var builder = new ODataConventionModelBuilder();
    builder.EntitySet<WeatherForecast>("WeatherForecasts");
    return builder.GetEdmModel();
}