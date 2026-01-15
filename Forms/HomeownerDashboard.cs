using System;
using System.Drawing;
using System.Windows.Forms;
using AntdUI;
using RentalSystemUI.Forms.DashboardSections;

namespace RentalSystemUI.Forms
{
    public partial class HomeownerDashboard : Form
    {
        private System.Windows.Forms.Panel _contentPanel = null!;
        private bool dragging = false;
        private Point dragCursorPoint, dragFormPoint;

        public HomeownerDashboard()
        {
            InitializeComponent();
            this.Size = new Size(1420, 900); // 1. Increase Total Width
            this.StartPosition = FormStartPosition.CenterScreen;
            
            SetupShell();
            NavigateTo(new DashboardHome());
        }

        private void SetupShell()
        {
            this.Controls.Clear();
            this.BackColor = ColorTranslator.FromHtml("#f6f7f8"); // background-light

            // 1. SIDEBAR (Width 260px ~ 64 tailwind w-64 is 256px)
            System.Windows.Forms.Panel sidebar = new System.Windows.Forms.Panel { Dock = DockStyle.Left, Width = 260, BackColor = Color.White, Padding = new Padding(16) };
            // Border Right
            System.Windows.Forms.Panel borderRight = new System.Windows.Forms.Panel { Dock = DockStyle.Right, Width = 1, BackColor = ColorTranslator.FromHtml("#e2e8f0") }; // slate-200
            sidebar.Controls.Add(borderRight);

            // -- Sidebar Content Container --
            System.Windows.Forms.Panel sidebarContent = new System.Windows.Forms.Panel { Dock = DockStyle.Fill };
            
            // Profile Header (Top of Sidebar)
            AntdUI.Panel profileHeader = new AntdUI.Panel { Dock = DockStyle.Top, Height = 60, BackColor = Color.Transparent, Padding = new Padding(0) };
            // Avatar (Use Button because Avatar doesn't support IconSvg)
            AntdUI.Button avatar = new AntdUI.Button { Text = "", IconSvg = "user", BackColor = Color.Gray, Size = new Size(40, 40), Location = new Point(0, 5), Radius = 20, Type = TTypeMini.Default, BorderWidth = 0 };
            // Name & Role
            AntdUI.Label lblName = new AntdUI.Label { Text = "Rental Manager", Font = new Font("Manrope", 10, FontStyle.Bold), ForeColor = ColorTranslator.FromHtml("#0f172a"), Location = new Point(50, 5), AutoSize = true }; // slate-900
            AntdUI.Label lblRole = new AntdUI.Label { Text = "Admin Console", Font = new Font("Manrope", 8), ForeColor = ColorTranslator.FromHtml("#64748b"), Location = new Point(50, 25), AutoSize = true }; // slate-500
            
            profileHeader.Controls.Add(lblRole);
            profileHeader.Controls.Add(lblName);
            profileHeader.Controls.Add(avatar);
            sidebarContent.Controls.Add(profileHeader);

            // Nav Menu (Below Profile)
            FlowLayoutPanel menuPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, WrapContents = false, Padding = new Padding(0, 20, 0, 0) };
            
            AddMenuButton(menuPanel, "Dashboard", "dashboard", true, () => NavigateTo(new DashboardHome()));
            AddMenuButton(menuPanel, "My Properties", "apartment", false, () => { });
            AddMenuButton(menuPanel, "Tenants", "team", false, () => { });
            AddMenuButton(menuPanel, "Financials", "dollar", false, () => NavigateTo(new PaymentList()));
            AddMenuButton(menuPanel, "Requests", "tool", false, () => NavigateTo(new RequestList()));
            AddMenuButton(menuPanel, "Settings", "setting", false, () => NavigateTo(new Settings()));

            sidebarContent.Controls.Add(menuPanel);
            menuPanel.BringToFront();
            profileHeader.SendToBack(); // Keep at top logically relative to flow?? No, Dock Top works.

            // Sign Out (Bottom)
            AntdUI.Button btnSignOut = new AntdUI.Button { Text = "Sign Out", IconSvg = "logout", Type = TTypeMini.Default, Dock = DockStyle.Bottom, Height = 45, BackColor = ColorTranslator.FromHtml("#f1f5f9"), ForeColor = ColorTranslator.FromHtml("#334155"), Radius = 8, Font = new Font("Manrope", 9, FontStyle.Bold) };
            sidebarContent.Controls.Add(btnSignOut);

            sidebar.Controls.Add(sidebarContent);
            this.Controls.Add(sidebar);

