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
                // Get property images and basic info
                var images = _propService.GetPropertyImages(_propertyId);
                var reviews = _propService.GetReviewsForProperty(_propertyId);

                // For now, set basic placeholder values
                // TODO: Add GetPropertyById method to PropertyService
                lblTitle.Text = "Property #" + _propertyId;
                lblSubHeader.Text = $"★ {(reviews.Count > 0 ? "4.98" : "New")} ({reviews.Count} reviews)";
                lblDescription.Text = "Property details loaded from database.";

                lblPriceLarge.Text = "$1,500";
                lblTotalValue.Text = "$1,650";

                // Images
                if (images.Count > 0) SetImage(picMain, images[0]);
                if (images.Count > 1) SetImage(picSub1, images[1]);
                if (images.Count > 2) SetImage(picSub2, images[2]);
                if (images.Count > 3) SetImage(picSub3, images[3]);
                if (images.Count > 4) SetImage(picSub4, images[4]);
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