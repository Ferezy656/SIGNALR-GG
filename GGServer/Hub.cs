using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace GGServer
{
    /// <summary> 
    /// Echoes messages sent using the Send message by calling the 
    /// addMessage method on the client. Also reports to the console 
    /// when clients connect and disconnect. 
    /// </summary> 
    public class MyHub : Hub
    {
        public void Send(string name, string message)
        {
            Clients.All.addMessage(name, message);
        }
        public override Task OnConnected()
        {
            Program.MainForm.WriteToConsole("Client connected: " + Context.ConnectionId);
            return base.OnConnected();
        }
        public async Task OnDisconnected()
        {
            Program.MainForm.WriteToConsole("Client disconnected: " + Context.ConnectionId);
            await base.OnDisconnected(true);
        }
    }
}
