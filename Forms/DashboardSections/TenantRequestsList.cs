using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AntdUI;
using RentalSystemUI.Models;
using RentalSystemUI.Services;

namespace RentalSystemUI.Forms.DashboardSections
{
    public class TenantRequestsList : Form
    {
        private readonly int _tenantId;
        private readonly TenantService _tenantService = new TenantService();

        private readonly FlowLayoutPanel _flow = new FlowLayoutPanel();
        private HashSet<int> _unpaidApprovedBookingIds = new HashSet<int>();

        public TenantRequestsList(int tenantId)
        {
            _tenantId = tenantId;

            BackColor = ColorTranslator.FromHtml("#f6f7f8");

            _flow.Dock = DockStyle.Fill;
            _flow.FlowDirection = FlowDirection.TopDown;
            _flow.WrapContents = false;
            _flow.AutoScroll = true;
            _flow.Padding = new Padding(0, 0, 0, 10);

            Controls.Add(_flow);

            Load += (s, e) => LoadData();
        }

        private void LoadData()
        {
            _flow.Controls.Clear();

            var bookings = _tenantService.GetBookingsByTenant(_tenantId);
            _unpaidApprovedBookingIds = _tenantService
                .GetApprovedUnpaidBookings(_tenantId)
                .Select(x => x.BookingID)
                .ToHashSet();

            if (bookings.Count == 0)
            {
                _flow.Controls.Add(new AntdUI.Label
                {
                    Text = "No bookings yet. Start by browsing homes!",
                    AutoSize = true,
                    ForeColor = Styles.TextGray,
                    Font = Styles.SubHeader,
                    Margin = new Padding(0, 0, 0, 10)
                });
                return;
            }

            foreach (var b in bookings.OrderByDescending(x => x.CreatedAt))
            {
                _flow.Controls.Add(CreateBookingCard(b));
            }
        }

        private Control CreateBookingCard(BookingWithProperty b)
        {
            AntdUI.Panel card = new AntdUI.Panel
            {
                Height = 130,
                Width = 1000,
                Radius = 15,
                Shadow = 5,
                Margin = new Padding(0, 0, 0, 15),
                BackColor = Color.White
            };

            var status = (b.Status ?? "Pending").Trim();
            Color stripeColor = status switch
            {
                "Approved" => Styles.GreenBg,
                "Rejected" => Styles.RedBg,
                _ => Styles.OrangeBg
            };

            card.Controls.Add(new System.Windows.Forms.Panel { Dock = DockStyle.Left, Width = 8, BackColor = stripeColor });

            var lblTitle = new AntdUI.Label
            {
                Text = string.IsNullOrWhiteSpace(b.PropertyTitle) ? $"Property #{b.PropertyID}" : b.PropertyTitle,
                Font = Styles.CardTitle,
                ForeColor = Styles.DarkBlue,
                Location = new Point(30, 18),
                AutoSize = true
            };

            var lblDates = new AntdUI.Label
            {
                Text = $"{b.StartDate:dd MMM yyyy} ? {b.EndDate:dd MMM yyyy}",
                ForeColor = Styles.TextGray,
                Location = new Point(30, 48),
                AutoSize = true,
                Font = Styles.Small
            };

            var lblStatus = new AntdUI.Label
            {
                Text = $"Status: {status}",
                ForeColor = Styles.TextGray,
                Location = new Point(30, 72),
                AutoSize = true,
                Font = Styles.Small
            };

            if (string.Equals(status, "Approved", StringComparison.OrdinalIgnoreCase))
            {
                bool isUnpaid = _unpaidApprovedBookingIds.Contains(b.BookingID);
                if (isUnpaid)
                {
                    var btnPay = new AntdUI.Button
                    {
                        Text = "Pay Now",
                        Type = TTypeMini.Primary,
                        Radius = 8,
                        Size = new Size(160, 40),
                        Location = new Point(card.Width - 190, 42),
                        Anchor = AnchorStyles.Top | AnchorStyles.Right
                    };

                    btnPay.Click += (s, e) =>
                    {
                        using (var payment = new RentalSystemUI.Forms.Payment(b.BookingID))
                        {
                            payment.ShowDialog(this);
                        }
                        LoadData();
                    };

                    card.Controls.Add(btnPay);
                }
                else
                {
                    var badge = new AntdUI.Button
                    {
                        Text = "PAID",
                        Type = TTypeMini.Default,
                        BackColor = Styles.GreenBg,
                        ForeColor = Styles.GreenTxt,
                        BorderWidth = 0,
                        Radius = 6,
                        Size = new Size(120, 32),
                        Location = new Point(card.Width - 150, 48),
                        Anchor = AnchorStyles.Top | AnchorStyles.Right,
                        Font = Styles.Bold
                    };
                    card.Controls.Add(badge);
                }
            }
            else if (string.Equals(status, "Pending", StringComparison.OrdinalIgnoreCase))
            {
                var badge = new AntdUI.Button
                {
                    Text = "WAITING FOR APPROVAL",
                    Type = TTypeMini.Default,
                    BackColor = Styles.OrangeBg,
                    ForeColor = Styles.OrangeTxt,
                    BorderWidth = 0,
                    Radius = 6,
                    Size = new Size(220, 32),
                    Location = new Point(card.Width - 250, 48),
                    Anchor = AnchorStyles.Top | AnchorStyles.Right,
                    Font = Styles.Bold
                };
                card.Controls.Add(badge);
            }
            else
            {
                var badge = new AntdUI.Button
                {
                    Text = "REJECTED",
                    Type = TTypeMini.Default,
                    BackColor = Styles.RedBg,
                    ForeColor = Styles.RedTxt,
                    BorderWidth = 0,
                    Radius = 6,
                    Size = new Size(140, 32),
                    Location = new Point(card.Width - 170, 48),
                    Anchor = AnchorStyles.Top | AnchorStyles.Right,
                    Font = Styles.Bold
                };
                card.Controls.Add(badge);
            }

            card.Controls.Add(lblTitle);
            card.Controls.Add(lblDates);
            card.Controls.Add(lblStatus);

            return card;
        }
    }
}
