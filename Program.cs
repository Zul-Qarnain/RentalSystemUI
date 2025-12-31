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
            // LOAD SECRETS FIRST
            DotNetEnv.Env.Load();

            ApplicationConfiguration.Initialize();
            Application.Run(new RentAllSearch());
        }
    }
}