using Microsoft.AspNetCore.SignalR;

namespace ETL.API.Hubs;

public class ETLHub : Hub
{
    public async Task SendLogUpdate(string message) => 
        await Clients.All.SendAsync("ReceiveLogUpdate", message);
}