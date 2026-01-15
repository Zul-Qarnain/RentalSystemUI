using System;
using System.Drawing;
using System.Windows.Forms;
using AntdUI;
using RentalSystemUI.Services;

namespace RentalSystemUI.Forms.DashboardSections
{
    public class PaymentList : UserControl
    {
        private LandlordService _service = new LandlordService();
        private int _landlordId = 1;
        private FlowLayoutPanel _flow = null!;

        public PaymentList()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.FromArgb(245, 247, 250);
            InitializeUI();
            LoadData();
        }

        private void InitializeUI()
        {
            AntdUI.Label lblTitle = new AntdUI.Label { Text = "Payments & Transactions", Font = new Font("Segoe UI", 20, FontStyle.Bold), Dock = DockStyle.Top, Height = 60, Padding = new Padding(20, 15, 0, 0), ForeColor = Color.FromArgb(38, 38, 38) };
            this.Controls.Add(lblTitle);

            _flow = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoScroll = true, Padding = new Padding(20) };
            this.Controls.Add(_flow);
        }

        private void LoadData()
        {
            _flow.Controls.Clear();
            var payments = _service.GetPayments(_landlordId);

            if (payments.Count == 0)
            {
                _flow.Controls.Add(new AntdUI.Label { Text = "No recent transactions.", AutoSize = true, ForeColor = Color.Gray });
                return;
            }

            foreach (var p in payments)
            {
                _flow.Controls.Add(CreatePaymentCard(p));
            }
        }

        private AntdUI.Panel CreatePaymentCard(RentalSystemUI.Models.Payment p)
        {
            AntdUI.Panel card = new AntdUI.Panel { Size = new Size(800, 110), Radius = 12, Shadow = 6, Margin = new Padding(0, 0, 0, 15), BackColor = Color.White };

            // Status Indicator Stripe (Left)
            Color stripeColor = p.Status == "Verified" ? Color.FromArgb(82, 196, 26) : (p.Status == "Pending" ? Color.FromArgb(250, 173, 20) : Color.FromArgb(255, 77, 79));
            AntdUI.Panel stripe = new AntdUI.Panel { Dock = DockStyle.Left, Width = 6, BackColor = stripeColor };
            card.Controls.Add(stripe);

            // Amount (Big, Bold)
            AntdUI.Label lblAmount = new AntdUI.Label { Text = $"${p.Amount:N0}", Font = new Font("Segoe UI", 18, FontStyle.Bold), ForeColor = Color.FromArgb(38, 38, 38), Location = new Point(30, 25), AutoSize = true };
            AntdUI.Label lblDue = new AntdUI.Label { Text = "Due: " + p.DueDate.ToShortDateString(), ForeColor = Color.Gray, Location = new Point(30, 60), AutoSize = true, Font = new Font("Segoe UI", 9) };
            
            // Details
            AntdUI.Label lblTenant = new AntdUI.Label { Text = p.TenantName, Font = new Font("Segoe UI", 12, FontStyle.Bold), Location = new Point(200, 25), AutoSize = true };
            AntdUI.Label lblProp = new AntdUI.Label { Text = p.PropertyTitle, ForeColor = Color.Gray, Location = new Point(200, 50), AutoSize = true };

            card.Controls.Add(lblProp);
            card.Controls.Add(lblTenant);
            card.Controls.Add(lblDue);
            card.Controls.Add(lblAmount);

            // Actions for Pending
            if (p.Status == "Pending")
            {
                AntdUI.Button btnVerify = new AntdUI.Button { Text = "Verify", Type = TTypeMini.Primary, Location = new Point(580, 35), Size = new Size(90, 40), Radius = 6, BackColor = Color.FromArgb(82, 196, 26), BorderWidth = 0 };
                btnVerify.Click += (s, e) => { _service.VerifyPayment(p.PaymentID); LoadData(); };

                AntdUI.Button btnReject = new AntdUI.Button { Text = "Reject", Type = TTypeMini.Default, ForeColor = Color.Red, Location = new Point(680, 35), Size = new Size(90, 40), Radius = 6 };
                btnReject.Click += (s, e) => { _service.RejectPayment(p.PaymentID); LoadData(); };

                card.Controls.Add(btnVerify);
                card.Controls.Add(btnReject);
            }
            else
            {
                AntdUI.Button badge = new AntdUI.Button { Text = p.Status.ToUpper(), Type = TTypeMini.Default, Location = new Point(600, 40), Size = new Size(150, 30), BorderWidth = 0, ForeColor = stripeColor, BackColor = Color.Transparent, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
                card.Controls.Add(badge);
            }

            return card;
        }
    }
}
