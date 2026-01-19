using System;
using System.Drawing;
using System.Windows.Forms;
using AntdUI;
using RentalSystemUI.Services;

namespace RentalSystemUI.Forms.DashboardSections
{
    public partial class ReviewList : Form
    {
        private LandlordService _service = new LandlordService();
        
        // Use current user's ID instead of hardcoded value (SECURITY FIX)
        private int LandlordId => AppSession.CurrentUser?.UserID ?? 0;

        public ReviewList()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            _flow.Controls.Clear();
            var reviews = _service.GetReviews(LandlordId);

            if (reviews.Count == 0)
            {
                _flow.Controls.Add(new AntdUI.Label { Text = "No feedback received yet.", AutoSize = true, ForeColor = Styles.TextGray, Font = Styles.SubHeader });
                return;
            }

            foreach (var r in reviews)
            {
                _flow.Controls.Add(CreateReviewCard(r));
            }
        }

        private AntdUI.Panel CreateReviewCard(RentalSystemUI.Models.Review r)
        {
            AntdUI.Panel card = new AntdUI.Panel 
            { 
                Width = 1000, 
                Height = 160, 
                Radius = 15, 
                Shadow = 5, 
                Margin = new Padding(0, 0, 0, 20), 
                BackColor = Color.White 
            };

            string stars = new string('★', r.Rating) + new string('☆', 5 - r.Rating);
            AntdUI.Label lblStars = new AntdUI.Label { Text = stars, Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.Gold, Location = new Point(20, 20), AutoSize = true };
            
            AntdUI.Label lblTenant = new AntdUI.Label { Text = r.TenantName, Font = Styles.Bold, Location = new Point(200, 25), AutoSize = true, ForeColor = Styles.DarkBlue };
            AntdUI.Label lblDate = new AntdUI.Label { Text = r.CreatedAt.ToShortDateString(), ForeColor = Styles.TextGray, Location = new Point(card.Width - 120, 25), AutoSize = true, Font = Styles.Small, Anchor = AnchorStyles.Top | AnchorStyles.Right };
            
            AntdUI.Label lblComment = new AntdUI.Label 
            { 
                 Text = r.Comment, 
                 Location = new Point(20, 60), 
                 Size = new Size(card.Width - 40, 50), 
                 ForeColor = Styles.TextGray, 
                 Font = new Font("Segoe UI", 10, FontStyle.Italic), 
                 AutoEllipsis = true,
                 Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            if (r.IsResolved)
            {
                AntdUI.Button badge = new AntdUI.Button { Text = "RESOLVED", Type = TTypeMini.Default, ForeColor = Styles.GreenTxt, BackColor = Styles.GreenBg, Location = new Point(20, 115), Size = new Size(100, 30), BorderWidth = 0, Radius = 6, Font = Styles.Bold };
                card.Controls.Add(badge);

                if (!string.IsNullOrEmpty(r.Reply))
                {
                    card.Height = 220; // Increase height for reply
                    AntdUI.Label lblReply = new AntdUI.Label 
                    { 
                        Text = $"Your Reply: {r.Reply}", 
                        Location = new Point(20, 155), 
                        Size = new Size(card.Width - 40, 50),
                        ForeColor = Styles.Blue,
                        Font = new Font("Segoe UI", 9, FontStyle.Regular),
                        AutoEllipsis = true,
                         Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                    };
                    card.Controls.Add(lblReply);
                }
            }
            else
            {
                AntdUI.Button btnReply = new AntdUI.Button { Text = "Write Reply", Type = TTypeMini.Primary, Ghost = true, ForeColor = Styles.Blue, Location = new Point(20, 115), Size = new Size(120, 35), Radius = 8, Font = Styles.Bold, BorderWidth = 1 };
                btnReply.Click += (s, e) => {
                     using (var dlg = new ReplyDialog(r.TenantName, r.Comment))
                     {
                         if (dlg.ShowDialog() == DialogResult.OK)
                         {
                             _service.ReplyToReview(r.ReviewID, dlg.ReplyText);
                             AntdUI.Message.success(this, "Reply submitted.");
                             LoadData(); // Refresh list to show RESOLVED status
                         }
                     }
                };
                card.Controls.Add(btnReply);
            }

            card.Controls.Add(lblComment);
            card.Controls.Add(lblDate);
            card.Controls.Add(lblTenant);
            card.Controls.Add(lblStars);

            return card;
        }
    }
}
