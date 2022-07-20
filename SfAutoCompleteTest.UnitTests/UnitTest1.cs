using Bunit;
using SfAutoCompleteTest.Data;
using SfAutoCompleteTest.Pages;
using Syncfusion.Blazor;
using Syncfusion.Blazor.DropDowns;

namespace SfAutoCompleteTest.UnitTests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Index_OpenPage_ExpectNoData()
    {
        using var ctx = new Bunit.TestContext();
        ctx.JSInterop.Mode = JSRuntimeMode.Loose;
        ctx.Services.AddSyncfusionBlazor();
        var cut = ctx.RenderComponent<Weather>();
        cut.MarkupMatches("<h3><span>Choose data</span></h3><hr>" +
                          "<pre diff:ignoreChildren diff:ignoreAttributes></pre>" +
                          "<div diff:ignoreChildren diff:ignoreAttributes></div>" +
                          "<style diff:ignoreChildren diff:ignoreAttributes></style>" +
                          "<span diff:ignoreChildren diff:ignoreAttributes></span>");
    }
    
    [Test]
    public void Index_SearchBoomDirect_ExpectData()
    {
      
            using var ctx = new Bunit.TestContext();
            ctx.JSInterop.Mode = JSRuntimeMode.Loose;
            ctx.Services.AddSyncfusionBlazor();
            var cut = ctx.RenderComponent<Weather>(parameters =>
            {
                parameters.Add(p => p.WeatherForecastChanged, (x) =>
                {
                    
                    Assert.AreEqual(x.Summary, CustomAdaptor.BelgiumBoom);
                    Assert.AreEqual(x.TemperatureC, 35);
                });
            });

            var autocomplete = cut.FindComponent<SfAutoComplete<string, WeatherForecast>>();

            var input = autocomplete.Find("input.e-autocomplete");
            input.Change("Boom");
            input.KeyUp(Key.Enter);            
            
            Assert.AreEqual(autocomplete.Instance.Value, "Boom");
            
    }
    
}