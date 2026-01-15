using System;
using System.Drawing;
using System.Windows.Forms;
using AntdUI;
using RentalSystemUI.Services;

namespace RentalSystemUI.Forms.DashboardSections
{
    public class RequestList : UserControl
    {
        private LandlordService _service = new LandlordService();
        private int _landlordId = 1;
        private FlowLayoutPanel _flow = null!;

        public RequestList()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.FromArgb(245, 247, 250); // Light BG
            InitializeUI();
            LoadRequests();
        }

        private void InitializeUI()
        {
            // Title & Filter Bar
            var header = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 60, FlowDirection = FlowDirection.LeftToRight, Padding = new Padding(0, 10, 0, 0) };
            
            AntdUI.Label lblTitle = new AntdUI.Label { Text = "Tenant Requests", Font = new Font("Segoe UI", 18, FontStyle.Bold), AutoSize = true, Margin = new Padding(0, 10, 20, 0) };
            
            AntdUI.Button btnAll = new AntdUI.Button { Text = "All Requests", Type = TTypeMini.Primary, BackColor = Color.FromArgb(38, 38, 38), ForeColor = Color.White, Radius = 20, Size = new Size(120, 36) };
            AntdUI.Button btnPending = new AntdUI.Button { Text = "Pending", Type = TTypeMini.Default, Radius = 20, Size = new Size(100, 36), BorderWidth = 1, BackColor = Color.White };
            AntdUI.Button btnAccepted = new AntdUI.Button { Text = "Accepted", Type = TTypeMini.Default, Radius = 20, Size = new Size(100, 36), BorderWidth = 1, BackColor = Color.White };
            AntdUI.Button btnRejected = new AntdUI.Button { Text = "Rejected", Type = TTypeMini.Default, Radius = 20, Size = new Size(100, 36), BorderWidth = 1, BackColor = Color.White };

            header.Controls.Add(lblTitle);
            header.Controls.Add(btnAll);
            header.Controls.Add(btnPending);
            header.Controls.Add(btnAccepted);
            header.Controls.Add(btnRejected);

            this.Controls.Add(header);

            _flow = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoScroll = true, Padding = new Padding(0, 20, 20, 20) };
            this.Controls.Add(_flow);
        }

        private void LoadRequests()
        {
            _flow.Controls.Clear();
            var requests = _service.GetApplications(_landlordId);

            if (requests.Count == 0)
            {
                _flow.Controls.Add(new AntdUI.Label { Text = "No requests found.", AutoSize = true, ForeColor = Color.Gray });
                return;
            }

            // Group logic
            var lastProp = "";
            foreach (var req in requests)
            {
                if (req.PropertyTitle != lastProp)
                {
                    AntdUI.Label propHeader = new AntdUI.Label { Text = "🏢 " + req.PropertyTitle, Font = new Font("Segoe UI", 14, FontStyle.Bold), Width = 800, Height = 40, Margin = new Padding(0, 20, 0, 10) };
                    _flow.Controls.Add(propHeader);
                    lastProp = req.PropertyTitle;
                }
                _flow.Controls.Add(CreateRequestCard(req));
            }
        }

        private AntdUI.Panel CreateRequestCard(RentalSystemUI.Models.Application req)
        {
            // Clean White Card
            AntdUI.Panel card = new AntdUI.Panel { Size = new Size(500, 240), Radius = 12, Shadow = 4, Margin = new Padding(0, 0, 30, 30), BackColor = Color.White };

            // 1. Header: Avatar + Name + Pending Badge
            AntdUI.Avatar avatar = new AntdUI.Avatar { Text = req.TenantName, Size = new Size(50, 50), Location = new Point(20, 20), BackColor = Color.FromArgb(222, 226, 230), ForeColor = Color.Black }; 
            
            AntdUI.Label lblName = new AntdUI.Label { Text = req.TenantName, Font = new Font("Segoe UI", 12, FontStyle.Bold), Location = new Point(80, 20), AutoSize = true };
            AntdUI.Label lblJob = new AntdUI.Label { Text = "Applicant", ForeColor = Color.Gray, Location = new Point(80, 45), AutoSize = true, Font = new Font("Segoe UI", 9) };
            
            AntdUI.Button statusBadge = new AntdUI.Button { Text = "Pending Review", Type = TTypeMini.Default, BackColor = Color.FromArgb(255, 251, 230), ForeColor = Color.Orange, BorderWidth = 0, Location = new Point(340, 20), Size = new Size(120, 30), Radius = 4 };
            if (req.Status != "Pending") {
                statusBadge.Text = req.Status;
                statusBadge.BackColor = req.Status == "Accepted" ? Color.FromArgb(246, 255, 237) : Color.FromArgb(255, 241, 240);
                statusBadge.ForeColor = req.Status == "Accepted" ? Color.Green : Color.Red;
            }

            card.Controls.Add(statusBadge);
            card.Controls.Add(lblJob);
            card.Controls.Add(lblName);
            card.Controls.Add(avatar);

            // 2. Stats Row
            AntdUI.Panel statsBox = new AntdUI.Panel { Location = new Point(20, 80), Size = new Size(460, 60), BackColor = Color.FromArgb(249, 250, 251), Radius = 6 };
            AddStat(statsBox, "Credit Score", "720", 0);
            AddStat(statsBox, "Monthly Income", "$8,500", 150);
            AddStat(statsBox, "Pets", "None", 300);
            card.Controls.Add(statsBox);

            // 3. Action Buttons
            if (req.Status == "Pending")
            {
                AntdUI.Button btnAccept = new AntdUI.Button { Text = "Accept", Type = TTypeMini.Primary, BackColor = Color.FromArgb(22, 119, 255), ForeColor = Color.White, Location = new Point(20, 160), Size = new Size(220, 45), Radius = 8 };
                btnAccept.Click += (s, e) => { _service.ApproveApplication(req.ApplicationID); LoadRequests(); };

                AntdUI.Button btnReject = new AntdUI.Button { Text = "Reject", Type = TTypeMini.Default, BackColor = Color.White, BorderWidth = 1, Location = new Point(250, 160), Size = new Size(180, 45), Radius = 8 };
                btnReject.Click += (s, e) => { _service.RejectApplication(req.ApplicationID); LoadRequests(); };

                card.Controls.Add(btnReject);
                card.Controls.Add(btnAccept);
            }
            else
            {
                AntdUI.Label lblDecide = new AntdUI.Label { Text = "Decision: " + req.Status, ForeColor = Color.Gray, Location = new Point(20, 170), AutoSize = true };
                card.Controls.Add(lblDecide);
            }

            return card;
        }

        private void AddStat(AntdUI.Panel parent, string label, string value, int x)
        {
            AntdUI.Label l = new AntdUI.Label { Text = label, ForeColor = Color.Gray, Font = new Font("Segoe UI", 8), Location = new Point(x + 20, 10), AutoSize = true };
            AntdUI.Label v = new AntdUI.Label { Text = value, ForeColor = Color.Black, Font = new Font("Segoe UI", 10, FontStyle.Bold), Location = new Point(x + 20, 30), AutoSize = true };
            parent.Controls.Add(v);
            parent.Controls.Add(l);
        }
    }
}
