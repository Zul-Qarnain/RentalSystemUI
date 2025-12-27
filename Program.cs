using System;
using System.Windows.Forms;
using RentalSystemUI.Forms; // <--- This line tells it where Form1 is!

namespace RentalSystemUI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            // Now it can find Form1 because we added the 'using' line above
            Application.Run(new Form1());
        }
    }
}