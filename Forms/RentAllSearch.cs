using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using AntdUI;
using RentalSystemUI.Controllers;

namespace RentalSystemUI.Forms
{
    public partial class RentAllSearch : Form
    {
        public RentAllSearch()
        {
            InitializeComponent();
            LoadRealData();
        }

        private void LoadRealData()
        {
            try
            {
                if (flowListings != null) flowListings.Controls.Clear();
                DatabaseHelper db = new DatabaseHelper();
                string query = @"SELECT p.PropertyID, p.Title, p.Address, p.RentAmount, (SELECT TOP 1 ImagePath FROM PROPERTY_IMAGES pi WHERE pi.PropertyID = p.PropertyID) as CoverImage FROM PROPERTIES p WHERE p.Status = 'Available'";
                DataTable table = db.ExecuteQuery(query);

                if (table == null || table.Rows.Count == 0) return;

                foreach (DataRow row in table.Rows)
                {
                    int id = Convert.ToInt32(row["PropertyID"]);
                    AddProperty(id, row["Title"].ToString() ?? "N/A", row["Address"].ToString() ?? "N/A", "$" + Convert.ToDecimal(row["RentAmount"]).ToString("N0"), "4.8", "", row["CoverImage"].ToString() ?? "");
                }
            }
            catch (Exception ex) { AntdUI.Message.error(this, "Error: " + ex.Message); }
        }

        private void AddProperty(int id, string title, string location, string price, string rating, string badge, string imagePath)
        {
            AntdUI.Panel card = new AntdUI.Panel { Size = new Size(320, 380), Radius = 12, BackColor = Color.White, Margin = new Padding(15), Shadow = 10, ShadowColor = Color.FromArgb(20, 0, 0, 0), Cursor = Cursors.Hand };

            card.Click += (s, e) => OpenDetailsPage(id);

            PictureBox pic = new PictureBox { Dock = DockStyle.Top, Height = 200, BackColor = Color.LightGray, SizeMode = PictureBoxSizeMode.Zoom };
            if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath)) pic.Image = Image.FromFile(imagePath);
            pic.Click += (s, e) => OpenDetailsPage(id);

            System.Windows.Forms.Label heart = new System.Windows.Forms.Label { Text = "♥", Font = new Font("Segoe UI", 14), ForeColor = Color.White, BackColor = Color.Transparent, Location = new Point(280, 10), Cursor = Cursors.Hand };
            pic.Controls.Add(heart);

            System.Windows.Forms.Panel pnlDetails = new System.Windows.Forms.Panel { Dock = DockStyle.Fill, Padding = new Padding(15) };
            pnlDetails.Click += (s, e) => OpenDetailsPage(id);

            System.Windows.Forms.Label lblTitle = new System.Windows.Forms.Label { Text = title, Font = new Font("Segoe UI", 12, FontStyle.Bold), AutoSize = true, Location = new Point(10, 10) };
            System.Windows.Forms.Label lblRating = new System.Windows.Forms.Label { Text = "★ " + rating, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.Black, AutoSize = true, Location = new Point(250, 12) };
            System.Windows.Forms.Label lblLoc = new System.Windows.Forms.Label { Text = location, Font = new Font("Segoe UI", 9), ForeColor = Color.Gray, AutoSize = true, Location = new Point(10, 35) };
            System.Windows.Forms.Label lblPrice = new System.Windows.Forms.Label { Text = price, Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.Black, AutoSize = true, Location = new Point(10, 90) };
            System.Windows.Forms.Label lblMonth = new System.Windows.Forms.Label { Text = "/ month", Font = new Font("Segoe UI", 10), ForeColor = Color.Gray, AutoSize = true, Location = new Point(10 + lblPrice.PreferredWidth, 95) };

            pnlDetails.Controls.Add(lblMonth); pnlDetails.Controls.Add(lblPrice); pnlDetails.Controls.Add(lblLoc); pnlDetails.Controls.Add(lblRating); pnlDetails.Controls.Add(lblTitle);
            card.Controls.Add(pnlDetails); card.Controls.Add(pic);
            flowListings.Controls.Add(card);
        }

        // --- NEW: EMBED LOGIC ---
        private void OpenDetailsPage(int propertyId)
        {
            // 1. Clear old pages if any
            pnlDetailsHost.Controls.Clear();

            // 2. Create the Form but treat it like a control
            PropertyDetails details = new PropertyDetails(propertyId);
            details.TopLevel = false;
            details.FormBorderStyle = FormBorderStyle.None;
            details.Dock = DockStyle.Fill;

            // 3. LISTEN TO THE BACK EVENT
            details.BackRequested += (s, e) =>
            {
                pnlDetailsHost.Visible = false; // Hide the overlay
                pnlDetailsHost.Controls.Clear(); // Clean up memory
            };

            // 4. Add to Panel and Show
            pnlDetailsHost.Controls.Add(details);
            details.Show();
            pnlDetailsHost.BringToFront();
            pnlDetailsHost.Visible = true;
        }

        private void btnMenuHome_Click(object sender, EventArgs e)
        {

        }
    }
}