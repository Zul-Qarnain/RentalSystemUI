using System;
using System.Drawing;
using System.Windows.Forms;

namespace RentalSystemUI.Forms
{
    public partial class Payment : Form
    {
        public Payment()
        {
            InitializeComponent();
            SetupUIComponents();
        }

        private void SetupUIComponents()
        {
            // FIX for 'Items' error: Try using 'Pages' which is standard for AntdUI Tabs
            try
            {
                // In AntdUI, we often set tab text like this if .Items doesn't exist
                tabMethods.Text = "Credit Card";
            }
            catch { }

            // Apply Placeholders (using Placeholder property)
            txtName.PlaceholderText = "Card Holder Name";
            txtNumber.PlaceholderText = "0000 0000 0000 0000";
            txtExpiry.PlaceholderText = "MM / YY";
            txtCVC.PlaceholderText = "123";

            // Left Side: Rental Summary Breakdown
            AddSummaryContent();
        }

        private void AddSummaryContent()
        {
            // Property Image Placeholder
            var pic = new System.Windows.Forms.PictureBox
            {
                Size = new Size(340, 180),
                Location = new Point(20, 20),
                BackColor = Color.FromArgb(235, 235, 235),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            cardSummary.Controls.Add(pic);

            // Property Info
            var lblTitle = new AntdUI.Label
            {
                Text = "Sunset Apartments, Unit 4B",
                Font = new Font("Segoe UI Semibold", 13),
                Location = new Point(20, 215),
                Size = new Size(340, 30)
            };
            cardSummary.Controls.Add(lblTitle);

            // List items to match the image exactly
            string[,] costData = { { "Base Rent", "$1,200.00" }, { "Maintenance Fee", "$50.00" }, { "Garbage Collection", "$15.00" } };
            int y = 270;
            for (int i = 0; i < 3; i++)
            {
                var lblKey = new AntdUI.Label { Text = costData[i, 0], Location = new Point(20, y), Size = new Size(180, 25), ForeColor = Color.Gray };
                var lblVal = new AntdUI.Label { Text = costData[i, 1], Location = new Point(240, y), Size = new Size(120, 25), TextAlign = ContentAlignment.TopRight };
                cardSummary.Controls.Add(lblKey);
                cardSummary.Controls.Add(lblVal);
                y += 35;
            }

            // Total Due
            var lblTotalDue = new AntdUI.Label
            {
                Text = "Total Due: $1,265.00",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(22, 119, 255),
                Location = new Point(20, 430),
                Size = new Size(340, 40)
            };
            cardSummary.Controls.Add(lblTotalDue);
        }

        // Draggable window logic
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                this.Capture = false;
                System.Windows.Forms.Message m = System.Windows.Forms.Message.Create(this.Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
                this.WndProc(ref m);
            }
        }

        private void lblNavLinks_Click(object sender, EventArgs e)
        {

        }
    }
}