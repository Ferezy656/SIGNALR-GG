using GG.Common;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GGServer
{
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
                this.Invoke((Action)(() => ButtonStart.Enabled = true));
                return;

            }
            this.Invoke((Action)(() => ButtonStop.Enabled = true));
            WriteToConsole("Server started at " + this.serverUri);
        }


        private void ButtonStop_Click(object sender, EventArgs e)
        {
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