            // 2. MAIN AREA
            System.Windows.Forms.Panel mainArea = new System.Windows.Forms.Panel { Dock = DockStyle.Fill, BackColor = ColorTranslator.FromHtml("#f6f7f8") };
            
            // Header (Top Bar)
            System.Windows.Forms.Panel header = new System.Windows.Forms.Panel { Dock = DockStyle.Top, Height = 70, BackColor = Color.White, Padding = new Padding(32, 16, 32, 16) };
            // Border Bottom
            header.Controls.Add(new System.Windows.Forms.Panel { Dock = DockStyle.Bottom, Height = 1, BackColor = ColorTranslator.FromHtml("#e2e8f0") });

            // Page Title
            AntdUI.Label lblPageTitle = new AntdUI.Label { Name="lblPageTitle", Text = "Dashboard Overview", Font = new Font("Manrope", 14, FontStyle.Bold), ForeColor = ColorTranslator.FromHtml("#0f172a"), Dock = DockStyle.Left, AutoSize = true, Padding = new Padding(0,10,0,0) };
            header.Controls.Add(lblPageTitle);

            // Right Actions (Search + Notif + Profile)
            FlowLayoutPanel rightActions = new FlowLayoutPanel { Dock = DockStyle.Right, FlowDirection = FlowDirection.RightToLeft, Width = 400, Padding = new Padding(0, 5, 0, 0) };
            
            // Profile Circle
            AntdUI.Button btnProfile = new AntdUI.Button { IconSvg = "user", Size = new Size(40, 40), Radius = 20, Type = TTypeMini.Default, BackColor = Color.Transparent, ForeColor = Color.Gray };
            rightActions.Controls.Add(btnProfile);

            // Notif
            AntdUI.Button btnNotif = new AntdUI.Button { IconSvg = "bell", Size = new Size(40, 40), Radius = 20, Type = TTypeMini.Default, BackColor = Color.Transparent, ForeColor = Color.Gray };
            // Badge logic can be added later
            rightActions.Controls.Add(btnNotif);

            // Search
            AntdUI.Input inputSearch = new AntdUI.Input { PrefixSvg = "search", PlaceholderText = "Search properties, tenants...", Radius = 8, Width = 250, Height = 40, BackColor = ColorTranslator.FromHtml("#f1f5f9"), BorderWidth = 0 };
            rightActions.Controls.Add(inputSearch);

            header.Controls.Add(rightActions);
            mainArea.Controls.Add(header);

            // Content Panel
            _contentPanel = new System.Windows.Forms.Panel { Dock = DockStyle.Fill, Padding = new Padding(32) };
            mainArea.Controls.Add(_contentPanel);

            this.Controls.Add(mainArea);
            
            // Dragging
            AttachDrag(header);
            AttachDrag(sidebar);
        }
        
        // Restore AttachDrag Method
        private void AttachDrag(Control c)
        {
            if (c == null) return;
            c.MouseDown += (s, e) => { dragging = true; dragCursorPoint = Cursor.Position; dragFormPoint = this.Location; };
            c.MouseMove += (s, e) => { if (dragging) { Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint)); this.Location = Point.Add(dragFormPoint, new Size(dif)); } };
            c.MouseUp += (s, e) => dragging = false;
        }

        private void AddMenuButton(FlowLayoutPanel container, string text, string icon, bool isActive, Action onClick)
        {
            // Active: bg-primary/10 (light blue) text-primary (#137fec)
            // Inactive: text-slate-600 (#475569) hover:bg-slate-100
            Color bg = isActive ? Color.FromArgb(25, 19, 127, 236) : Color.Transparent; // approx primary/10
            Color fg = isActive ? ColorTranslator.FromHtml("#137fec") : ColorTranslator.FromHtml("#475569");
            
            AntdUI.Button btn = new AntdUI.Button
            {
                Text = text,
                Width = 220, // fit sidebar padding
                Height = 45,
                Type = TTypeMini.Default, 
                BorderWidth = 0, 
                BackColor = bg,
                ForeColor = fg,
                IconSvg = icon,
                Margin = new Padding(0, 0, 0, 4),
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Manrope", 10, isActive ? FontStyle.Bold : FontStyle.Regular),
                Radius = 8
            };
            btn.Name = "btn" + text.Replace(" ", ""); 
            btn.Click += (s, e) => onClick();
            
            container.Controls.Add(btn);
        }

        private void NavigateTo(UserControl page)
        {
            _contentPanel.Controls.Clear();
            page.Dock = DockStyle.Fill;
            _contentPanel.Controls.Add(page);
        }
    }
}
