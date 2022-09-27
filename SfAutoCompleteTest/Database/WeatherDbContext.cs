using Microsoft.EntityFrameworkCore;
using SfAutoCompleteTest.Data;

namespace SfAutoCompleteTest.Database;

public class WeatherDbContext: DbContext
{
    public WeatherDbContext(DbContextOptions<WeatherDbContext> options) :
        base(options)
    {
       
    }
    
    public virtual DbSet<WeatherForecast> WeatherForecasts { get; set; }
}