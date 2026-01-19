using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AntdUI;
using RentalSystemUI.Forms.DashboardSections;
using RentalSystemUI.Services;
using System.Collections.Generic;

namespace RentalSystemUI.Forms
{
    public partial class HomeownerDashboard : Form
    {
        private System.Windows.Forms.Panel _contentPanel = null!;
        private AntdUI.Label _lblPageTitle = null!;
        private System.Windows.Forms.Panel _sidebar = null!;
        private AntdUI.Panel _profileHeader = null!;
        private FlowLayoutPanel _menuPanel = null!;
        private AntdUI.Label _lblRole = null!;
        private AntdUI.Label _lblName = null!;
        private System.Windows.Forms.Panel _signOutPanel = null!;
        private AntdUI.Label _lblSignOut = null!;

        private bool dragging = false;
        private Point dragCursorPoint, dragFormPoint;
        private bool isSidebarCollapsed = false;
        private List<Control> _menuControls = new List<Control>();

        public HomeownerDashboard()
        {
            InitializeComponent();
            
            // Skip runtime code in designer
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            
            SetupShell();
            NavigateTo(new DashboardHome(this), "Dashboard Overview");
        }

        private void SetupShell()
        {
            this.Controls.Clear();
            this.Size = new Size(1500, 900);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;
            this.Text = "Rental Manager Admin";
            this.BackColor = ColorTranslator.FromHtml("#f6f7f8");

            // --- 1. SIDEBAR ---
            _sidebar = new System.Windows.Forms.Panel 
            { 
                Dock = DockStyle.Left, 
                Width = 235, 
                BackColor = Color.White, 
                Padding = new Padding(16) 
            };
            
            // Border Right
            System.Windows.Forms.Panel borderRight = new System.Windows.Forms.Panel 
            { 
                Dock = DockStyle.Right, 
                Width = 1, 
                BackColor = ColorTranslator.FromHtml("#e2e8f0") 
            };
            _sidebar.Controls.Add(borderRight);

            // Sidebar Content
            System.Windows.Forms.Panel sidebarContent = new System.Windows.Forms.Panel { Dock = DockStyle.Fill };
            
            // Toggle Button
            string assetsPathForToggle = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets");
            System.Windows.Forms.Panel btnToggle = new System.Windows.Forms.Panel
            {
                Size = new Size(32, 32),
                Location = new Point(0, 0),
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand
            };
            
            PictureBox toggleIcon = new PictureBox
            {
                Size = new Size(20, 20),
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(6, 6),
                BackColor = Color.Transparent
            };
            string menuIconPath = System.IO.Path.Combine(assetsPathForToggle, "menu.png");
            if (System.IO.File.Exists(menuIconPath))
            {
                try { toggleIcon.Image = Image.FromFile(menuIconPath); } catch { }
            }
            btnToggle.Controls.Add(toggleIcon);
            btnToggle.Click += (s, e) => ToggleSidebar();
            toggleIcon.Click += (s, e) => ToggleSidebar();
            
            // Profile Header
            _profileHeader = new AntdUI.Panel 
            { 
                Dock = DockStyle.Top, 
                Height = 80, 
                BackColor = Color.Transparent
            };
            
            btnToggle.Location = new Point(0, 0);
            _profileHeader.Controls.Add(btnToggle);

            _lblName = new AntdUI.Label 
            { 
                Text = "Rental Manager", 
                Font = new Font("Segoe UI", 12, FontStyle.Bold), 
                ForeColor = ColorTranslator.FromHtml("#0f172a"), 
                Location = new Point(0, 35), 
                AutoSize = true 
            };
            
            _lblRole = new AntdUI.Label 
            { 
                Text = "Admin Console", 
                Font = new Font("Segoe UI", 8), 
                ForeColor = ColorTranslator.FromHtml("#64748b"), 
                Location = new Point(0, 57), 
                AutoSize = true 
            };
            
            _profileHeader.Controls.Add(_lblRole);
            _profileHeader.Controls.Add(_lblName);
            sidebarContent.Controls.Add(_profileHeader);

            // Nav Menu
            _menuPanel = new FlowLayoutPanel 
            { 
                Dock = DockStyle.Fill, 
                FlowDirection = FlowDirection.TopDown, 
                WrapContents = false, 
                Padding = new Padding(0, 10, 0, 0) 
            };
            
            string assetsPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets");
            
            AddMenuButton(_menuPanel, "Dashboard", System.IO.Path.Combine(assetsPath, "dashboard.png"), true, () => NavigateTo(new DashboardHome(this), "Dashboard Overview"));
            AddMenuButton(_menuPanel, "My Properties", System.IO.Path.Combine(assetsPath, "properties.png"), false, () => NavigateTo(new MyProperties(1, this), "My Properties"));
            AddMenuButton(_menuPanel, "Financials", System.IO.Path.Combine(assetsPath, "payment.png"), false, () => NavigateTo(new PaymentList(), "Financials"));
            AddMenuButton(_menuPanel, "Bookings", System.IO.Path.Combine(assetsPath, "calendar.png"), false, () => NavigateTo(new RequestList(), "Booking Requests"));
            AddMenuButton(_menuPanel, "Messages", System.IO.Path.Combine(assetsPath, "message.png"), false, () => NavigateTo(new MessagesSection(), "Messages"));
            AddMenuButton(_menuPanel, "Settings", System.IO.Path.Combine(assetsPath, "settings.png"), false, () => NavigateTo(new Settings(1), "Settings"));

            sidebarContent.Controls.Add(_menuPanel);
            _menuPanel.BringToFront();
            _profileHeader.SendToBack();

            // Sign Out Button
            _signOutPanel = new System.Windows.Forms.Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50,
                BackColor = ColorTranslator.FromHtml("#f1f5f9"),
                Cursor = Cursors.Hand
            };
            
