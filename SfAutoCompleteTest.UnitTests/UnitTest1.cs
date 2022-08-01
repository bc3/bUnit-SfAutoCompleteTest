using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using SfAutoCompleteTest.Data;
using SfAutoCompleteTest.Pages;
using Syncfusion.Blazor;
using Syncfusion.Blazor.DropDowns;
using Index = SfAutoCompleteTest.Pages.Index;

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
    
    [TestCase(0, CustomAdaptor.BelgiumBoom, "Boom" , 1)]
    [TestCase(0,  CustomAdaptor.BelgiumBoortmeerbeek , "Boor", 1)] 
    [TestCase(0, CustomAdaptor.BelgiumBorgloon, "Borg", 1)] 
    [TestCase(0, CustomAdaptor.BelgiumBoom, "Bo", 3)] 
    [TestCase(2, CustomAdaptor.BelgiumBoortmeerbeek, "Bo", 3)] 
    [TestCase(1, CustomAdaptor.BelgiumBorgloon, "Bo", 3)] 
    [TestCase(0, CustomAdaptor.BelgiumBoom, "Boo", 2)] 
    [TestCase(1, CustomAdaptor.BelgiumBoortmeerbeek, "Boo", 2)] 
    public async Task Index_SearchBoomDirect_ExpectData(int index, string value, string searchText, int count)
    {
      
            using var ctx = new Bunit.TestContext();
            ctx.JSInterop.Mode = JSRuntimeMode.Loose;
            ctx.Services.AddSyncfusionBlazor();
            var cut = ctx.RenderComponent<Weather>(parameters =>
            {
                parameters.Add(p => p.WeatherForecastChanged, (x) =>
                {
                    
                    Assert.AreEqual(x.Summary, value);
                });
            });

            var dropdown = cut.FindComponent<SfAutoComplete<string, WeatherForecast>>();
           
            var filterInput = dropdown.Find("input");
            Assert.NotNull(filterInput);
            KeyboardEventArgs args = new KeyboardEventArgs() { Code = searchText, Key = searchText };
            filterInput.NodeValue = searchText;
            filterInput.Input(new ChangeEventArgs() { Value = searchText });
            filterInput.KeyUp(args);
            await Task.Delay(100);
            var popupEle = dropdown.Find(".e-popup");
            var liCollection = popupEle.QuerySelectorAll("li.e-list-item");
            liCollection[index].Click();
            await Task.Delay(100);
            Assert.AreEqual(count, liCollection.Length);
            
            Assert.AreEqual(dropdown.Instance.Value, value);
            
    }
    
}