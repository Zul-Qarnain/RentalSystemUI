using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace LandlordPortal.Controls
{
    public partial class DashboardControl : UserControl
    {
        public DashboardControl()
        {
            this.BackColor = Styles.Back;
            this.Size = new Size(1100, 850);
            this.AutoScroll = true;
            InitUI();
        }

        private void InitUI()
        {
            // --- HEADER ---
            Label title = new Label { Text = "Dashboard", Font = Styles.Header, ForeColor = Styles.TextMain, Location = new Point(25, 30), AutoSize = true };
            Label sub = new Label { Text = "Overview of your properties and requests", Font = Styles.SubHeader, ForeColor = Styles.TextGray, Location = new Point(30, 75), AutoSize = true };

            Button btnAdd = new Button { Text = "+ Add Property", BackColor = Styles.Blue, ForeColor = Color.White, Size = new Size(140, 45), Location = new Point(900, 30), FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold), Cursor = Cursors.Hand };
            btnAdd.Region = Region.FromHrgn(Styles.CreateRoundRectRgn(0, 0, 140, 45, 15, 15)); btnAdd.FlatAppearance.BorderSize = 0;

            this.Controls.Add(title); this.Controls.Add(sub); this.Controls.Add(btnAdd);

            // --- STATS CARDS ---
            this.Controls.Add(CreateStatCard("Total Properties", "12", "+2 new", Styles.Blue, 30, 140, "chart"));
            this.Controls.Add(CreateStatCard("Active Bookings", "8", "+1 this week", Color.Purple, 380, 140, "cal"));
            this.Controls.Add(CreateStatCard("Monthly Earnings", "$24,500", "+12%", Color.Green, 730, 140, "money"));

            // --- RECENT PROPERTIES (Left Side) ---
            RoundedPanel propPanel = new RoundedPanel { Location = new Point(30, 290), Size = new Size(680, 450), BackColor = Styles.White };

            Label lblPTitle = new Label { Text = "Recent Properties", Font = Styles.CardTitle, ForeColor = Styles.TextMain, Location = new Point(20, 20), AutoSize = true };
            Label lblView = new Label { Text = "View All", Font = Styles.Bold, ForeColor = Styles.Blue, Location = new Point(600, 25), Cursor = Cursors.Hand, AutoSize = true };
            propPanel.Controls.Add(lblPTitle); propPanel.Controls.Add(lblView);

            AddHeader(propPanel, "PROPERTY", 20); AddHeader(propPanel, "LOCATION", 220); AddHeader(propPanel, "RENT", 350); AddHeader(propPanel, "STATUS", 460); AddHeader(propPanel, "ACTIONS", 600);

            FlowLayoutPanel list = new FlowLayoutPanel { Location = new Point(0, 70), Size = new Size(680, 380), FlowDirection = FlowDirection.TopDown };

            // --- FIX: Pass specific details for each row ---
            list.Controls.Add(CreatePropRow("Sunnyvale Heights", "2 Bed, 2 Bath", "San Francisco", "$3,200/mo", "Occupied", Styles.OrangeBg, Styles.OrangeTxt));
            list.Controls.Add(CreatePropRow("Lakeside Cabin", "3 Bed, 1 Bath", "Tahoe", "$450/night", "Available", Styles.GreenBg, Color.White));
            list.Controls.Add(CreatePropRow("Urban Loft", "Studio", "Seattle", "$2,100/mo", "Maintenance", Styles.BlueBg, Styles.Blue));

            propPanel.Controls.Add(list);
            this.Controls.Add(propPanel);

            // --- BOOKING REQUESTS (Right Side) ---
            Label lblReqTitle = new Label { Text = "Booking Requests", Font = Styles.CardTitle, ForeColor = Styles.TextMain, Location = new Point(730, 290), AutoSize = true };
            Label badge = new Label { Text = " 2 New ", BackColor = Color.Red, ForeColor = Color.White, Font = new Font("Segoe UI", 8, FontStyle.Bold), Location = new Point(900, 295), AutoSize = true };
            this.Controls.Add(lblReqTitle); this.Controls.Add(badge);

            FlowLayoutPanel reqList = new FlowLayoutPanel { Location = new Point(730, 330), Size = new Size(340, 450), FlowDirection = FlowDirection.TopDown };

            // --- FIX: Pass specific names and messages ---
            reqList.Controls.Add(CreateReqCard("Sarah Jenkins", "Oct 1 - Oct 15 (14 days)", "2 Bedroom Apt, Downtown", "\"Looking for a quiet place while I renovate my house.\"", true));
            reqList.Controls.Add(CreateReqCard("Mike Ross", "Nov 1 (Long term)", "Studio Loft, Arts District", "\"Is parking included?\"", false));

            this.Controls.Add(reqList);
        }

        // --- DRAWING HELPERS ---
        private Panel CreateStatCard(string t, string v, string tr, Color c, int x, int y, string type)
        {
            RoundedPanel p = new RoundedPanel { Location = new Point(x, y), Size = new Size(330, 120), BackColor = Color.White };
            p.Controls.Add(new Label { Text = t, ForeColor = Styles.TextGray, Location = new Point(20, 20), AutoSize = true });
            p.Controls.Add(new Label { Text = v, Font = new Font("Segoe UI", 22, FontStyle.Bold), ForeColor = Styles.TextMain, Location = new Point(15, 45), AutoSize = true });
            p.Controls.Add(new Label { Text = tr, ForeColor = c, Font = Styles.Bold, Location = new Point(200, 60), AutoSize = true });

            Panel icon = new Panel { Size = new Size(45, 45), Location = new Point(270, 20), BackColor = Styles.Back };
            icon.Region = Region.FromHrgn(Styles.CreateRoundRectRgn(0, 0, 45, 45, 45, 45));
            icon.Paint += (s, e) => {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (Pen pen = new Pen(c, 2))
                {
                    if (type == "chart") { e.Graphics.DrawRectangle(pen, 12, 12, 6, 6); e.Graphics.DrawRectangle(pen, 20, 12, 6, 6); e.Graphics.DrawRectangle(pen, 12, 20, 6, 6); e.Graphics.DrawRectangle(pen, 20, 20, 6, 6); }
                    if (type == "cal") e.Graphics.DrawRectangle(pen, 12, 12, 20, 20);
                    if (type == "money") e.Graphics.DrawString("$", new Font("Arial", 14, FontStyle.Bold), new SolidBrush(c), 12, 10);
                }
            };
            p.Controls.Add(icon);
            return p;
        }

        private Panel CreatePropRow(string n, string sub, string l, string p, string s, Color bg, Color fg)
        {
            Panel pan = new Panel { Size = new Size(680, 70), BackColor = Color.White };

            // Draw Fake House Image
            Panel img = new Panel { Size = new Size(45, 45), Location = new Point(20, 12) };
            img.Paint += (s, e) => { using (LinearGradientBrush b = new LinearGradientBrush(img.ClientRectangle, Color.LightGray, Color.Gray, 45F)) e.Graphics.FillRectangle(b, img.ClientRectangle); };

            pan.Controls.Add(img);
            pan.Controls.Add(new Label { Text = n, Font = Styles.Bold, ForeColor = Styles.TextMain, Location = new Point(75, 12), AutoSize = true });

            // --- FIX: Use the specific subtitle passed in ---
            pan.Controls.Add(new Label { Text = sub, ForeColor = Styles.TextGray, Font = new Font("Segoe UI", 8), Location = new Point(75, 32), AutoSize = true });

            pan.Controls.Add(new Label { Text = l, ForeColor = Styles.TextMain, Location = new Point(220, 25), AutoSize = true });
            pan.Controls.Add(new Label { Text = p, Font = Styles.Bold, ForeColor = Styles.TextMain, Location = new Point(350, 25), AutoSize = true });

            Label lStat = new Label { Text = s, BackColor = bg, ForeColor = fg, Font = new Font("Segoe UI", 8, FontStyle.Bold), Location = new Point(460, 25), AutoSize = true, Padding = new Padding(3) };
            pan.Controls.Add(lStat);

            Label edit = new Label { Text = "✎", ForeColor = Styles.TextGray, Font = new Font("Segoe UI Symbol", 12), Location = new Point(600, 22), Cursor = Cursors.Hand };
            pan.Controls.Add(edit);

            pan.Paint += (s, e) => e.Graphics.DrawLine(new Pen(Color.WhiteSmoke), 20, 69, 660, 69);
            return pan;
        }

        private Panel CreateReqCard(string n, string date, string prop, string msg, bool active)
        {
            RoundedPanel p = new RoundedPanel { Size = new Size(330, 220), BackColor = Color.White, Margin = new Padding(0, 0, 0, 20) };

            if (active) { Panel bar = new Panel { Size = new Size(5, 160), BackColor = Styles.Blue, Location = new Point(0, 30) }; p.Controls.Add(bar); }

            Panel av = new Panel { Size = new Size(40, 40), Location = new Point(20, 20) };
            av.Paint += (s, e) => { e.Graphics.SmoothingMode = SmoothingMode.AntiAlias; e.Graphics.FillEllipse(new SolidBrush(active ? Color.PeachPuff : Color.LightBlue), 0, 0, 40, 40); };

            p.Controls.Add(av);
            p.Controls.Add(new Label { Text = n, Font = Styles.Bold, ForeColor = Styles.TextMain, Location = new Point(70, 20), AutoSize = true });
            p.Controls.Add(new Label { Text = date, ForeColor = Styles.TextGray, Font = new Font("Segoe UI", 8), Location = new Point(70, 40), AutoSize = true });
            p.Controls.Add(new Label { Text = "Pending", ForeColor = Styles.Blue, Font = new Font("Segoe UI", 8, FontStyle.Bold), Location = new Point(270, 20), AutoSize = true });

            // --- FIX: Use specific property name and message ---
            p.Controls.Add(new Label { Text = prop, Font = Styles.Bold, ForeColor = Styles.TextMain, Location = new Point(20, 75), AutoSize = true });
            p.Controls.Add(new Label { Text = msg, ForeColor = Styles.TextGray, Font = new Font("Segoe UI", 9, FontStyle.Italic), Location = new Point(20, 100), Size = new Size(290, 40) });

            Button btnApp = new Button { Text = "Approve", BackColor = Styles.Blue, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Size = new Size(130, 35), Location = new Point(20, 160), Cursor = Cursors.Hand };
            btnApp.FlatAppearance.BorderSize = 0;
            p.Controls.Add(btnApp);
            p.Controls.Add(new Label { Text = "Reject", ForeColor = Styles.TextMain, Location = new Point(170, 170), Cursor = Cursors.Hand });

            return p;
        }

        private void AddHeader(Panel p, string t, int x)
        {
            p.Controls.Add(new Label { Text = t, ForeColor = Styles.TextGray, Font = new Font("Segoe UI", 7, FontStyle.Bold), Location = new Point(x, 50), AutoSize = true });
        }
    }
}