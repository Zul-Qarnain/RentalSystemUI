using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using RentalSystemUI.Data;
using RentalSystemUI.Services;

namespace RentalSystemUI.Forms
{
    public partial class Payment : Form
    {
        private readonly int? _bookingId;
        private readonly TenantService _tenantService = new TenantService();

        private decimal _amount;

        public Payment() : this(null)
        {
        }

        public Payment(int? bookingId)
        {
            _bookingId = bookingId;

            InitializeComponent();
            this.Size = new Size(1280, 800);
            this.MinimumSize = new Size(1280, 800);
            this.MaximumSize = new Size(1280, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            SetupUIComponents();

            if (btnPay != null)
            {
                btnPay.Click -= BtnPay_Click;
                btnPay.Click += BtnPay_Click;
            }
        }

        private void BtnPay_Click(object? sender, EventArgs e)
        {
            if (!_bookingId.HasValue)
            {
                AntdUI.Message.error(this, "Missing booking.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtNumber.Text))
            {
                AntdUI.Message.error(this, "Please fill card holder name and card number.");
                return;
            }

            var method = "Card";
            var tx = Guid.NewGuid().ToString("N").Substring(0, 12).ToUpperInvariant();

            int paymentId = _tenantService.CreatePaymentForBooking(_bookingId.Value, _amount, method, tx);
            if (paymentId <= 0)
            {
                AntdUI.Message.error(this, "Payment failed.");
                return;
            }

            AntdUI.Message.success(this, $"Payment completed (ID: {paymentId}).");
            Close();
        }

        private void SetupUIComponents()
        {
            try { tabMethods.Text = "Credit Card"; } catch { }

            txtName.PlaceholderText = "Card Holder Name";
            txtNumber.PlaceholderText = "0000 0000 0000 0000";
            txtExpiry.PlaceholderText = "MM / YY";
            txtCVC.PlaceholderText = "123";

            LoadBookingSummary();
        }

        private void LoadBookingSummary()
        {
            cardSummary.Controls.Clear();

            if (!_bookingId.HasValue)
            {
                _amount = 0;
                AddSummaryContent("Booking", "Total Due: ৳0");
                return;
            }

            try
            {
                using var conn = new Database().GetConnection();
                conn.Open();

                using var cmd = new SqlCommand(@"
                    SELECT b.BookingID, b.TotalAmount, b.StartDate, b.EndDate, b.DurationMonths,
                           p.Title, p.RentAmount
                    FROM BOOKINGS b
                    JOIN PROPERTIES p ON b.PropertyID = p.PropertyID
                    WHERE b.BookingID=@bid", conn);

                cmd.Parameters.AddWithValue("@bid", _bookingId.Value);
                using var r = cmd.ExecuteReader();
                if (!r.Read())
                {
                    _amount = 0;
                    AddSummaryContent("Booking", "Total Due: ৳0");
                    return;
                }

                _amount = (decimal)r["TotalAmount"]; 
                var title = r["Title"].ToString() ?? $"Booking #{_bookingId.Value}";
                var months = r["DurationMonths"] as int? ?? 1;
                var rent = (decimal)r["RentAmount"];

                // Property Image Placeholder
                var pic = new System.Windows.Forms.PictureBox
                {
                    Size = new Size(340, 180),
                    Location = new Point(20, 20),
                    BackColor = Color.FromArgb(235, 235, 235),
                    SizeMode = PictureBoxSizeMode.StretchImage
                };
                cardSummary.Controls.Add(pic);

                var lblTitle = new AntdUI.Label
                {
                    Text = title,
                    Font = new Font("Segoe UI Semibold", 13),
                    Location = new Point(20, 215),
                    Size = new Size(340, 30)
                };
                cardSummary.Controls.Add(lblTitle);

                var lblLine1 = new AntdUI.Label { Text = $"Rent: ৳{rent:N0} / month", Location = new Point(20, 260), Size = new Size(340, 25), ForeColor = Color.Gray };
                var lblLine2 = new AntdUI.Label { Text = $"Duration: {months} month(s)", Location = new Point(20, 290), Size = new Size(340, 25), ForeColor = Color.Gray };
                cardSummary.Controls.Add(lblLine1);
                cardSummary.Controls.Add(lblLine2);

                var lblTotalDue = new AntdUI.Label
                {
                    Text = $"Total Due: ৳{_amount:N0}",
                    Font = new Font("Segoe UI", 16, FontStyle.Bold),
                    ForeColor = Color.FromArgb(22, 119, 255),
                    Location = new Point(20, 430),
                    Size = new Size(340, 40)
                };
                cardSummary.Controls.Add(lblTotalDue);
            }
            catch
            {
                _amount = 0;
                AddSummaryContent("Booking", "Total Due: ৳0");
            }
        }

        private void AddSummaryContent(string titleText, string totalText)
        {
            var pic = new System.Windows.Forms.PictureBox
            {
                Size = new Size(340, 180),
                Location = new Point(20, 20),
                BackColor = Color.FromArgb(235, 235, 235),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            cardSummary.Controls.Add(pic);

            var lblTitle = new AntdUI.Label
            {
                Text = titleText,
                Font = new Font("Segoe UI Semibold", 13),
                Location = new Point(20, 215),
                Size = new Size(340, 30)
            };
            cardSummary.Controls.Add(lblTitle);

            var lblTotalDue = new AntdUI.Label
            {
                Text = totalText,
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