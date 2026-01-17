using System;
using System.Drawing;
using System.Windows.Forms;
using AntdUI;
using RentalSystemUI.Services;
using RentalSystemUI.Models;

namespace RentalSystemUI.Forms.DashboardSections
{
    public partial class RequestList : Form
    {
        private LandlordService _service = new LandlordService();
        private int _landlordId = 1;

        public RequestList()
        {
            InitializeComponent();
            LoadRequests();
        }

        private void LoadRequests()
        {
            _flow.Controls.Clear();
            var requests = _service.GetBookings(_landlordId);

            if (requests.Count == 0)
            {
                _flow.Controls.Add(new AntdUI.Label { Text = "No pending requests.", AutoSize = true, ForeColor = Styles.TextGray, Font = Styles.SubHeader });
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
            AntdUI.Label lblAddr = new AntdUI.Label { Text = req.PropertyAddress, ForeColor = Styles.TextGray, Location = new Point(350, 45), AutoSize = true, Font = Styles.Small };

            if (req.Status != "Pending")
            {
                AntdUI.Button statusBadge = new AntdUI.Button
                {
                    Text = req.Status.ToUpper(),
                    Type = TTypeMini.Default,
                    BackColor = (req.Status == "Approved" ? Styles.GreenBg : Styles.RedBg),
                    ForeColor = (req.Status == "Approved" ? Styles.GreenTxt : Styles.RedTxt),
                    BorderWidth = 0,
                    Location = new Point(card.Width - 140, 20),
                    Size = new Size(100, 30),
                    Radius = 6,
                    Anchor = AnchorStyles.Top | AnchorStyles.Right,
                    Font = Styles.Bold
                };
                card.Controls.Add(statusBadge);
            }

            if (req.Status == "Pending")
            {
                AntdUI.Button btnAccept = new AntdUI.Button { Text = "Approve", Type = TTypeMini.Primary, BackColor = Styles.Blue, ForeColor = Color.White, Location = new Point(card.Width - 340, 80), Size = new Size(150, 40), Radius = 8, Anchor = AnchorStyles.Top | AnchorStyles.Right };
                btnAccept.Click += (s, e) => { _service.ApproveBooking(req.BookingID); LoadRequests(); };

                AntdUI.Button btnReject = new AntdUI.Button { Text = "Reject", Type = TTypeMini.Default, BackColor = Color.White, BorderWidth = 1, ForeColor = Styles.TextGray, Location = new Point(card.Width - 170, 80), Size = new Size(130, 40), Radius = 8, Anchor = AnchorStyles.Top | AnchorStyles.Right };
                btnReject.Click += (s, e) => { _service.RejectBooking(req.BookingID); LoadRequests(); };

                card.Controls.Add(btnReject);
                card.Controls.Add(btnAccept);
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
