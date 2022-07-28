using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;

namespace SfAutoCompleteTest.Data;

public class CustomAdaptor : DataAdaptor
{
    public const string BelgiumBoom = "Belgium - Boom";
    
    public static List<WeatherForecast> order = new List<WeatherForecast>()
    {
        new() { Date = DateTime.Today.Date, Summary = "Belgium - Hasselt", TemperatureC = 40},
        new() { Date = DateTime.Today.Date, Summary = BelgiumBoom, TemperatureC = 35 },
        new() { Date = DateTime.Today.Date, Summary = "Belgium - Sint-Truiden", TemperatureC = 32 },
        new() { Date = DateTime.Today.Date, Summary = "Belgium - Borgloon", TemperatureC = 37 }
    };

    public override object Read(DataManagerRequest dm, string key = null)
    {
        var searchValue = dm.Where[0].value.ToString();

        if (searchValue != null && searchValue.Length > 2)
        {
            IEnumerable<WeatherForecast> DataSource = order;
            if (dm.Search != null && dm.Search.Count > 0)
            {
                DataSource = DataOperations.PerformSearching(DataSource, dm.Search); //Search
            }

            if (dm.Sorted != null && dm.Sorted.Count > 0) //Sorting
            {
                DataSource = DataOperations.PerformSorting(DataSource, dm.Sorted);
            }

            if (dm.Where != null && dm.Where.Count > 0) //Filtering
            {
                DataSource = DataOperations.PerformFiltering(DataSource, dm.Where, dm.Where[0].Operator);
            }

            int count = DataSource.Cast<WeatherForecast>().Count();
            if (dm.Skip != 0)
            {
                DataSource = DataOperations.PerformSkip(DataSource, dm.Skip); //Paging
            }

            if (dm.Take != 0)
            {
                DataSource = DataOperations.PerformTake(DataSource, dm.Take);
            }

            return dm.RequiresCounts ? new DataResult() { Result = DataSource, Count = count } : (object)DataSource;
        }

        return new DataResult();
    }
}