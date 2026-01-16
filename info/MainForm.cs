using System;
using System.Drawing;
using System.Windows.Forms;
using LandlordPortal.Controls;

namespace LandlordPortal
{
    public partial class MainForm : Form
    {
        Panel pnlContent;

        public MainForm()
        {
            this.Text = "Landlord Portal";
            this.Size = new Size(1380, 850);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Styles.Back;
            InitUI();
        }

        private void InitUI()
        {
            // Sidebar
            Panel sidebar = new Panel { Dock = DockStyle.Left, Width = 260, BackColor = Styles.White, Padding = new Padding(25) };
            Label logo = new Label { Text = "Landlord Portal", Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Styles.Blue, Location = new Point(25, 30), AutoSize = true };
            sidebar.Controls.Add(logo);

            // Nav Buttons
            int y = 110;
            AddNavButton(sidebar, "Dashboard", y, () => LoadView(new DashboardControl()));
            AddNavButton(sidebar, "Properties", y + 50, () => LoadView(new PropertiesControl())); // Ensure PropertiesControl exists

            // Profile
            Panel profile = new Panel { Size = new Size(220, 60), Location = new Point(20, 700) };
            Label uName = new Label { Text = "Marcus Admin", Font = Styles.Bold, Location = new Point(10, 12), AutoSize = true, ForeColor = Styles.TextMain };
            profile.Controls.Add(uName);
            sidebar.Controls.Add(profile);
            this.Controls.Add(sidebar);

            // Content
            pnlContent = new Panel { Dock = DockStyle.Fill, Padding = new Padding(0) };
            this.Controls.Add(pnlContent);

            // Load Default
            LoadView(new DashboardControl());
        }

        private void AddNavButton(Panel p, string txt, int y, Action action)
        {
            Panel btn = new Panel { Size = new Size(210, 45), Location = new Point(20, y), Cursor = Cursors.Hand };
            Panel icon = new Panel { Size = new Size(20, 20), Location = new Point(15, 12) };
            icon.Paint += (s, e) => { using (Pen pen = new Pen(Styles.TextGray, 2)) e.Graphics.DrawRectangle(pen, 0, 0, 14, 14); };
            Label lbl = new Label { Text = txt, Font = Styles.Normal, ForeColor = Styles.TextGray, Location = new Point(45, 12), AutoSize = true };

            // Click Events
            EventHandler onClick = (s, e) => action();
            btn.Click += onClick; lbl.Click += onClick; icon.Click += onClick;

            btn.Controls.Add(icon); btn.Controls.Add(lbl);
            p.Controls.Add(btn);
        }

        private void LoadView(UserControl uc)
        {
            pnlContent.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(uc);
        }
    }
}