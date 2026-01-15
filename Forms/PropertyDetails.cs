using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AntdUI;
using Microsoft.Data.SqlClient;
using RentalSystemUI.Controllers;
using RentalSystemUI.Services;
using RentalSystemUI.Models;

namespace RentalSystemUI.Forms
{
    public partial class PropertyDetails : Form
    {
        private int _propertyId;
        private PropertyService _propService = new PropertyService();

        // --- CUSTOM EVENT: Tell parent to close me ---
        public event EventHandler? BackRequested;

        public PropertyDetails(int propertyId)
        {
            InitializeComponent();
            this.Size = new Size(1280, 800);
            this.MinimumSize = new Size(1280, 800);
            this.MaximumSize = new Size(1280, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            _propertyId = propertyId;
            LoadPropertyData();
            AddDummyAmenities();
        }

        private void LoadPropertyData()
        {
            try
            {
                // Call Service
                var prop = _propService.GetPropertyDetails(_propertyId);

                if (prop != null)
                {
                    lblTitle.Text = prop.Title ?? "Unknown Title";
                    lblSubHeader.Text = "★ 4.98 (124 reviews)  •  " + (prop.Address ?? "") + ", " + (prop.City ?? "");
                    lblDescription.Text = prop.Description ?? "No description available.";

                    decimal rent = prop.RentAmount;
                    lblPriceLarge.Text = "$" + rent.ToString("N0");
                    lblTotalValue.Text = "$" + (rent + 150).ToString("N0");

                    // Images
                    if (prop.ImagePaths.Count > 0) SetImage(picMain, prop.ImagePaths[0]);
                    if (prop.ImagePaths.Count > 1) SetImage(picSub1, prop.ImagePaths[1]);
                    if (prop.ImagePaths.Count > 2) SetImage(picSub2, prop.ImagePaths[2]);
                    if (prop.ImagePaths.Count > 3) SetImage(picSub3, prop.ImagePaths[3]);
                    if (prop.ImagePaths.Count > 4) SetImage(picSub4, prop.ImagePaths[4]);
                }
            }
            catch (Exception ex)
            {
                AntdUI.Message.error(this, "Error: " + ex.Message);
            }
        }

        private void SetImage(PictureBox box, string path)
        {
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                box.Image = Image.FromFile(path);
                box.Cursor = Cursors.Hand;
                box.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void AddDummyAmenities()
        {
            if (flowAmenities.Controls.Count > 0) return;
            string[] items = { "Wifi", "Kitchen", "Washer", "Dryer", "Air conditioning", "Heating", "Dedicated workspace", "TV", "Hair dryer", "Iron" };
            foreach (var item in items)
            {
                System.Windows.Forms.Label lbl = new System.Windows.Forms.Label();
                lbl.Text = "• " + item;
                lbl.AutoSize = true;
                lbl.Font = new Font("Segoe UI", 10);
                lbl.Margin = new Padding(0, 5, 20, 5);
                flowAmenities.Controls.Add(lbl);
            }
        }

        private void btnBookNow_Click(object sender, EventArgs e)
        {
            AntdUI.Message.success(this, "Booking Request Sent!");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // TRIGGER EVENT INSTEAD OF CLOSING
            BackRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}