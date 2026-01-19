using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AntdUI;
using RentalSystemUI.Services;
using RentalSystemUI.Models;

namespace RentalSystemUI.Forms.DashboardSections
{
    public partial class RequestList : Form
    {
        private readonly LandlordService _service = new LandlordService();
        
        // Use current user's ID instead of hardcoded value (SECURITY FIX)
        private int LandlordId => AppSession.CurrentUser?.UserID ?? 0;

        private string _activeFilter = "All";
        private System.Collections.Generic.List<BookingWithProperty> _allRequests = new System.Collections.Generic.List<BookingWithProperty>();

        public RequestList()
        {
            InitializeComponent();

            WireFilters();
            LoadRequests();
        }

        private void WireFilters()
        {
            if (btnAll != null) btnAll.Click += (s, e) => { SetFilter("All"); };
            if (btnPending != null) btnPending.Click += (s, e) => { SetFilter("Pending"); };
            if (btnApproved != null) btnApproved.Click += (s, e) => { SetFilter("Approved"); };
            if (btnRejected != null) btnRejected.Click += (s, e) => { SetFilter("Rejected"); };

            ApplyFilterButtonStyles();
        }

        private void SetFilter(string filter)
        {
            _activeFilter = filter;
            ApplyFilterButtonStyles();
            RenderRequests();
        }

        private void ApplyFilterButtonStyles()
        {
            void StyleBtn(AntdUI.Button b, bool active)
            {
                if (b == null) return;
                if (active)
                {
                    b.Type = TTypeMini.Primary;
                    b.BackColor = Color.FromArgb(67, 24, 255);
                    b.ForeColor = Color.White;
                }
                else
                {
                    b.Type = TTypeMini.Default;
                    b.BackColor = Color.White;
                    b.ForeColor = Color.FromArgb(163, 174, 208);
                }
            }

            StyleBtn(btnAll, _activeFilter == "All");
            StyleBtn(btnPending, _activeFilter == "Pending");
            StyleBtn(btnApproved, _activeFilter == "Approved");
            StyleBtn(btnRejected, _activeFilter == "Rejected");
        }

        private void LoadRequests()
        {
            _flow.Controls.Clear();
            _allRequests = _service.GetBookings(LandlordId) ?? new System.Collections.Generic.List<BookingWithProperty>();
            RenderRequests();
        }

