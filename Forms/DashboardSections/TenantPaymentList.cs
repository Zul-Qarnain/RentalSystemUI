using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AntdUI;
using RentalSystemUI.Services;
using RentalSystemUI.Models;
using PaymentModel = RentalSystemUI.Models.Payment;

namespace RentalSystemUI.Forms.DashboardSections
{
    public class TenantPaymentList : Form
    {
        private readonly int _tenantId;
        private readonly TenantService _service = new TenantService();

        private readonly FlowLayoutPanel _flow = new FlowLayoutPanel();
        private readonly AntdUI.Label _lblSummary = new AntdUI.Label();

        public TenantPaymentList(int tenantId)
        {
            _tenantId = tenantId;

            BackColor = ColorTranslator.FromHtml("#f6f7f8");
            Padding = new Padding(10);

            _lblSummary.AutoSize = true;
            _lblSummary.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            _lblSummary.ForeColor = Styles.DarkBlue;
            _lblSummary.Margin = new Padding(0, 0, 0, 12);

            _flow.Dock = DockStyle.Fill;
            _flow.FlowDirection = FlowDirection.TopDown;
            _flow.WrapContents = false;
            _flow.AutoScroll = true;

            var container = new System.Windows.Forms.Panel { Dock = DockStyle.Fill, BackColor = BackColor };
            container.Controls.Add(_flow);

            Controls.Add(container);
            Controls.Add(_lblSummary);

            Load += (s, e) => LoadData();
        }

        private void LoadData()
        {
            _flow.Controls.Clear();

            // 1) Unpaid approved bookings
            var unpaid = _service.GetApprovedUnpaidBookings(_tenantId);
            if (unpaid.Count > 0)
            {
                _flow.Controls.Add(new AntdUI.Label
                {
                    Text = "Unpaid Bills",
                    Font = Styles.CardTitle,
                    ForeColor = Styles.DarkBlue,
                    AutoSize = true,
                    Margin = new Padding(0, 0, 0, 8)
                });

                foreach (var b in unpaid)
                {
                    _flow.Controls.Add(CreateUnpaidBookingCard(b));
                }

                _flow.Controls.Add(new System.Windows.Forms.Panel { Height = 10, Width = 10 });
            }

            // 2) Payments summary + history
            var summary = _service.GetPaymentSummary(_tenantId);
            _lblSummary.Text = $"Paid: {summary.PaidCount} (৳{summary.PaidTotal:N0})";

            var payments = _service.GetPayments(_tenantId);
            if (payments.Count == 0)
            {
                _flow.Controls.Add(new AntdUI.Label
                {
                    Text = "No payment history yet.",
                    AutoSize = true,
                    ForeColor = Styles.TextGray,
                    Font = Styles.SubHeader
                });
                return;
            }

            _flow.Controls.Add(new AntdUI.Label
            {
                Text = "Payment History",
                Font = Styles.CardTitle,
                ForeColor = Styles.TextGray,
                AutoSize = true,
                Margin = new Padding(0, 10, 0, 8)
            });

            foreach (var p in payments.OrderByDescending(x => x.PaymentDate))
            {
                _flow.Controls.Add(CreatePaymentCard(p));
            }
        }

        private Control CreateUnpaidBookingCard(BookingWithProperty b)
        {
            AntdUI.Panel card = new AntdUI.Panel
            {
                Height = 120,
                Width = 1000,
                Radius = 15,
                Shadow = 5,
                Margin = new Padding(0, 0, 0, 15),
                BackColor = Color.White
            };

            var stripe = new System.Windows.Forms.Panel { Dock = DockStyle.Left, Width = 8, BackColor = Styles.OrangeBg };
            card.Controls.Add(stripe);

            var lblTitle = new AntdUI.Label
            {
                Text = b.PropertyTitle,
                Font = Styles.CardTitle,
                ForeColor = Styles.DarkBlue,
                Location = new Point(30, 18),
                AutoSize = true
            };

            var lblMeta = new AntdUI.Label
            {
                Text = $"{b.StartDate:dd MMM yyyy} → {b.EndDate:dd MMM yyyy}  •  {b.DurationMonths ?? 1} month(s)",
                ForeColor = Styles.TextGray,
                Location = new Point(30, 48),
                AutoSize = true,
                Font = Styles.Small
            };

            var lblAmount = new AntdUI.Label
            {
                Text = $"৳{b.TotalAmount:N0}",
                Font = Styles.PageTitle,
                ForeColor = Styles.DarkBlue,
                Location = new Point(30, 74),
                AutoSize = true
            };

            var btnPay = new AntdUI.Button
            {
                Text = "Pay",
                Type = TTypeMini.Primary,
                Radius = 8,
                Size = new Size(140, 40),
                Location = new Point(card.Width - 170, 38),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            btnPay.Click += (s, e) =>
            {
                using (var payment = new RentalSystemUI.Forms.Payment(bookingId: b.BookingID))
                {
                    payment.ShowDialog(this);
                }
                LoadData();
            };

            card.Controls.Add(btnPay);
            card.Controls.Add(lblAmount);
            card.Controls.Add(lblMeta);
            card.Controls.Add(lblTitle);

            return card;
        }

        private Control CreatePaymentCard(PaymentModel p)
        {
            AntdUI.Panel card = new AntdUI.Panel
            {
                Height = 100,
                Width = 1000,
                Radius = 15,
                Shadow = 5,
                Margin = new Padding(0, 0, 0, 15),
                BackColor = Color.White
            };

            Color stripeColor = p.Status == "Verified" ? Styles.GreenBg : Styles.RedBg;
            var stripe = new System.Windows.Forms.Panel { Dock = DockStyle.Left, Width = 8, BackColor = stripeColor };
            card.Controls.Add(stripe);

            AntdUI.Label lblAmount = new AntdUI.Label
            {
                Text = $"৳{p.Amount:N0}",
                Font = Styles.PageTitle,
                ForeColor = Styles.DarkBlue,
                Location = new Point(30, 25),
                AutoSize = true
            };

            var dateText = "Paid: " + (p.PaymentDate?.ToShortDateString() ?? "N/A");
            AntdUI.Label lblDate = new AntdUI.Label
            {
                Text = dateText,
                ForeColor = Styles.TextGray,
                Location = new Point(30, 60),
                AutoSize = true,
                Font = Styles.Small
            };

            AntdUI.Label lblProp = new AntdUI.Label
            {
                Text = p.PropertyTitle,
                ForeColor = Styles.TextGray,
                Location = new Point(260, 40),
                AutoSize = true,
                Font = Styles.Normal
            };

            AntdUI.Button badge = new AntdUI.Button
            {
                Text = (p.Status ?? "").ToUpperInvariant(),
                BackColor = stripeColor,
                ForeColor = (p.Status == "Verified" ? Styles.GreenTxt : Styles.RedTxt),
                Location = new Point(card.Width - 180, 35),
                Size = new Size(150, 30),
                Radius = 6,
                Type = TTypeMini.Default,
                BorderWidth = 0,
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                Font = Styles.Bold
            };

            card.Controls.Add(lblProp);
            card.Controls.Add(lblDate);
            card.Controls.Add(lblAmount);
            card.Controls.Add(badge);

            return card;
        }
    }
}
