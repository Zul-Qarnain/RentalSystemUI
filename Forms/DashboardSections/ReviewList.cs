using System;
using System.Drawing;
using System.Windows.Forms;
using AntdUI;
using RentalSystemUI.Services;

namespace RentalSystemUI.Forms.DashboardSections
{
    public class ReviewList : UserControl
    {
        private LandlordService _service = new LandlordService();
        private int _landlordId = 1;
        private FlowLayoutPanel _flow = null!;

        public ReviewList()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.FromArgb(245, 247, 250);
            InitializeUI();
            LoadData();
        }

        private void InitializeUI()
        {
            AntdUI.Label lblTitle = new AntdUI.Label { Text = "Tenant Feedback", Font = new Font("Segoe UI", 20, FontStyle.Bold), Dock = DockStyle.Top, Height = 60, Padding = new Padding(20, 15, 0, 0), ForeColor = Color.FromArgb(38, 38, 38) };
            this.Controls.Add(lblTitle);

            _flow = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoScroll = true, Padding = new Padding(20) };
            this.Controls.Add(_flow);
        }

        private void LoadData()
        {
            _flow.Controls.Clear();
            var reviews = _service.GetReviews(_landlordId);

            if (reviews.Count == 0)
            {
                _flow.Controls.Add(new AntdUI.Label { Text = "No feedback received yet.", AutoSize = true, ForeColor = Color.Gray, Font = new Font("Segoe UI", 12) });
                return;
            }

            foreach (var r in reviews)
            {
                _flow.Controls.Add(CreateReviewCard(r));
            }
        }

        private AntdUI.Panel CreateReviewCard(RentalSystemUI.Models.Review r)
        {
            AntdUI.Panel card = new AntdUI.Panel { Size = new Size(800, 150), Radius = 12, Shadow = 6, Margin = new Padding(0, 0, 0, 15), BackColor = Color.White };

            // Stars
            string stars = new string('★', r.Rating) + new string('☆', 5 - r.Rating);
            AntdUI.Label lblStars = new AntdUI.Label { Text = stars, Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.Gold, Location = new Point(25, 15), AutoSize = true };

            // Tenant Info
            AntdUI.Label lblTenant = new AntdUI.Label { Text = r.TenantName, Font = new Font("Segoe UI", 11, FontStyle.Bold), Location = new Point(25, 45), AutoSize = true, ForeColor = Color.FromArgb(38, 38, 38) };
            AntdUI.Label lblDate = new AntdUI.Label { Text = r.CreatedAt.ToShortDateString(), ForeColor = Color.Gray, Location = new Point(650, 20), AutoSize = true, Font = new Font("Segoe UI", 9) };
            
            // Comment
            AntdUI.Label lblComment = new AntdUI.Label { Text = "\"" + r.Comment + "\"", Location = new Point(25, 75), Size = new Size(700, 30), ForeColor = Color.FromArgb(89, 89, 89), Font = new Font("Segoe UI", 11, FontStyle.Italic), AutoEllipsis = true };

            card.Controls.Add(lblComment);
            card.Controls.Add(lblDate);
            card.Controls.Add(lblTenant);
            card.Controls.Add(lblStars);

            // Actions
             if (r.IsResolved)
            {
                AntdUI.Button badge = new AntdUI.Button { Text = "RESOLVED", Type = TTypeMini.Default, ForeColor = Color.Green, BackColor = Color.FromArgb(246, 255, 237), Location = new Point(650, 100), Size = new Size(100, 30), BorderWidth = 0, Radius = 4 };
                card.Controls.Add(badge);
            }
            else
            {
                AntdUI.Button btnReply = new AntdUI.Button { Text = "Reply", Type = TTypeMini.Primary, Location = new Point(25, 110), Size = new Size(90, 30), Radius = 6 };
                btnReply.Ghost = true; // Outline style
                btnReply.Click += (s, e) => {
                     // Mock Reply action
                     if (this.FindForm() is Form f) AntdUI.Message.info(f, "Reply feature coming soon.");
                };
                card.Controls.Add(btnReply);
            }

            return card;
        }
    }
}
