using System;
using System.Drawing;
using System.Windows.Forms;
using AntdUI;
using RentalSystemUI.Services;

namespace RentalSystemUI.Forms.DashboardSections
{
    public partial class PaymentList : Form
    {
        private LandlordService _service = new LandlordService();
        
        // Use current user's ID instead of hardcoded value (SECURITY FIX)
        private int LandlordId => AppSession.CurrentUser?.UserID ?? 0;

        public PaymentList()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            _flow.Controls.Clear();
            var payments = _service.GetPayments(LandlordId);

            if (payments.Count == 0)
            {
                _flow.Controls.Add(new AntdUI.Label { Text = "No recent transactions.", AutoSize = true, ForeColor = Styles.TextGray, Font = Styles.SubHeader });
                return;
            }

            foreach (var p in payments)
            {
                _flow.Controls.Add(CreatePaymentCard(p));
            }
        }

        private Control CreatePaymentCard(RentalSystemUI.Models.Payment p)
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
            System.Windows.Forms.Panel stripe = new System.Windows.Forms.Panel { Dock = DockStyle.Left, Width = 8, BackColor = stripeColor };
            card.Controls.Add(stripe);

            AntdUI.Label lblAmount = new AntdUI.Label { Text = $"৳{p.Amount:N0}", Font = Styles.PageTitle, ForeColor = Styles.DarkBlue, Location = new Point(30, 25), AutoSize = true };
            AntdUI.Label lblDate = new AntdUI.Label
            {
                Text = "Paid: " + (p.PaymentDate?.ToShortDateString() ?? "N/A"),
                ForeColor = Styles.TextGray,
                Location = new Point(30, 60),
                AutoSize = true,
                Font = Styles.Small
            };

            AntdUI.Label lblTenant = new AntdUI.Label { Text = p.TenantName, Font = Styles.CardTitle, Location = new Point(220, 25), AutoSize = true, ForeColor = Styles.DarkBlue };
            AntdUI.Label lblProp = new AntdUI.Label { Text = p.PropertyTitle, ForeColor = Styles.TextGray, Location = new Point(220, 50), AutoSize = true, Font = Styles.Normal };

            AntdUI.Button badge = new AntdUI.Button { Text = p.Status.ToUpper(), BackColor = stripeColor, ForeColor = (p.Status == "Verified" ? Styles.GreenTxt : Styles.RedTxt), Location = new Point(card.Width - 150, 35), Size = new Size(120, 30), Radius = 6, Type = TTypeMini.Default, BorderWidth = 0, Anchor = AnchorStyles.Right | AnchorStyles.Top, Font = Styles.Bold };
            card.Controls.Add(badge);

            card.Controls.Add(lblProp);
            card.Controls.Add(lblTenant);
            card.Controls.Add(lblDate);
            card.Controls.Add(lblAmount);

            return card;
        }
    }
}