            PictureBox logoutIcon = new PictureBox
            {
                Size = new Size(20, 20),
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(16, 15),
                BackColor = Color.Transparent
            };
            string logoutIconPath = System.IO.Path.Combine(assetsPath, "log-out.png");
            if (System.IO.File.Exists(logoutIconPath))
            {
                try { logoutIcon.Image = Image.FromFile(logoutIconPath); } catch { }
            }
            
            _lblSignOut = new AntdUI.Label
            {
                Text = "Sign Out",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml("#334155"),
                Location = new Point(44, 13),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            
            _signOutPanel.Controls.Add(_lblSignOut);
            _signOutPanel.Controls.Add(logoutIcon);
            
            EventHandler signOutClick = (s, e) =>
            {
                try { AppSession.Clear(); } catch { }
                try
                {
                    Hide();
                    var login = new Form1();
                    login.FormClosed += (ss, ee) => { try { Close(); } catch { } };
                    login.Show();
                }
                catch
                {
                    try { Close(); } catch { }
                }
            };
            _signOutPanel.Click += signOutClick;
            logoutIcon.Click += signOutClick;
            _lblSignOut.Click += signOutClick;
            
            _signOutPanel.MouseEnter += (s, e) => _signOutPanel.BackColor = ColorTranslator.FromHtml("#e2e8f0");
            _signOutPanel.MouseLeave += (s, e) => _signOutPanel.BackColor = ColorTranslator.FromHtml("#f1f5f9");

            sidebarContent.Controls.Add(_signOutPanel);

            _sidebar.Controls.Add(sidebarContent);
            this.Controls.Add(_sidebar);

            // --- 2. MAIN AREA ---
            System.Windows.Forms.Panel mainArea = new System.Windows.Forms.Panel 
            { 
                Dock = DockStyle.Fill, 
                BackColor = ColorTranslator.FromHtml("#f6f7f8") 
            };

            // Header
            System.Windows.Forms.Panel header = new System.Windows.Forms.Panel 
            { 
                Dock = DockStyle.Top, 
                Height = 80, 
                BackColor = Color.White, 
                Padding = new Padding(32, 20, 32, 20) 
            };

            // Page Title
            _lblPageTitle = new AntdUI.Label 
            { 
                Text = "Dashboard Overview", 
                Font = new Font("Segoe UI", 20, FontStyle.Bold), 
                ForeColor = ColorTranslator.FromHtml("#1e293b"), 
                Dock = DockStyle.Left, 
                AutoSize = true 
            };
            header.Controls.Add(_lblPageTitle);

            mainArea.Controls.Add(header);

            // Content Panel
            _contentPanel = new System.Windows.Forms.Panel 
            { 
                Dock = DockStyle.Fill, 
                BackColor = ColorTranslator.FromHtml("#f6f7f8"), 
                Padding = new Padding(24) 
            };
            mainArea.Controls.Add(_contentPanel);

            // Window Control Buttons - Added to mainArea (like Form1's panel2)
            // Minimize Button (GREEN - Success)
            AntdUI.Button btnMin = new AntdUI.Button 
            { 
                Text = "--", 
                Name = "btnMinimize",
                Size = new Size(45, 37), 
                Location = new Point(mainArea.Width - 100, 0),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Type = TTypeMini.Success
            };
            btnMin.Click += (s, e) => this.WindowState = FormWindowState.Minimized;
            mainArea.Controls.Add(btnMin);
            btnMin.BringToFront();

            // Close Button (RED - Error)
            AntdUI.Button btnClose = new AntdUI.Button 
            { 
                Text = "✕", 
                Name = "btnClose",
                Size = new Size(45, 37), 
                Location = new Point(mainArea.Width - 55, 0),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Type = TTypeMini.Error
            };
            btnClose.Click += (s, e) => Application.Exit();
            mainArea.Controls.Add(btnClose);
            btnClose.BringToFront();

            this.Controls.Add(mainArea);
            
            // Z-Order
            _sidebar.SendToBack(); 
            mainArea.BringToFront(); 
            
            // Drag Support
            AttachDrag(header);
            AttachDrag(_sidebar);
            AttachDrag(_lblPageTitle); 
        }

