using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.SignalR;
using SfAutoCompleteTest.Data;
using SfAutoCompleteTest.Database;
using SfAutoCompleteTest.Logger;

namespace SfAutoCompleteTest.Controllers;

public class WeatherForecastsController : ODataController
{
    private readonly WeatherDbContext _context;
    private readonly IJsConsoleService _logger;
    private IHubContext<LoggerHub, ITypedHubClient> _hubContext;

    public WeatherForecastsController(WeatherDbContext context, IHubContext<LoggerHub, ITypedHubClient> hubContext)
    {
        _context = context;
        _hubContext = hubContext;
        if (!context.WeatherForecasts.Any())
        {
            _context.Database.EnsureCreated();

            for (int i = 0; i <= 1000; i++)
            {
                _context.WeatherForecasts.Add(new WeatherForecast()
                {
                    Date = DateTime.Now,
                    Summary = $"Plaats {i}",
                    TemperatureC = i
                });
            }

            _context.SaveChanges();
        }
    }

    [EnableQuery]
    public IActionResult Get()
    {
        _hubContext.Clients.All.BroadcastMessage($"{Request.Path} - {Request.QueryString}");
        var albums = _context.WeatherForecasts;
        return Ok(albums);
    }
}