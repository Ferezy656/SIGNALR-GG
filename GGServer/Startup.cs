using Owin;

namespace GGServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
    /// <summary> 
    /// Echoes messages sent using the Send message by calling the 
    /// addMessage method on the client. Also reports to the console 
    /// when clients connect and disconnect. 
    /// </summary> 
}
