using Microsoft.Owin.Hosting;
using System;

namespace GGServer
{
    public class Server
    {
        private IDisposable SignalR { get; set; }

        public Server() { }

        public void StartServer(string uri)
        {
            SignalR = WebApp.Start(uri);
        }

        public void StopServer()
        {
            if (SignalR != null)
            {
                SignalR.Dispose();
            }
        }
    }
}
