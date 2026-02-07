using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AntdUI;
using RentalSystemUI.Services;
using RentalSystemUI.Models;

namespace RentalSystemUI.Forms.DashboardSections
{
    public class RefundList : Form
    {
        private readonly LandlordService _service = new LandlordService();
        private int LandlordId => AppSession.CurrentUser?.UserID ?? 0;
        private FlowLayoutPanel _flow;

        public RefundList()
        {
            BackColor = ColorTranslator.FromHtml("#f6f7f8");
            Padding = new Padding(20);

            _flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true
            };
            Controls.Add(_flow);

            Load += (s, e) => LoadData();
        }

        private void LoadData()
        {
            _flow.Controls.Clear();
            var requests = _service.GetRefundRequests(LandlordId);

            if (requests.Count == 0)
            {
                _flow.Controls.Add(new AntdUI.Label
                {
                    Text = "No pending refund requests.",
                    AutoSize = true,
                    ForeColor = Styles.TextGray,
                    Font = Styles.SubHeader
                });
                return;
            }

            foreach (var req in requests)
            {
                _flow.Controls.Add(CreateCard(req));
            }
        }

        private Control CreateCard(RefundRequestModel req)
        {
            var card = new AntdUI.Panel
            {
                Width = 880,
                Height = 160,
                Radius = 12,
                Shadow = 4,
                Margin = new Padding(0, 0, 0, 20),
                BackColor = Color.White
            };

            // Left Side Info
            var lblTitle = new AntdUI.Label { Text = req.PropertyTitle, Font = Styles.Bold, ForeColor = Styles.DarkBlue, Location = new Point(25, 20), AutoSize = true };
            var lblTenant = new AntdUI.Label { Text = "Tenant: " + req.TenantName, ForeColor = Styles.TextGray, Location = new Point(25, 55), AutoSize = true };
            var lblReason = new AntdUI.Label { Text = "Reason: " + req.Reason, ForeColor = Styles.TextGray, Location = new Point(25, 85), AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Italic) };
            
            // Right Side Actions
            // Fixed positions to ensure visibility and alignment
            var lblAmount = new AntdUI.Label { Text = $"Refund: ৳{req.Amount:N0}", Font = Styles.Header, ForeColor = Styles.RedTxt, Location = new Point(550, 25), AutoSize = true };

            var btnApprove = new AntdUI.Button
            {
                Text = "Approve",
                Type = TTypeMini.Primary,
                BackColor = Styles.GreenBg,
                ForeColor = Styles.GreenTxt,
                Location = new Point(550, 80),
                Size = new Size(120, 45),
                Radius = 6
            };
            btnApprove.Click += (s, e) =>
            {
                if (MessageBox.Show("Approve this refund? Payment status will be set to Refunded.", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    _service.ApproveRefund(req.RefundRequestID);
                    LoadData();
                }
            };

            var btnReject = new AntdUI.Button
            {
                Text = "Reject",
                Type = TTypeMini.Default,
                Location = new Point(690, 80),
                Size = new Size(100, 45),
                Radius = 6
            };
            btnReject.Click += (s, e) =>
            {
                _service.RejectRefund(req.RefundRequestID);
                LoadData();
            };

            card.Controls.Add(btnReject);
            card.Controls.Add(btnApprove);
            card.Controls.Add(lblAmount);
            card.Controls.Add(lblReason);
            card.Controls.Add(lblTenant);
            card.Controls.Add(lblTitle);

            return card;
        }
    }
}
