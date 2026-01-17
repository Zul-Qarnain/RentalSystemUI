using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RentalSystemUI
{
    public static class Styles
    {
        // --- EXACT COLORS FROM DESIGN ---
        public static Color Back = Color.FromArgb(244, 247, 254); // Main Background
        public static Color White = Color.White;
        public static Color Blue = Color.FromArgb(67, 24, 255);   // Brand Blue (Primary)
        public static Color DarkBlue = Color.FromArgb(43, 54, 116); // Dark Navy (Text Main)
        public static Color TextGray = Color.FromArgb(163, 174, 208); // Soft Gray (Text Secondary)
        
        // Secondary Accents
        public static Color LightBlue = Color.FromArgb(220, 240, 255);
        public static Color Purple = Color.Purple;

        // Badge / Status Colors
        public static Color OrangeBg = Color.FromArgb(255, 237, 213);
        public static Color OrangeTxt = Color.FromArgb(200, 100, 0);
        public static Color GreenBg = Color.FromArgb(5, 205, 153);
        public static Color GreenTxt = Color.White;
        public static Color RedBg = Color.FromArgb(255, 241, 240);
        public static Color RedTxt = Color.Red;

        // Fonts
        public static Font Header = new Font("Segoe UI", 24, FontStyle.Bold);
        public static Font PageTitle = new Font("Segoe UI", 20, FontStyle.Bold);
        public static Font SubHeader = new Font("Segoe UI", 10, FontStyle.Regular);
        public static Font CardTitle = new Font("Segoe UI", 14, FontStyle.Bold);
        public static Font Bold = new Font("Segoe UI", 9, FontStyle.Bold);
        public static Font Normal = new Font("Segoe UI", 9, FontStyle.Regular);
        public static Font Small = new Font("Segoe UI", 8, FontStyle.Regular);
    }
}