        private void RenderRequests()
        {
            _flow.Controls.Clear();

            var requests = _allRequests;
            if (_activeFilter != "All")
            {
                requests = requests.Where(r => string.Equals(r.Status, _activeFilter, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (requests.Count == 0)
            {
                var msg = _activeFilter == "All" ? "No bookings yet." : $"No {_activeFilter.ToLowerInvariant()} bookings.";
                _flow.Controls.Add(new AntdUI.Label { Text = msg, AutoSize = true, ForeColor = Styles.TextGray, Font = Styles.SubHeader });
                return;
            }

            var lastProp = "";
            foreach (var req in requests)
            {
                if (req.PropertyTitle != lastProp)
                {
                    AntdUI.Label propHeader = new AntdUI.Label { Text = "🏢 " + req.PropertyTitle, Font = Styles.CardTitle, ForeColor = Styles.TextGray, Width = 800, Height = 40, Margin = new Padding(0, 20, 0, 5) };
                    _flow.Controls.Add(propHeader);
                    lastProp = req.PropertyTitle;
                }
                _flow.Controls.Add(CreateRequestCard(req));
            }
        }

        private AntdUI.Panel CreateRequestCard(BookingWithProperty req)
        {
            AntdUI.Panel card = new AntdUI.Panel
            {
                Width = 1000,
                Height = 140,
                Radius = 15,
                Shadow = 5,
                Margin = new Padding(0, 0, 0, 15),
                BackColor = Color.White
            };

            AntdUI.Avatar avatar = new AntdUI.Avatar { Text = req.TenantName?.Substring(0, 1) ?? "?", Size = new Size(50, 50), Location = new Point(20, 20), BackColor = Styles.LightBlue, ForeColor = Styles.Blue };
            AntdUI.Label lblName = new AntdUI.Label { Text = req.TenantName, Font = Styles.Bold, Location = new Point(85, 20), AutoSize = true, ForeColor = Styles.DarkBlue };
            AntdUI.Label lblMeta = new AntdUI.Label { Text = $"{req.StartDate:dd MMM yyyy} → {req.EndDate:dd MMM yyyy}  •  {req.DurationMonths ?? 1} month(s)", ForeColor = Styles.TextGray, Location = new Point(85, 45), AutoSize = true, Font = Styles.Small };

            AntdUI.Label lblTotal = new AntdUI.Label { Text = $"Total: ৳{req.TotalAmount:N0}", ForeColor = Styles.DarkBlue, Font = Styles.Bold, Location = new Point(350, 20), AutoSize = true };
            AntdUI.Label lblAddr = new AntdUI.Label { Text = req.PropertyAddress, ForeColor = Styles.TextGray, Location = new Point(350, 45), AutoSize = true, Font = Styles.Small, MaximumSize = new Size(400, 0), AutoEllipsis = true };

            if (!string.Equals(req.Status, "Pending", StringComparison.OrdinalIgnoreCase))
            {
                var statusUpper = (req.Status ?? "").ToUpperInvariant();

                Color bg = Styles.RedBg;
                Color fg = Styles.RedTxt;
                if (string.Equals(req.Status, "Approved", StringComparison.OrdinalIgnoreCase)) { bg = Styles.GreenBg; fg = Styles.GreenTxt; }
                else if (string.Equals(req.Status, "Terminated", StringComparison.OrdinalIgnoreCase)) { bg = Styles.OrangeBg; fg = Styles.OrangeTxt; }

                AntdUI.Button statusBadge = new AntdUI.Button
                {
                    Text = statusUpper,
                    Type = TTypeMini.Default,
                    BackColor = bg,
                    ForeColor = fg,
                    BorderWidth = 0,
                    Location = new Point(card.Width - 160, 20),
                    Size = new Size(130, 30),
                    Radius = 6,
                    Anchor = AnchorStyles.Top | AnchorStyles.Right,
                    Font = Styles.Bold
                };
                card.Controls.Add(statusBadge);
            }

            if (string.Equals(req.Status, "Pending", StringComparison.OrdinalIgnoreCase))
            {
                AntdUI.Button btnAccept = new AntdUI.Button { Text = "Approve", Type = TTypeMini.Primary, BackColor = Styles.Blue, ForeColor = Color.White, Location = new Point(card.Width - 340, 80), Size = new Size(150, 40), Radius = 8, Anchor = AnchorStyles.Top | AnchorStyles.Right };
                btnAccept.Click += (s, e) => { _service.ApproveBooking(req.BookingID); LoadRequests(); };

                AntdUI.Button btnReject = new AntdUI.Button { Text = "Reject", Type = TTypeMini.Default, BackColor = Color.White, BorderWidth = 1, ForeColor = Styles.TextGray, Location = new Point(card.Width - 170, 80), Size = new Size(130, 40), Radius = 8, Anchor = AnchorStyles.Top | AnchorStyles.Right };
                btnReject.Click += (s, e) => { _service.RejectBooking(req.BookingID); LoadRequests(); };

                card.Controls.Add(btnReject);
                card.Controls.Add(btnAccept);
            }
            else if (string.Equals(req.Status, "Approved", StringComparison.OrdinalIgnoreCase))
            {
                AntdUI.Button btnTerminate = new AntdUI.Button
                {
                    Text = "Remove Tenant",
                    Type = TTypeMini.Default,
                    BackColor = Color.White,
                    BorderWidth = 1,
                    ForeColor = Styles.OrangeTxt,
                    Location = new Point(card.Width - 220, 80),
                    Size = new Size(180, 40),
                    Radius = 8,
                    Anchor = AnchorStyles.Top | AnchorStyles.Right
                };

                btnTerminate.Click += (s, e) =>
                {
                    if (MessageBox.Show(this, "Terminate this tenant?", "Remove Tenant", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                     {
                         _service.TerminateBooking(req.BookingID);
                         LoadRequests();
                     }
                 };

                card.Controls.Add(btnTerminate);
            }
            else
            {
                AntdUI.Label lblDecided = new AntdUI.Label { Text = "Updated", ForeColor = Styles.TextGray, Font = Styles.Small, Location = new Point(card.Width - 160, 86), AutoSize = true, Anchor = AnchorStyles.Top | AnchorStyles.Right };
                card.Controls.Add(lblDecided);
            }

            card.Controls.Add(lblAddr);
            card.Controls.Add(lblTotal);
            card.Controls.Add(lblMeta);
            card.Controls.Add(lblName);
            card.Controls.Add(avatar);

            return card;
        }
    }
}
