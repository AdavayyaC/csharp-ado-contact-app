using System;
using System.Windows.Forms;

namespace csharp_ado_contact
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// 
        /// SWITCH BETWEEN MODES:
        /// - FormConnected   → Connected ADO.NET (queries DB each time)
        /// - FormDisconnected → Disconnected ADO.NET (loads into memory, saves on close)
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Change to FormConnected to use Connected mode
            Application.Run(new FormConnected());
        }
    }
}