        private void ToggleSidebar()
        {
            isSidebarCollapsed = !isSidebarCollapsed;

            if (isSidebarCollapsed)
            {
                _sidebar.Width = 65;
                _lblName.Visible = false;
                _lblRole.Visible = false;

                // Signout panel adjust
                _signOutPanel.Width = 70;
                foreach(Control c in _signOutPanel.Controls) {
                    if (c is AntdUI.Label) c.Visible = false;
                    if (c is PictureBox pb) pb.Location = new Point(25, 15);
                }

                foreach (Control pnl in _menuControls)
                {
                    pnl.Width = 70; 
                    foreach(Control c in pnl.Controls)
                    {
                        if(c is AntdUI.Label) c.Visible = false;
                        if(c is PictureBox pb) pb.Location = new Point(25, 12); 
                    }
                }
            }
            else
            {
                _sidebar.Width = 235;
                _lblName.Visible = true;
                _lblRole.Visible = true;

                // Signout panel adjust
                _signOutPanel.Width = 200;
                foreach(Control c in _signOutPanel.Controls) {
                    if (c is AntdUI.Label) c.Visible = true;
                    if (c is PictureBox pb) pb.Location = new Point(16, 15);
                }

                foreach (Control pnl in _menuControls)
                {
                    pnl.Width = 208;
                    foreach(Control c in pnl.Controls)
                    {
                        if(c is AntdUI.Label) c.Visible = true;
                        if(c is PictureBox pb) pb.Location = new Point(12, 12);
                    }
                }
            }
        }

        private void AttachDrag(Control c)
        {
            if (c == null) return;
            c.MouseDown += (s, e) => { dragging = true; dragCursorPoint = Cursor.Position; dragFormPoint = this.Location; };
            c.MouseMove += (s, e) => { if (dragging) { Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint)); this.Location = Point.Add(dragFormPoint, new Size(dif)); } };
            c.MouseUp += (s, e) => dragging = false;
        }

        private void AddMenuButton(FlowLayoutPanel container, string text, string iconPath, bool isActive, Action onClick)
        {
            System.Windows.Forms.Panel btnPanel = new System.Windows.Forms.Panel
            {
                Width = 208,
                Height = 45,
                Margin = new Padding(0, 0, 0, 4),
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand
            };
            
            PictureBox pb = new PictureBox
            {
                Size = new Size(20, 20),
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(12, 12), 
                BackColor = Color.Transparent
            };

            if (System.IO.File.Exists(iconPath))
            {
                try { pb.Image = Image.FromFile(iconPath); } catch {}
            }
            else
            {
                pb.BackColor = Color.Silver; 
            }

            AntdUI.Label lbl = new AntdUI.Label
            {
                Text = text,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = ColorTranslator.FromHtml("#475569"),
                AutoSize = true,
                Location = new Point(44, 11), 
                BackColor = Color.Transparent
            };

            EventHandler clickHandler = (s, e) => onClick();
            btnPanel.Click += clickHandler;
            pb.Click += clickHandler;
            lbl.Click += clickHandler;

            btnPanel.MouseEnter += (s, e) => btnPanel.BackColor = ColorTranslator.FromHtml("#f1f5f9");
            btnPanel.MouseLeave += (s, e) => btnPanel.BackColor = Color.Transparent;
            lbl.MouseEnter += (s, e) => btnPanel.BackColor = ColorTranslator.FromHtml("#f1f5f9");
            lbl.MouseLeave += (s, e) => btnPanel.BackColor = Color.Transparent;

            btnPanel.Controls.Add(lbl);
            btnPanel.Controls.Add(pb);

            _menuControls.Add(btnPanel);
            container.Controls.Add(btnPanel);
        }

        public void NavigateTo(Form page, string title)
        {
            _contentPanel.Controls.Clear();
            
            // Allow form to be embedded
            page.TopLevel = false; 
            page.FormBorderStyle = FormBorderStyle.None;
            page.Dock = DockStyle.Fill;
            
            _contentPanel.Controls.Add(page);
            page.Show(); // Important for Forms
            
            _lblPageTitle.Text = title;
        }

        public void NavigateToProperties()
        {
            NavigateTo(new MyProperties(1, this), "My Properties");
        }
    }
}
