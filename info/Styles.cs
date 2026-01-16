using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace LandlordPortal
{
    public static class Styles
    {
        // --- EXACT COLORS FROM DESIGN ---
        public static Color Back = Color.FromArgb(244, 247, 254); // Main Background
        public static Color White = Color.White;
        public static Color Blue = Color.FromArgb(67, 24, 255);   // Brand Blue
        public static Color TextMain = Color.FromArgb(43, 54, 116); // Dark Navy
        public static Color TextGray = Color.FromArgb(163, 174, 208); // Soft Gray

        // Badge Colors
        public static Color OrangeBg = Color.FromArgb(255, 237, 213);
        public static Color OrangeTxt = Color.FromArgb(200, 100, 0);
        public static Color GreenBg = Color.FromArgb(5, 205, 153);
        public static Color GreenTxt = Color.White;
        public static Color BlueBg = Color.FromArgb(220, 240, 255);

        // Fonts
        public static Font Header = new Font("Segoe UI", 24, FontStyle.Bold);
        public static Font SubHeader = new Font("Segoe UI", 10, FontStyle.Regular);
        public static Font CardTitle = new Font("Segoe UI", 14, FontStyle.Bold);
        public static Font Bold = new Font("Segoe UI", 9, FontStyle.Bold);
        public static Font Normal = new Font("Segoe UI", 9, FontStyle.Regular);

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
    }

    // Helper class for Rounded Panels
    public class RoundedPanel : Panel
    {
        public RoundedPanel() { this.DoubleBuffered = true; this.BackColor = Styles.White; }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this.Region = Region.FromHrgn(Styles.CreateRoundRectRgn(0, 0, this.Width, this.Height, 25, 25));
        }
    }
}