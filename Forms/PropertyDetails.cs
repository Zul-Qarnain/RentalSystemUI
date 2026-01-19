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
        private readonly TenantService _tenantService = new TenantService();

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
        }

        private void LoadPropertyData()
        {
            try
            {
                // Get full property details
                var prop = _propService.GetPropertyById(_propertyId);
                if (prop == null)
                {
                    AntdUI.Message.error(this, "Property not found!");
                    Close();
                    return;
                }

                // Get images and reviews
                var images = _propService.GetPropertyImages(_propertyId);
                var reviews = _propService.GetReviewsForProperty(_propertyId);

                // Populate UI
                lblTitle.Text = prop.Title;
                lblSubHeader.Text = $"★ {(reviews.Count > 0 ? "4.98" : "New")} ({reviews.Count} reviews) • {prop.City}";
                lblDescription.Text = prop.Description;

                // Price Formatting (Real BDT)
                lblPriceLarge.Text = $"৳{prop.RentAmount:N0} / month";
                lblTotalValue.Text = $"৳{prop.RentAmount:N0}";

                // Images
                if (images.Count > 0) SetImage(picMain, images[0]);
                if (images.Count > 1) SetImage(picSub1, images[1]);
                if (images.Count > 2) SetImage(picSub2, images[2]);
                if (images.Count > 3) SetImage(picSub3, images[3]);
                if (images.Count > 4) SetImage(picSub4, images[4]);

                // Handle Booked State
                bool isRented = prop.Status == "Rented" || !prop.AvailabilityStatus;
                if (isRented)
                {
                    btnBook.Text = "ALREADY BOOKED";
                    btnBook.Enabled = false;
                    btnBook.Type = TTypeMini.Default; // Grey button
                    btnBook.BackColor = Color.LightGray;
                    btnBook.ForeColor = Color.DarkGray;
                }
                
                // Real Amenities
                LoadAmenities(prop);
            }
            catch (Exception ex)
            {
                AntdUI.Message.error(this, "Error: " + ex.Message);
            }
        }

        private void LoadAmenities(PropertyModel p)
        {
             flowAmenities.Controls.Clear();
             AddAmenityIf(p.Rooms > 0, $"{p.Rooms} Bedrooms");
             AddAmenityIf(p.Kitchen > 0, $"{p.Kitchen} Kitchen");
             AddAmenityIf(p.WashRoom > 0, $"{p.WashRoom} Washroom");
             AddAmenityIf(p.IsAC, "Air Conditioning");
             AddAmenityIf(p.IsPetAllowed, "Pets Allowed");
             AddAmenityIf(true, "Wifi"); // Assuming all have wifi for now as it's not in DB
        }

        private void AddAmenityIf(bool condition, string text)
        {
            if (!condition) return;
            System.Windows.Forms.Label lbl = new System.Windows.Forms.Label();
            lbl.Text = "• " + text;
            lbl.AutoSize = true;
            lbl.Font = new Font("Segoe UI", 10);
            lbl.Margin = new Padding(0, 5, 20, 5);
            flowAmenities.Controls.Add(lbl);
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



        private void btnBookNow_Click(object sender, EventArgs e)
        {
            var user = AppSession.CurrentUser;
            if (user == null)
            {
                AntdUI.Message.error(this, "Please login again.");
                return;
            }
            if (!string.Equals(user.UserType, "Tenant", StringComparison.OrdinalIgnoreCase))
            {
                AntdUI.Message.error(this, "Only tenants can book a property.");
                return;
            }

            DateTime start = dateCheckIn.Value is DateTime d1 ? d1 : DateTime.Today;
            DateTime end = dateCheckOut.Value is DateTime d2 ? d2 : DateTime.Today.AddMonths(1);
            if (end <= start)
            {
                AntdUI.Message.error(this, "Check-out must be after check-in.");
                return;
            }

            int bookingId = _tenantService.CreateBooking(user.UserID, _propertyId, start, end);
            if (bookingId == -2)
            {
                AntdUI.Message.error(this, "This property is already booked. Please try another property.");
                return;
            }
            if (bookingId <= 0)
            {
                AntdUI.Message.error(this, "Could not create booking. Please try again.");
                return;
            }

            AntdUI.Message.success(this, $"Booking submitted successfully! Awaiting landlord approval.");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // TRIGGER EVENT INSTEAD OF CLOSING
            BackRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}