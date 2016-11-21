using System;
using System.Collections.Generic;
using System.Windows.Forms; 

namespace SynchronousMultipleCapture
{
    static class EntryPoint
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ParamerersWindow frmMainForm = new ParamerersWindow();
            frmMainForm.ShowDialog();
        }
    }
}