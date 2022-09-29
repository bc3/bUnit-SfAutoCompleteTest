using Microsoft.JSInterop;

namespace SfAutoCompleteTest.Logger;

public interface IJsConsoleService
{
    Task Log(string message);
    Task LogError(Exception exception);
}

public class JsConsoleService : IJsConsoleService
{
    private readonly IJSRuntime _jsRuntime;

    public JsConsoleService(IJSRuntime jSRuntime)
    {
        this._jsRuntime = jSRuntime;
    }

    public async Task Log(string message)
    {
        await this._jsRuntime.InvokeVoidAsync("console.log", message);
    }

    public async Task LogError(Exception exception)
    {
        await this._jsRuntime.InvokeVoidAsync("console.log",
            $"{exception.Message}{Environment.NewLine}{exception.StackTrace}");
    }
}