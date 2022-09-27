using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using SfAutoCompleteTest.Data;
using SfAutoCompleteTest.Database;

namespace SfAutoCompleteTest.Controllers;

public class WeatherForecastsController : ODataController
{
    private readonly WeatherDbContext _context;

    public WeatherForecastsController(WeatherDbContext context)
    {
        _context = context;
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
        var albums = _context.WeatherForecasts;
        return Ok(albums);
    }
}