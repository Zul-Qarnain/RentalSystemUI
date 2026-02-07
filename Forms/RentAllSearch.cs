using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using AntdUI;
using RentalSystemUI.Controllers;
using RentalSystemUI.Services;
using RentalSystemUI.Models;

namespace RentalSystemUI.Forms
{
    public partial class RentAllSearch : Form
    {
        private readonly PropertyService _propService = new PropertyService();
        private readonly UserDashboard? _dashboard;
        private bool _loadedOnce;

        private const int RentMaxTaka = 100000;

        private bool _rentTouched;
        private bool _bedsTouched;
        private bool _bathTouched;
        private bool _kitchenTouched;
        private bool _corrTouched;
        private bool _sqftTouched;
        private bool _petTouched;


        public RentAllSearch(UserDashboard? dashboard = null)
        {
            _dashboard = dashboard;
            InitializeComponent();

            StartPosition = FormStartPosition.Manual;

            // Defaults
            InitializeFilterDefaults();

            if (flowListings != null)
            {
                flowListings.Resize += (s, e) => RecalculatePadding();
            }

            // Wire search
            if (btnSearch != null)
            {
                btnSearch.Click += (s, e) => LoadRealData();
            }
            if (txtSearch != null)
            {
                txtSearch.KeyDown += (s, e) =>
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        LoadRealData();
                    }
                };
            }

            // Filters: only apply after user touches
            if (sliderRent != null)
            {
                sliderRent.MouseDown += (s, e) => _rentTouched = true;
                sliderRent.ValueChanged += (s, e) => { UpdateRentLabel(); if (_rentTouched) LoadRealData(); };
            }

            if (selBedroom != null) selBedroom.SelectedValueChanged += (s, e) => { _bedsTouched = true; LoadRealData(); };
            if (selBathroom != null) selBathroom.SelectedValueChanged += (s, e) => { _bathTouched = true; LoadRealData(); };
            if (selKitchen != null) selKitchen.SelectedValueChanged += (s, e) => { _kitchenTouched = true; LoadRealData(); };
            if (selCorridor != null) selCorridor.SelectedValueChanged += (s, e) => { _corrTouched = true; LoadRealData(); };

            if (inputSqFt != null)
            {
                inputSqFt.TextChanged += (s, e) => { _sqftTouched = true; LoadRealData(); };
            }

            if (chkPet != null) chkPet.CheckedChanged += (s, e) => { _petTouched = true; LoadRealData(); };
            // if (chkParking != null) chkParking.CheckedChanged += (s, e) => { _parkingTouched = true; LoadRealData(); };

            // Remove deprecated checkboxes from behavior (and optionally hide)
            try { if (chkAC != null) chkAC.Visible = false; } catch { }
            try { if (chkWasher != null) chkWasher.Visible = false; } catch { }

            if (btnResetFilters != null)
            {
                btnResetFilters.Click += (s, e) =>
                {
                    InitializeFilterDefaults();
                    LoadRealData();
                };
            }

            Shown += (s, e) => RefreshEmbeddedLayout();
            VisibleChanged += (s, e) => { if (Visible) RefreshEmbeddedLayout(); };

