using Microsoft.AspNetCore.SignalR;

namespace SfAutoCompleteTest.Logger;

public interface ITypedHubClient
{
    Task BroadcastMessage(string message);
}

public class LoggerHub : Hub<ITypedHubClient>
{
    public async Task SendMessage(string message)
    {
        await Clients.All.BroadcastMessage(message);
    }
}