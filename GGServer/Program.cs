using System;
using System.Windows.Forms;

namespace GGServer
{
    static class Program
    {
        internal static WinFormsServer MainForm { get; set; }
        /////////////////////////////////////////////////
        /// The main entry point for the application.
        /////////////////////////////////////////////////
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm = new WinFormsServer();
            Application.Run(MainForm);
        }
    }
}
