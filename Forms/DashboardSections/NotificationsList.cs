using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AntdUI;
using RentalSystemUI.Services;

namespace RentalSystemUI.Forms.DashboardSections
{
    public class NotificationsList : Form
    {
        private readonly int _userId;
        private readonly NotificationService _service = new NotificationService();
        private readonly FlowLayoutPanel _flow = new FlowLayoutPanel();

        public NotificationsList(int userId)
        {
            _userId = userId;

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

            var list = _service.GetLatest(_userId, 50);
            _service.MarkAllRead(_userId);

            if (list.Count == 0)
            {
                _flow.Controls.Add(new AntdUI.Label
                {
                    Text = "No notifications.",
                    AutoSize = true,
                    ForeColor = Styles.TextGray,
                    Font = Styles.SubHeader,
                    Margin = new Padding(0, 0, 0, 10)
                });
                return;
            }

            foreach (var n in list.OrderByDescending(x => x.CreatedAt))
            {
                _flow.Controls.Add(CreateCard(n.Title, n.Message, n.CreatedAt));
            }
        }

        private Control CreateCard(string title, string message, DateTime created)
        {
            AntdUI.Panel card = new AntdUI.Panel
            {
                Height = 110,
                Width = 1000,
                Radius = 14,
                Shadow = 4,
                Margin = new Padding(0, 0, 0, 12),
                BackColor = Color.White
            };

            var lblTitle = new AntdUI.Label
            {
                Text = title,
                Font = Styles.CardTitle,
                ForeColor = Styles.DarkBlue,
                Location = new Point(20, 16),
                AutoSize = true
            };

            var lblMsg = new AntdUI.Label
            {
                Text = message,
                Font = Styles.Small,
                ForeColor = Styles.TextGray,
                Location = new Point(20, 44),
                AutoSize = false,
                Size = new Size(860, 40)
            };

            var lblTime = new AntdUI.Label
            {
                Text = created.ToString("dd MMM yyyy, hh:mm tt"),
                Font = Styles.Small,
                ForeColor = Styles.TextGray,
                Location = new Point(20, 82),
                AutoSize = true
            };

            card.Controls.Add(lblTitle);
            card.Controls.Add(lblMsg);
            card.Controls.Add(lblTime);
            return card;
        }
    }
}
