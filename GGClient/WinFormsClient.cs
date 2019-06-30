using GG.Common;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Net.Http;
using System.Windows.Forms;

namespace GGClient
{
    public partial class WinFormsClient : Form
    {
        private String UserName { get; set; }
        private IHubProxy HubProxy { get; set; }
        private HubConnection Connection { get; set; }

        public WinFormsClient()
        {
            InitializeComponent();
        }

        private void ButtonSend_Click(object sender, EventArgs e)
        {
            HubProxy.Invoke("Send", UserName, TextBoxMessage.Text);
            TextBoxMessage.Text = String.Empty;
            TextBoxMessage.Focus();
        }

        private async void ConnectAsync()
        {
            string ServerURI = String.Empty;

            try
            {
                ServerURI = ServerUriBuilder.GetServerUriForClient(this.textBox1.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Podany adres jest nieprawidłowy. Wyjątek: {ex.Message}");
                return;
            }

            Connection = new HubConnection(ServerURI);
            Connection.Closed += Connection_Closed;
            HubProxy = Connection.CreateHubProxy("MyHub");
            HubProxy.On<string, string>("AddMessage", (name, message) =>
                this.Invoke((Action)(() =>
                    RichTextBoxConsole.AppendText(String.Format("{0} {1}: {2}" + Environment.NewLine, name, DateTime.Now ,message))
                ))
            );
            try
            {
                await Connection.Start();
            }
            catch (HttpRequestException)
            {
                StatusText.Text = "Unable to connect to server: Start server before connecting clients.";
                return;
            }

            SignInPanel.Visible = false;
            ChatPanel.Visible = true;
            ButtonSend.Enabled = true;
            TextBoxMessage.Focus();
            RichTextBoxConsole.AppendText("Connected to server at " + ServerURI + Environment.NewLine);
        }

        private void Connection_Closed()
        {
            this.Invoke((Action)(() => ChatPanel.Visible = false));
            this.Invoke((Action)(() => ButtonSend.Enabled = false));
            this.Invoke((Action)(() => textBox1.Visible = true));
            this.Invoke((Action)(() => StatusText.Text = "You have been disconnected."));
            this.Invoke((Action)(() => SignInPanel.Visible = true));
        }

        private void SignInButton_Click(object sender, EventArgs e)
        {
            UserName = UserNameTextBox.Text;
            if (!String.IsNullOrEmpty(UserName))
            {
                StatusText.Visible = true;
                StatusText.Text = "Connecting to server...";
                ConnectAsync();
            }
        }

        private void WinFormsClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Connection != null)
            {
                Connection.Stop();
                Connection.Dispose();
            }
        }
    }
}
