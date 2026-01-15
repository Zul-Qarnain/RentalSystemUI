using System;
using System.Drawing;
using System.Windows.Forms;
using AntdUI;
using RentalSystemUI.Forms.DashboardSections;
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
        private AntdUI.Button _btnSignOut = null!;
        private AntdUI.Label _lblRole = null!;
        private AntdUI.Label _lblName = null!;

        private bool dragging = false;
        private Point dragCursorPoint, dragFormPoint;
        private bool isSidebarCollapsed = false;
        private List<Control> _menuControls = new List<Control>();

        public HomeownerDashboard()
        {
            // Manual UI Setup - No Designer
            // Form Properties
            this.Size = new Size(1366, 800); // Increased width slightly
            this.StartPosition = FormStartPosition.CenterScreen;
            // this.WindowState = FormWindowState.Maximized; // Manual control
            this.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;
            this.Text = "Rental Manager Admin";
            this.Icon = SystemIcons.Application; 

            SetupShell();

            // Navigate to default page
            NavigateTo(new DashboardHome(), "Dashboard Overview");
        }

        private void SetupShell()
        {
            this.Controls.Clear();
            this.BackColor = ColorTranslator.FromHtml("#f6f7f8"); // background-light

            // --- 1. SIDEBAR ---
            _sidebar = new System.Windows.Forms.Panel 
            { 
                Dock = DockStyle.Left, 
                Width = 250, 
                BackColor = Color.White, 
                Padding = new Padding(16) 
            };
            
            // Border Right (Separator)
            System.Windows.Forms.Panel borderRight = new System.Windows.Forms.Panel 
            { 
                Dock = DockStyle.Right, 
                Width = 1, 
                BackColor = ColorTranslator.FromHtml("#e2e8f0") 
            };
            _sidebar.Controls.Add(borderRight);

            // Sidebar Content
            System.Windows.Forms.Panel sidebarContent = new System.Windows.Forms.Panel { Dock = DockStyle.Fill };
            
            // -- Toggle Button (Top Left of Sidebar) --
            AntdUI.Button btnToggle = new AntdUI.Button
            {
                IconSvg = "menu-fold", 
                Size = new Size(32, 32),
                Radius = 4,
                Type = TTypeMini.Default,
                BackColor = Color.Transparent,
                ForeColor = Color.Gray,
                Location = new Point(0, 0)
            };
            btnToggle.Click += (s, e) => ToggleSidebar(btnToggle);
            
            // -- Profile Header --
            _profileHeader = new AntdUI.Panel 
            { 
                Dock = DockStyle.Top, 
                Height = 80, 
                BackColor = Color.Transparent, 
                Padding = new Padding(0) 
            };
            
            // Add Toggle to Header
            btnToggle.Location = new Point(0, 0);
            _profileHeader.Controls.Add(btnToggle);

            AntdUI.Button avatar = new AntdUI.Button 
            { 
                Text = "", 
                IconSvg = "user", 
                BackColor = Color.Gray, 
                Size = new Size(40, 40), 
                Location = new Point(0, 35), 
                Radius = 20, 
                Type = TTypeMini.Default, 
                BorderWidth = 0 
            };

            _lblName = new AntdUI.Label 
            { 
                Text = "Rental Manager", 
                Font = new Font("Manrope", 10, FontStyle.Bold), 
                ForeColor = ColorTranslator.FromHtml("#0f172a"), 
                Location = new Point(50, 35), 
                AutoSize = true 
            };
            
            _lblRole = new AntdUI.Label 
            { 
                Text = "Admin Console", 
                Font = new Font("Manrope", 8), 
                ForeColor = ColorTranslator.FromHtml("#64748b"), 
                Location = new Point(50, 55), 
                AutoSize = true 
            };
            
            _profileHeader.Controls.Add(_lblRole);
            _profileHeader.Controls.Add(_lblName);
            _profileHeader.Controls.Add(avatar);
            sidebarContent.Controls.Add(_profileHeader);

            // -- Nav Menu --
            _menuPanel = new FlowLayoutPanel 
            { 
                Dock = DockStyle.Fill, 
                FlowDirection = FlowDirection.TopDown, 
                WrapContents = false, 
                Padding = new Padding(0, 10, 0, 0) 
            };
            
            // Add Menu Items
            string assetsPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets");
            
            AddMenuButton(_menuPanel, "Dashboard", System.IO.Path.Combine(assetsPath, "dashboard.png"), true, () => NavigateTo(new DashboardHome(), "Dashboard Overview"));
            AddMenuButton(_menuPanel, "My Properties", System.IO.Path.Combine(assetsPath, "properties.png"), false, () => { });
            AddMenuButton(_menuPanel, "Tenants", System.IO.Path.Combine(assetsPath, "users.png"), false, () => { });
            AddMenuButton(_menuPanel, "Financials", System.IO.Path.Combine(assetsPath, "payment.png"), false, () => NavigateTo(new PaymentList(), "Financials"));
            AddMenuButton(_menuPanel, "Requests", System.IO.Path.Combine(assetsPath, "calendar.png"), false, () => NavigateTo(new RequestList(), "Maintenance Requests"));
            AddMenuButton(_menuPanel, "Settings", System.IO.Path.Combine(assetsPath, "settings.png"), false, () => NavigateTo(new Settings(), "Settings"));

            sidebarContent.Controls.Add(_menuPanel);
            _menuPanel.BringToFront();
            _profileHeader.SendToBack();

            // -- Sign Out Button --
            _btnSignOut = new AntdUI.Button 
            { 
                Text = "Sign Out", 
                IconSvg = "logout", 
                Type = TTypeMini.Default, 
                Dock = DockStyle.Bottom, 
                Height = 45, 
                BackColor = ColorTranslator.FromHtml("#f1f5f9"), 
                ForeColor = ColorTranslator.FromHtml("#334155"), 
                Radius = 8, 
                Font = new Font("Manrope", 9, FontStyle.Bold) 
            };
            _btnSignOut.Click += (s, e) => this.Close();

            sidebarContent.Controls.Add(_btnSignOut);

            _sidebar.Controls.Add(sidebarContent);
            this.Controls.Add(_sidebar);


            // --- 2. MAIN AREA ---
            System.Windows.Forms.Panel mainArea = new System.Windows.Forms.Panel 
            { 
                Dock = DockStyle.Fill, 
                BackColor = ColorTranslator.FromHtml("#f6f7f8") 
            };
            
            // -- Header --
            System.Windows.Forms.Panel header = new System.Windows.Forms.Panel 
            { 
                Dock = DockStyle.Top, 
                Height = 70, 
                BackColor = Color.White, 
                Padding = new Padding(32, 16, 32, 16) 
            };
            
            // Header Border Bottom
            header.Controls.Add(new System.Windows.Forms.Panel 
            { 
                Dock = DockStyle.Bottom, 
                Height = 1, 
                BackColor = ColorTranslator.FromHtml("#e2e8f0") 
            });

            // Page Title
            _lblPageTitle = new AntdUI.Label 
            { 
                Text = "Dashboard Overview", 
                Font = new Font("Manrope", 14, FontStyle.Bold), 
                ForeColor = ColorTranslator.FromHtml("#0f172a"), 
                Dock = DockStyle.Left, 
                AutoSize = true, 
                Padding = new Padding(0, 10, 0, 0) 
            };
            header.Controls.Add(_lblPageTitle);

            // Right Actions Area
            FlowLayoutPanel rightActions = new FlowLayoutPanel 
            { 
                Dock = DockStyle.Right, 
                FlowDirection = FlowDirection.RightToLeft, 
                Width = 200, 
                Padding = new Padding(0, 5, 0, 0) 
            };
            
             // --- WINDOW CONTROLS ---
            
            // Close Button
            AntdUI.Button btnClose = new AntdUI.Button 
            { 
                IconSvg = "close", 
                Size = new Size(40, 40), 
                Radius = 20, 
                Type = TTypeMini.Default, 
                BackColor = Color.Transparent, 
                ForeColor = Color.Gray 
            };
            btnClose.Click += (s, e) => Application.Exit();
            btnClose.BackHover = Color.Red;
            btnClose.ForeHover = Color.White;
            rightActions.Controls.Add(btnClose);

            // Maximize
            AntdUI.Button btnMax = new AntdUI.Button { IconSvg = "border", Size = new Size(40, 40), Radius = 20, Type = TTypeMini.Default, BackColor = Color.Transparent, ForeColor = Color.Gray };
            btnMax.Click += (s, e) => { this.WindowState = (this.WindowState == FormWindowState.Normal) ? FormWindowState.Maximized : FormWindowState.Normal; };
            rightActions.Controls.Add(btnMax);

            // Minimize
            AntdUI.Button btnMin = new AntdUI.Button { IconSvg = "minus", Size = new Size(40, 40), Radius = 20, Type = TTypeMini.Default, BackColor = Color.Transparent, ForeColor = Color.Gray };
            btnMin.Click += (s, e) => { this.WindowState = FormWindowState.Minimized; };
            rightActions.Controls.Add(btnMin);

            header.Controls.Add(rightActions);
            mainArea.Controls.Add(header);

            // -- Content Panel --
            _contentPanel = new System.Windows.Forms.Panel 
            { 
                Dock = DockStyle.Fill, 
                Padding = new Padding(24) 
            };
            mainArea.Controls.Add(_contentPanel);

            this.Controls.Add(mainArea);
            
            // --- Z-ORDER FIX ---
            _sidebar.SendToBack(); 
            mainArea.BringToFront(); 
            
            // --- Drag Support ---
            AttachDrag(header);
            AttachDrag(_sidebar);
            AttachDrag(_lblPageTitle); 
        }

        private void ToggleSidebar(AntdUI.Button toggleBtn)
        {
            isSidebarCollapsed = !isSidebarCollapsed;

            if (isSidebarCollapsed)
            {
                _sidebar.Width = 80; // Collapsed Width
                toggleBtn.IconSvg = "menu-unfold";
                
                // Hide Text Elements
                _lblName.Visible = false;
                _lblRole.Visible = false;
                _btnSignOut.Text = ""; 
                _btnSignOut.Width = 45; 
                _btnSignOut.Location = new Point(0, _btnSignOut.Location.Y); 

                foreach (Control pnl in _menuControls)
                {
                    pnl.Width = 45; 
                    foreach(Control c in pnl.Controls)
                    {
                         if(c is AntdUI.Label) c.Visible = false;
                         if(c is PictureBox) c.Location = new Point(12, 12); 
                    }
                }
            }
            else
            {
                _sidebar.Width = 250; // Expanded Width
                toggleBtn.IconSvg = "menu-fold";
                
                _lblName.Visible = true;
                _lblRole.Visible = true;
                _btnSignOut.Text = "Sign Out";
                _btnSignOut.Width = 220; 
                
                foreach (Control pnl in _menuControls)
                {
                    pnl.Width = 220; 
                    foreach(Control c in pnl.Controls)
                    {
                         if(c is AntdUI.Label) c.Visible = true;
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
            // Custom Menu Button: Panel + PictureBox + Label
            System.Windows.Forms.Panel btnPanel = new System.Windows.Forms.Panel
            {
                Width = 220,
                Height = 45,
                Margin = new Padding(0, 0, 0, 4),
                Padding = new Padding(0), 
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand
            };
            
            // Icon (PictureBox)
            PictureBox pb = new PictureBox
            {
                Size = new Size(20, 20),
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(12, 12), 
                BackColor = Color.Transparent
            };

            // Loading Image
            if (System.IO.File.Exists(iconPath))
            {
                try { pb.Image = Image.FromFile(iconPath); } catch {}
            }
            else
            {
                 pb.BackColor = Color.Silver; 
            }

            // Text (Label)
            AntdUI.Label lbl = new AntdUI.Label
            {
                Text = text,
                Font = new Font("Manrope", 10, FontStyle.Regular),
                ForeColor = ColorTranslator.FromHtml("#475569"),
                AutoSize = true,
                Location = new Point(44, 11), 
                BackColor = Color.Transparent
            };

            // Events for Interactivity
            EventHandler clickHandler = (s, e) => onClick();
            EventHandler enterHandler = (s, e) => btnPanel.BackColor = ColorTranslator.FromHtml("#f1f5f9");
            EventHandler leaveHandler = (s, e) => btnPanel.BackColor = Color.Transparent;

            btnPanel.Click += clickHandler;
            pb.Click += clickHandler;
            lbl.Click += clickHandler;

            btnPanel.MouseEnter += enterHandler;
            pb.MouseEnter += enterHandler;
            lbl.MouseEnter += enterHandler;

            btnPanel.MouseLeave += leaveHandler;
            pb.MouseLeave += leaveHandler;
            lbl.MouseLeave += leaveHandler;

            btnPanel.Controls.Add(lbl);
            btnPanel.Controls.Add(pb);

            container.Controls.Add(btnPanel);
             _menuControls.Add(btnPanel); 
        }

        private void NavigateTo(UserControl page, string title)
        {
            _contentPanel.Controls.Clear();
            page.Dock = DockStyle.Fill;
            _contentPanel.Controls.Add(page);
            
            if(_lblPageTitle != null) _lblPageTitle.Text = title;
        }
    }
}
