using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AntdUI;
using Microsoft.Data.SqlClient;
using RentalSystemUI.Controllers;

namespace RentalSystemUI.Forms
{
    public partial class PropertyDetails : Form
    {
        private int _propertyId;

        // --- CUSTOM EVENT: Tell parent to close me ---
        public event EventHandler? BackRequested;

        public PropertyDetails(int propertyId)
        {
            InitializeComponent();
            _propertyId = propertyId;
            LoadPropertyData();
            AddDummyAmenities();
        }

        private void LoadPropertyData()
        {
            try
            {
                DatabaseHelper db = new DatabaseHelper();
                // 1. GET DETAILS
                string queryProps = "SELECT Title, Address, City, RentAmount, Description FROM PROPERTIES WHERE PropertyID = @pid";
                SqlParameter[] p1 = { new SqlParameter("@pid", _propertyId) };

                DataTable dt = db.ExecuteQuery(queryProps, p1);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    lblTitle.Text = row["Title"]?.ToString() ?? "Unknown Title";
                    lblSubHeader.Text = "★ 4.98 (124 reviews)  •  " + (row["Address"]?.ToString() ?? "") + ", " + (row["City"]?.ToString() ?? "");
                    lblDescription.Text = row["Description"]?.ToString() ?? "No description available.";

                    decimal rent = 0;
                    decimal.TryParse(row["RentAmount"]?.ToString(), out rent);
                    lblPriceLarge.Text = "$" + rent.ToString("N0");
                    lblTotalValue.Text = "$" + (rent + 150).ToString("N0");
                }

                // 2. GET IMAGES
                string queryImages = "SELECT ImagePath FROM PROPERTY_IMAGES WHERE PropertyID = @pid";
                SqlParameter[] p2 = { new SqlParameter("@pid", _propertyId) };
                DataTable dtImages = db.ExecuteQuery(queryImages, p2);

                if (dtImages.Rows.Count > 0) SetImage(picMain, dtImages.Rows[0]["ImagePath"]?.ToString() ?? "");
                if (dtImages.Rows.Count > 1) SetImage(picSub1, dtImages.Rows[1]["ImagePath"]?.ToString() ?? "");
                if (dtImages.Rows.Count > 2) SetImage(picSub2, dtImages.Rows[2]["ImagePath"]?.ToString() ?? "");
                if (dtImages.Rows.Count > 3) SetImage(picSub3, dtImages.Rows[3]["ImagePath"]?.ToString() ?? "");
                if (dtImages.Rows.Count > 4) SetImage(picSub4, dtImages.Rows[4]["ImagePath"]?.ToString() ?? "");
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