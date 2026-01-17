using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AntdUI;
using RentalSystemUI.Data;
using RentalSystemUI.Services;

namespace RentalSystemUI.Forms.DashboardSections
{
    public class MyRentals : Form
    {
        private readonly int _tenantId;
        private readonly TenantService _service = new TenantService();

        private readonly FlowLayoutPanel _flow = new FlowLayoutPanel();

        public MyRentals(int tenantId)
        {
            _tenantId = tenantId;

            BackColor = ColorTranslator.FromHtml("#f6f7f8");
            Padding = new Padding(10);

            _flow.Dock = DockStyle.Fill;
            _flow.FlowDirection = FlowDirection.TopDown;
            _flow.WrapContents = false;
            _flow.AutoScroll = true;

            Controls.Add(_flow);

            Load += (s, e) => LoadData();
        }

        private void LoadData()
        {
            _flow.Controls.Clear();

            var rentals = _service.GetRentals(_tenantId);
            if (rentals.Count == 0)
            {
                _flow.Controls.Add(new AntdUI.Label
                {
                    Text = "You have no active rentals.",
                    AutoSize = true,
                    ForeColor = Styles.TextGray,
                    Font = Styles.SubHeader
                });
                return;
            }

            foreach (var r in rentals.OrderByDescending(x => x.CreatedAt))
            {
                _flow.Controls.Add(CreateRentalCard(r));
            }
        }

        private Control CreateRentalCard(TenantRental r)
        {
            var card = new AntdUI.Panel
            {
                Width = 1100,
                Height = 130,
                Radius = 15,
                Shadow = 5,
                Margin = new Padding(0, 0, 0, 15),
                BackColor = Color.White
            };

            // Left stripe
            var stripeColor = r.Status == "Accepted" ? Styles.GreenBg : Styles.OrangeBg;
            if (r.Status == "Cancelled" || r.Status == "Rejected") stripeColor = Styles.RedBg;
            card.Controls.Add(new System.Windows.Forms.Panel { Dock = DockStyle.Left, Width = 8, BackColor = stripeColor });

            // Title
            var lblTitle = new AntdUI.Label
            {
                Text = r.PropertyTitle,
                Font = Styles.CardTitle,
                ForeColor = Styles.DarkBlue,
                Location = new Point(30, 20),
                AutoSize = true
            };

            var lblAddr = new AntdUI.Label
            {
                Text = r.PropertyAddress,
                Font = Styles.Normal,
                ForeColor = Styles.TextGray,
                Location = new Point(30, 48),
                AutoSize = true
            };

            var lblPrice = new AntdUI.Label
            {
                Text = $"?{r.MonthlyRent:N0} / month",
                Font = Styles.Bold,
                ForeColor = Styles.Blue,
                Location = new Point(30, 78),
                AutoSize = true
            };

            var badge = new AntdUI.Button
            {
                Text = r.Status.ToUpperInvariant(),
                BackColor = stripeColor,
                ForeColor = Styles.DarkBlue,
                Location = new Point(card.Width - 190, 20),
                Size = new Size(160, 30),
                Radius = 6,
                Type = TTypeMini.Default,
                BorderWidth = 0,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Font = Styles.Bold
            };

            // Cancel is only allowed for Accepted.
            var btnCancel = new AntdUI.Button
            {
                Text = "Cancel",
                Type = TTypeMini.Default,
                ForeColor = Styles.RedTxt,
                BackColor = Styles.White,
                BorderWidth = 1,
                Radius = 8,
                Size = new Size(120, 36),
                Location = new Point(card.Width - 270, 70),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Enabled = r.Status == "Accepted"
            };
            btnCancel.Click += (s, e) =>
            {
                var result = MessageBox.Show(this, "Cancel this rental?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                {
                    _service.CancelRental(r.BookingId, _tenantId);
                    LoadData();
                }
            };

            // Pay: opens Payment form (existing) and refreshes after
            var btnPay = new AntdUI.Button
            {
                Text = "Pay",
                Type = TTypeMini.Primary,
                Radius = 8,
                Size = new Size(120, 36),
                Location = new Point(card.Width - 140, 70),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Enabled = r.Status == "Accepted"
            };
            btnPay.Click += (s, e) =>
            {
                using (var payment = new RentalSystemUI.Forms.Payment(bookingId: r.BookingId))
                {
                    payment.ShowDialog(this);
                }
            };

            card.Controls.Add(btnPay);
            card.Controls.Add(btnCancel);
            card.Controls.Add(badge);
            card.Controls.Add(lblPrice);
            card.Controls.Add(lblAddr);
            card.Controls.Add(lblTitle);

            return card;
        }
    }
}