            LoadRealData();
        }

        private void InitializeFilterDefaults()
        {
            _rentTouched = false;
            _bedsTouched = false;
            _bathTouched = false;
            _kitchenTouched = false;
            _corrTouched = false;
            _sqftTouched = false;
            _petTouched = false;
            _petTouched = false;

            if (sliderRent != null)
            {
                // Use slider as a MAX rent filter in range 0..50000.
                // Default = 50000 => no constraint.
                try { sliderRent.Value = RentMaxTaka; } catch { }
            }
            UpdateRentLabel();

            // Default selects to "Any" (so they do not constrain)
            TrySelectAny(selBedroom);
            TrySelectAny(selBathroom);
            TrySelectAny(selKitchen);
            TrySelectAny(selCorridor);

            if (inputSqFt != null) inputSqFt.Text = "0";

            if (chkPet != null) chkPet.Checked = false;
            if (chkParking != null) chkParking.Checked = false;
        }

        private void TrySelectAny(AntdUI.Select? sel)
        {
            if (sel == null) return;
            try
            {
                sel.Text = "Any";
            }
            catch { }
        }

        private void UpdateRentLabel()
        {
            if (lblRentTitle != null) lblRentTitle.Text = "Monthly Rent (Max)";
            if (lblRentValue == null || sliderRent == null) return;
            lblRentValue.Text = $"৳0 — ৳{sliderRent.Value:N0}";
        }

        private PropertySearchFilter BuildFilterFromUi()
        {
            var filter = PropertySearchFilter.Default;
            filter.SearchText = txtSearch?.Text?.Trim();

            // Only apply max rent if user touched it AND it is less than max.
            if (sliderRent != null && _rentTouched)
            {
                var max = (decimal)sliderRent.Value;
                if (max > 0 && max < RentMaxTaka)
                {
                    filter.MaxMonthlyRent = max;
                }
            }

            // Only apply counts if user touched relevant dropdown
            if (_bedsTouched) filter.Bedrooms = TryParseSelectInt(selBedroom);
            if (_bathTouched) filter.Washrooms = TryParseSelectInt(selBathroom);
            if (_kitchenTouched) filter.Kitchens = TryParseSelectInt(selKitchen);
            if (_corrTouched) filter.Corridors = TryParseSelectInt(selCorridor);

            if (_sqftTouched && inputSqFt != null)
            {
                try
                {
                    var raw = inputSqFt.Text?.ToString();
                    if (int.TryParse(raw, out var sqft) && sqft > 0)
                        filter.MinSquareFeet = sqft;
                }
                catch { }
            }

            // Only apply checkbox filters if user touched
            if (_petTouched && chkPet != null && chkPet.Checked)
                filter.PetFriendly = true;

            // Parking isn't in current DB schema, so no backend filter.
            // We keep the checkbox for UI only but don't track state.

            filter.OnlyAvailable = true;
            return filter;
        }

        private int? TryParseSelectInt(AntdUI.Select? sel)
        {
            if (sel == null) return null;
            try
            {
                var t = (sel.Text ?? string.Empty).Trim();
                if (string.IsNullOrWhiteSpace(t) || string.Equals(t, "Any", StringComparison.OrdinalIgnoreCase))
                    return null;

                t = t.Replace("+", "");
                if (int.TryParse(t, out var n)) return n;
            }
            catch { }
            return null;
        }

        private void RefreshEmbeddedLayout()
        {
            try
            {
                if (pnlMainContent != null) pnlMainContent.PerformLayout();
                if (pnlSearchHeader != null) pnlSearchHeader.PerformLayout();
                if (flowListings != null) flowListings.PerformLayout();
                RecalculatePadding();

                if (!_loadedOnce && flowListings != null && flowListings.ClientSize.Width > 0)
                {
                    _loadedOnce = true;
                    LoadRealData();
                }
            }
            catch { }
        }

        private void RecalculatePadding()
        {
            if (flowListings == null || flowListings.Controls.Count == 0) return;

            int cardWidth = 320 + 30;
            int availableWidth = flowListings.ClientSize.Width;

            if (availableWidth <= 0) return;

            int columns = Math.Max(1, availableWidth / cardWidth);
            int totalContentWidth = columns * cardWidth;

            int leftPadding = Math.Max(0, (availableWidth - totalContentWidth) / 2);

            if (flowListings.Padding.Left != leftPadding)
            {
                flowListings.Padding = new Padding(leftPadding, 10, 0, 10);
            }
        }

        private void LoadRealData()
        {
            try
            {
                if (flowListings != null) flowListings.Controls.Clear();

                var filter = BuildFilterFromUi();
                var properties = _propService.SearchProperties(filter);

                if (properties == null || properties.Count == 0)
                {
                    if (lblResultCount != null) lblResultCount.Text = "No properties available";
                    RecalculatePadding();
                    return;
                }

                if (lblResultCount != null)
                {
                    lblResultCount.Text = $"{properties.Count} properties found";
                }

                foreach (var prop in properties)
                {
                    AddProperty(
                        prop.PropertyID,
                        prop.Title,
                        prop.Address,
                        $"৳{prop.RentAmount:N0}",
                        "4.8",
                        "",
                        prop.FirstImagePath,
                        prop.Status
                    );
                }

                RecalculatePadding();
            }
            catch (Exception ex) { AntdUI.Message.error(this, "Error: " + ex.Message); }
        }

        private void AddProperty(int id, string title, string location, string price, string rating, string badge, string imagePath, string status)
        {
            AntdUI.Panel card = new AntdUI.Panel { Size = new Size(320, 420), Radius = 12, BackColor = Color.White, Margin = new Padding(15), Shadow = 10, ShadowColor = Color.FromArgb(20, 0, 0, 0), Cursor = Cursors.Hand };


            card.Click += (s, e) => OpenDetailsPage(id);

            PictureBox pic = new PictureBox { Dock = DockStyle.Top, Height = 200, BackColor = Color.LightGray, SizeMode = PictureBoxSizeMode.Zoom };
            if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
            {
                pic.Image = Image.FromFile(imagePath);
            }
            else
            {
                try
                {
                    var placeholder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "placeholder.png");
                    if (System.IO.File.Exists(placeholder))
                        pic.Image = Image.FromFile(placeholder);
                }
                catch { }
            }
            pic.Click += (s, e) => OpenDetailsPage(id);

            System.Windows.Forms.Label heart = new System.Windows.Forms.Label { Text = "♥", Font = new Font("Segoe UI", 14), ForeColor = Color.White, BackColor = Color.Transparent, Location = new Point(280, 10), Cursor = Cursors.Hand };
            pic.Controls.Add(heart);

            System.Windows.Forms.Panel pnlDetails = new System.Windows.Forms.Panel { Dock = DockStyle.Fill, Padding = new Padding(15) };
            pnlDetails.Click += (s, e) => OpenDetailsPage(id);

            System.Windows.Forms.Label lblTitle = new System.Windows.Forms.Label { Text = title, Font = new Font("Segoe UI", 12, FontStyle.Bold), AutoSize = true, Location = new Point(10, 10) };
            System.Windows.Forms.Label lblRating = new System.Windows.Forms.Label { Text = "★ " + rating, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.Black, AutoSize = true, Location = new Point(250, 12) };
            System.Windows.Forms.Label lblLoc = new System.Windows.Forms.Label { Text = location, Font = new Font("Segoe UI", 9), ForeColor = Color.Gray, AutoSize = true, Location = new Point(10, 35) };
            System.Windows.Forms.Label lblPrice = new System.Windows.Forms.Label { Text = price, Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.Black, AutoSize = true, Location = new Point(10, 90) };
            System.Windows.Forms.Label lblMonth = new System.Windows.Forms.Label { Text = " / month", Font = new Font("Segoe UI", 10), ForeColor = Color.Gray, AutoSize = true, Location = new Point(10 + lblPrice.PreferredWidth, 95) };

            pnlDetails.Controls.Add(lblMonth); pnlDetails.Controls.Add(lblPrice); pnlDetails.Controls.Add(lblLoc); pnlDetails.Controls.Add(lblRating); pnlDetails.Controls.Add(lblTitle);

            if (status == "Rented")
            {
                AntdUI.Button btnBooked = new AntdUI.Button
                {
                    Text = "BOOKED",
                    Type = TTypeMini.Error,
                    Size = new Size(80, 26),
                    Location = new Point(10, 10),
                    Radius = 4,
                    Cursor = Cursors.Arrow // Non-clickable look
                };
                pic.Controls.Add(btnBooked);
            }

            AntdUI.Button btnView = new AntdUI.Button
            {
                Text = "View Details",
                Type = TTypeMini.Primary,
                Ghost = true,
                Size = new Size(290, 40),
                Location = new Point(15, 125),
                Radius = 8
            };
            btnView.Click += (s, e) => OpenDetailsPage(id);
            pnlDetails.Controls.Add(btnView);

            card.Controls.Add(pnlDetails); card.Controls.Add(pic);
            flowListings.Controls.Add(card);
        }

        private void OpenDetailsPage(int propertyId)
        {
            pnlDetailsHost.Controls.Clear();

            PropertyDetails details = new PropertyDetails(propertyId);
            details.TopLevel = false;
            details.FormBorderStyle = FormBorderStyle.None;
            details.Dock = DockStyle.Fill;

            details.BackRequested += (s, e) =>
            {
                pnlDetailsHost.Visible = false;
                pnlDetailsHost.Controls.Clear();
                _dashboard?.SetSidebarVisibility(true);
                pnlSidebar.Visible = true;

                RefreshEmbeddedLayout();
            };

            pnlDetailsHost.Controls.Add(details);
            details.Show();
            pnlDetailsHost.Visible = true;
            pnlDetailsHost.BringToFront();

            _dashboard?.SetSidebarVisibility(false);
            pnlSidebar.Visible = false;
        }
    }
}