using GG.Common;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GGServer
{
    /// <summary> 
    /// WinForms host for a SignalR server. The host can stop and start the SignalR 
    /// server, report errors when trying to start the server on a URI where a 
    /// server is already being hosted, and monitor when clients connect and disconnect.  
    /// The hub used in this server is a simple echo service, and has the same  
    /// functionality as the other hubs in the SignalR Getting Started tutorials. 
    /// </summary> 
    public partial class WinFormsServer : Form
    {
        private Server server;
        private readonly string serverUriTemplate = "http://localhost:{0}";
        private string serverUri;

        internal WinFormsServer()
        {
            InitializeComponent();
            this.server = new Server();
        }
        /// <summary> 
        /// Calls the StartServer method with Task.Run to not 
        /// block the UI thread.  
        /// </summary> 

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            this.serverUri = ServerUriBuilder.PrepareServerUri(this.serverUriTemplate, textBox1.Text);

            WriteToConsole("Starting server...");
            ButtonStart.Enabled = false;

            try
            {
                Task.Run(() => server.StartServer(this.serverUri));
            }
            catch (TargetInvocationException)
            {
                WriteToConsole("Server failed to start. A server is already running on " + this.serverUri);
                //Re-enable button to let user try to start server again 
                this.Invoke((Action)(() => ButtonStart.Enabled = true));
                return;

            }
            this.Invoke((Action)(() => ButtonStop.Enabled = true));
            WriteToConsole("Server started at " + this.serverUri);
        }
        /// <summary> 
        /// This method adds a line to the RichTextBoxConsole control, using Invoke if used 
        /// from a SignalR hub thread rather than the UI thread. 
        /// </summary> 
        /// <param name="message"></param> 


        private void ButtonStop_Click(object sender, EventArgs e)
        {
            ///SignalR will be disposed in the FormClosing event 
            Close();
        }

        internal void WriteToConsole(String message)
        {
            if (richTextBox1.InvokeRequired)
            {
                this.Invoke((Action)(() =>
                    WriteToConsole(message)
                ));
                return;
            }
            richTextBox1.AppendText(message + Environment.NewLine);
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
