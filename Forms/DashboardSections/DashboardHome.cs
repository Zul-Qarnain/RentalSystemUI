using System;
using System.Drawing;
using System.Windows.Forms;
using AntdUI;
using RentalSystemUI.Services;

namespace RentalSystemUI.Forms.DashboardSections
{
    public partial class DashboardHome : Form
    {
        private HomeownerDashboard _parent;

        public DashboardHome(HomeownerDashboard parent)
        {
            _parent = parent;
            
            // Designer Setup
            InitializeComponent();
            
            // Dynamic Data Loading
            LoadStats();
            LoadSplitContent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _parent.NavigateToProperties();
        }

        private void LoadStats()
        {
            // Add dynamic stats to the flow panel created in Designer
            statsFlow.Controls.Add(CreateStatCard("Total Earnings", "$5,840", "+12%", Styles.Blue, "dollar"));
            statsFlow.Controls.Add(CreateStatCard("Properties", "12", "Active", Styles.OrangeBg, "home"));
            statsFlow.Controls.Add(CreateStatCard("Requests", "5", "New", Styles.RedBg, "message"));
            statsFlow.Controls.Add(CreateStatCard("Occupancy", "92%", "+2%", Styles.GreenBg, "user"));
        }

        private void LoadSplitContent()
        {
            // Left: Recent Properties
            AntdUI.Panel panelProps = CreateSectionPanel("Recent Properties");
            splitLayout.Controls.Add(panelProps, 0, 0);

            // Right: Recent Requests
            AntdUI.Panel panelReqs = CreateSectionPanel("Booking Requests");
            splitLayout.Controls.Add(panelReqs, 1, 0);
        }

        private AntdUI.Panel CreateStatCard(string title, string value, string badge, Color iconBg, string icon)
        {
            AntdUI.Panel card = new AntdUI.Panel
            {
                Size = new Size(260, 110),
                BackColor = Color.White,
                Radius = 15,
                Shadow = 5,
                Margin = new Padding(0, 0, 20, 0)
            };

            AntdUI.Button ico = new AntdUI.Button
            {
                IconSvg = icon,
                Location = new Point(15, 25),
                Size = new Size(50, 50),
                Radius = 25,
                BackColor = iconBg, 
                ForeColor = Styles.Blue, 
                Type = TTypeMini.Default,
                BorderWidth = 0
            };

            AntdUI.Label lblTitle = new AntdUI.Label { Text = title, ForeColor = Styles.TextGray, Location = new Point(80, 30), AutoSize = true, Font = Styles.Small };
            AntdUI.Label lblValue = new AntdUI.Label { Text = value, ForeColor = Styles.DarkBlue, Location = new Point(80, 50), AutoSize = true, Font = Styles.Header };
            
            card.Controls.Add(lblValue);
            card.Controls.Add(lblTitle);
            card.Controls.Add(ico);

            return card;
        }

        private AntdUI.Panel CreateSectionPanel(string title)
        {
            AntdUI.Panel p = new AntdUI.Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Radius = 15,
                Shadow = 6,
                Margin = new Padding(10)
            };
            
            AntdUI.Label lbl = new AntdUI.Label { Text = title, Font = Styles.CardTitle, ForeColor = Styles.DarkBlue, Location = new Point(20, 20), AutoSize = true };
            p.Controls.Add(lbl);

            // Placeholder content
            AntdUI.Label place = new AntdUI.Label { Text = "Loading data...", ForeColor = Styles.TextGray, Location = new Point(20, 60), AutoSize = true };
            p.Controls.Add(place);

            return p;
        }
    }
}
