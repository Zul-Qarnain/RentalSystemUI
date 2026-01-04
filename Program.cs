using System;
using System.Windows.Forms;
using RentalSystemUI.Forms;

namespace RentalSystemUI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            try
            {
                // Force AntdUI English (Optional)
                try { AntdUI.Localization.SetLanguage("en-US"); } catch { }

                // Check if .env file exists before running
                if (!System.IO.File.Exists(".env"))
                {
                    MessageBox.Show("CRITICAL ERROR: The .env file is missing from the build folder.\n\nMake sure you set 'Copy to Output Directory' to 'Copy Always' in Visual Studio properties.", "Startup Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Run the App
                Application.Run(new Form1());
            }
            catch (Exception ex)
            {
                // This catches the silent crash and shows it to you
                MessageBox.Show($"Application Crash:\n\n{ex.Message}\n\nStack Trace:\n{ex.StackTrace}", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}