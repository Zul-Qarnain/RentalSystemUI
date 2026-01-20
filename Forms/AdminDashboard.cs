using System;
using System.Drawing;
using System.Windows.Forms;
using AntdUI;
using RentalSystemUI.Forms.AdminDashboardSections;

namespace RentalSystemUI.Forms
{
    public partial class AdminDashboard : Form
    {
        private System.Windows.Forms.Panel _sidebar = null!;
        private System.Windows.Forms.Panel _contentHost = null!;
        private System.Windows.Forms.Panel _contentPanel = null!;
        private AntdUI.Label _lblPageTitle = null!;
        private Form? _currentPage;

        private readonly Color _bgApp = ColorTranslator.FromHtml("#f6f7f8");
        private readonly Color _accent = Color.FromArgb(22, 119, 255);

        public AdminDashboard()
        {
            InitializeComponent();
            SetupShell();
            NavigateTo(new AdminHome(), "Dashboard Overview");
        }

        private void SetupShell()
        {
            Size = new Size(1500, 900);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.None;
            BackColor = _bgApp;

            Controls.Clear();

            // Sidebar
            _sidebar = new System.Windows.Forms.Panel
            {
                Dock = DockStyle.Left,
                Width = 265,
                BackColor = Color.White,
                Padding = new Padding(0, 10, 0, 10)
            };

            var lblLogo = new AntdUI.Label
            {
                Text = "Admin Portal",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = _accent,
                Dock = DockStyle.Top,
                Height = 70,
                TextAlign = ContentAlignment.MiddleCenter
            };
            _sidebar.Controls.Add(lblLogo);

            var menuPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(20, 10, 20, 10),
                AutoScroll = true
            };

            menuPanel.Controls.Add(CreateMenuItem("Dashboard", () => NavigateTo(new AdminHome(), "Dashboard Overview")));
            menuPanel.Controls.Add(CreateMenuItem("User Management", () => NavigateTo(new AdminUsers(), "User Management")));
            menuPanel.Controls.Add(CreateMenuItem("Transactions", () => NavigateTo(new AdminTransactions(), "Transaction History")));

            var btnLogout = CreateMenuItem("Logout", () => Logout());
            btnLogout.ForeColor = Color.Red;
            menuPanel.Controls.Add(btnLogout);

            _sidebar.Controls.Add(menuPanel);
            Controls.Add(_sidebar);

            // Main Content Area
            var mainArea = new System.Windows.Forms.Panel
            {
                Dock = DockStyle.Fill,
                BackColor = _bgApp,
                Padding = new Padding(24)
            };

            var header = new System.Windows.Forms.Panel
            {
                Dock = DockStyle.Top,
                Height = 72,
                BackColor = Color.White,
                Padding = new Padding(20, 16, 20, 16)
            };

            _lblPageTitle = new AntdUI.Label
            {
                Text = "Overview",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Dock = DockStyle.Left,
                AutoSize = false,
                Width = 600,
                TextAlign = ContentAlignment.MiddleLeft
            };
            header.Controls.Add(_lblPageTitle);

            // Host inside main area to ensure padding is respected
            _contentHost = new System.Windows.Forms.Panel
            {
                Dock = DockStyle.Fill,
                BackColor = _bgApp,
                Padding = new Padding(0, 16, 0, 0)
            };

            // Actual content panel where forms are embedded
            _contentPanel = new System.Windows.Forms.Panel
            {
                Dock = DockStyle.Fill,
                BackColor = _bgApp
            };

            _contentHost.Controls.Add(_contentPanel);

            mainArea.Controls.Add(_contentHost);
            mainArea.Controls.Add(header);

            Controls.Add(mainArea);

            // Ensure Z-order is correct
            _sidebar.BringToFront();
            mainArea.BringToFront();
        }

        private Control CreateMenuItem(string text, Action onClick)
        {
            var btn = new AntdUI.Button
            {
                Text = text,
                Size = new Size(225, 45),
                Type = TTypeMini.Default,
                Ghost = true,
                Margin = new Padding(0, 0, 0, 10),
                TextAlign = ContentAlignment.MiddleLeft,
                Radius = 8,
                Font = new Font("Segoe UI", 10)
            };
            btn.Click += (s, e) => onClick();
            return btn;
        }

        private void NavigateTo(Form page, string title)
        {
            if (_currentPage != null)
            {
                _currentPage.Close();
                _currentPage.Dispose();
            }

            _currentPage = page;
            page.TopLevel = false;
            page.FormBorderStyle = FormBorderStyle.None;
            page.Dock = DockStyle.Fill;

            _contentPanel.Controls.Clear();
            _contentPanel.Controls.Add(page);
            page.Show();

            _lblPageTitle.Text = title;
        }

        private void Logout()
        {
            Hide();
            new Form1().Show();
        }
    }
}
