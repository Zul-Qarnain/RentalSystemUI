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
        private int _landlordId = 1;

        public PaymentList()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            _flow.Controls.Clear();
            var payments = _service.GetPayments(_landlordId);

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
            
            // Left Stripe
            Color stripeColor = p.Status == "Verified" ? Styles.GreenBg : (p.Status == "Pending" ? Styles.OrangeBg : Styles.RedBg);
            System.Windows.Forms.Panel stripe = new System.Windows.Forms.Panel { Dock = DockStyle.Left, Width = 8, BackColor = stripeColor };
            card.Controls.Add(stripe);

            // Content
            AntdUI.Label lblAmount = new AntdUI.Label { Text = $"${p.Amount:N0}", Font = Styles.PageTitle, ForeColor = Styles.DarkBlue, Location = new Point(30, 25), AutoSize = true };
            AntdUI.Label lblDue = new AntdUI.Label { Text = "Due: " + p.DueDate.ToShortDateString(), ForeColor = Styles.TextGray, Location = new Point(30, 60), AutoSize = true, Font = Styles.Small };
            
            AntdUI.Label lblTenant = new AntdUI.Label { Text = p.TenantName, Font = Styles.CardTitle, Location = new Point(220, 25), AutoSize = true, ForeColor = Styles.DarkBlue };
            AntdUI.Label lblProp = new AntdUI.Label { Text = p.PropertyTitle, ForeColor = Styles.TextGray, Location = new Point(220, 50), AutoSize = true, Font = Styles.Normal };

            // Status Badge (Right)
            if (p.Status == "Pending")
            {
                 AntdUI.Button btnVerify = new AntdUI.Button { Text = "Verify", Type = TTypeMini.Primary, BackColor = Styles.GreenBg, ForeColor = Styles.GreenTxt, Location = new Point(card.Width - 220, 30), Size = new Size(90, 40), Radius = 8, Anchor = AnchorStyles.Top | AnchorStyles.Right };
                 btnVerify.Click += (s, e) => { _service.VerifyPayment(p.PaymentID); LoadData(); };

                 AntdUI.Button btnReject = new AntdUI.Button { Text = "Reject", Type = TTypeMini.Default, ForeColor = Styles.RedTxt, BackColor = Styles.White, Location = new Point(card.Width - 120, 30), Size = new Size(90, 40), Radius = 8, Anchor = AnchorStyles.Top | AnchorStyles.Right, BorderWidth = 1 };
                 btnReject.Click += (s, e) => { _service.RejectPayment(p.PaymentID); LoadData(); };
                 
                 card.Controls.Add(btnVerify);
                 card.Controls.Add(btnReject);
            }
            else 
            {
                 AntdUI.Button badge = new AntdUI.Button { Text = p.Status.ToUpper(), BackColor = stripeColor, ForeColor = (p.Status=="Verified" ? Styles.GreenTxt : Styles.RedTxt), Location = new Point(card.Width - 150, 35), Size = new Size(120, 30), Radius = 6, Type = TTypeMini.Default, BorderWidth = 0, Anchor = AnchorStyles.Right | AnchorStyles.Top, Font = Styles.Bold };
                 card.Controls.Add(badge);
            }

            card.Controls.Add(lblProp);
            card.Controls.Add(lblTenant);
            card.Controls.Add(lblDue);
            card.Controls.Add(lblAmount);
            
            return card;
        }
    }
}
