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
                Console.WriteLine("--- APP STARTUP ---");
                
                // Initializer Database Tables (Do not swallow errors)
                Console.WriteLine("Initializing Database...");
                new RentalSystemUI.Data.DatabaseInitializer().EnsureTablesExist();
                Console.WriteLine("Database Initialized.");

                // DEBUG: Check Columns
                using (var conn = new RentalSystemUI.Data.Database().GetConnection())
                {
                    conn.Open();
                    Console.WriteLine("\n--- DEBUG: PAYMENTS COLUMNS ---");
                    using (var cmd = new Microsoft.Data.SqlClient.SqlCommand("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PAYMENTS'", conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) Console.WriteLine(reader[0]);
                    }
                    Console.WriteLine("---------------------------------\n");
                }

                // Check if .env file exists before running
                if (!System.IO.File.Exists(".env"))
                {
                    Console.WriteLine("CRITICAL ERROR: .env file is missing!");
                    MessageBox.Show("CRITICAL ERROR: The .env file is missing from the build folder.\n\nMake sure you set 'Copy to Output Directory' to 'Copy Always' in Visual Studio properties.", "Startup Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Run the App
                //Application.Run(new HomeownerDashboard());
                //Application.Run(new UserDashboard(1));
               Application.Run(new Form1());
            }
            catch (Exception ex)
            {
                // This catches the silent crash and shows it to you
                Console.WriteLine($"Application Crash: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                MessageBox.Show($"Application Crash:\n\n{ex.Message}\n\nStack Trace:\n{ex.StackTrace}", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}