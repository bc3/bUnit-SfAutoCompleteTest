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
    public async Task Index_SearchBoomDirect_ExpectData()
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

            var dropdown = cut.FindComponent<SfAutoComplete<string, WeatherForecast>>();
            var index = 1;
            var containerEle = dropdown.Find("input").ParentElement;
            await dropdown.Instance.ShowPopup();
            var popupEle = dropdown.Find(".e-popup");
            var liColl = popupEle.QuerySelectorAll("li.e-list-item");
            liColl[index].Click();
            liColl = popupEle.QuerySelectorAll("li.e-list-item");
            Assert.Contains("e-active", liColl[index].ClassName.Split(" "));
            var focusItem = popupEle.QuerySelector("li.e-item-focus");
            Assert.Null(focusItem);
            await dropdown.Instance.HidePopup();
            // dropdown.SetParametersAndRender(("Value", "AU"));
            // await Task.Delay(200);
            var inputEle = dropdown.Find("input");
            Assert.AreEqual(index, dropdown.Instance.Index);
            // dropdown.SetParametersAndRender(("ShowClearButton", true));
            // containerEle = dropdown.Find("input").ParentElement;
            // var clearEle = containerEle.Children[1];
            // Assert.AreEqual("e-clear-icon", clearEle.ClassName);
            // clearEle.MouseDown();       
            
            Assert.AreEqual(dropdown.Instance.Value, "Belgium - Boom");
            
    }
    
}