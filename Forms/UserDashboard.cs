using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AntdUI;
using RentalSystemUI.Forms.DashboardSections;
using System.Collections.Generic;

namespace RentalSystemUI.Forms
{
    public partial class UserDashboard : Form
    {
        private System.Windows.Forms.Panel _contentPanel = null!;
        private AntdUI.Label _lblPageTitle = null!;
        private System.Windows.Forms.Panel _sidebar = null!;
        private FlowLayoutPanel _menuPanel = null!;
        private AntdUI.Label _lblRole = null!;
        private AntdUI.Label _lblName = null!;
        private System.Windows.Forms.Panel _signOutPanel = null!;
        private AntdUI.Label _lblSignOut = null!;

        private bool dragging = false;
        private Point dragCursorPoint, dragFormPoint;
        private bool isSidebarCollapsed = false;

        private readonly Dictionary<System.Windows.Forms.Panel, Action> _menuHandlers = new();
        private System.Windows.Forms.Panel? _activeMenu;

        private Form? _currentPage;
        private readonly int _tenantId;

        // Layout containers
        private TableLayoutPanel _root = null!;
        private TableLayoutPanel _mainGrid = null!;
        private System.Windows.Forms.Panel _header = null!;

        // Theme
        private readonly Color _bgApp = ColorTranslator.FromHtml("#f6f7f8");
        private readonly Color _bgSurface = Color.White;
        private readonly Color _border = ColorTranslator.FromHtml("#e2e8f0");
        private readonly Color _textMain = ColorTranslator.FromHtml("#0f172a");
        private readonly Color _textSub = ColorTranslator.FromHtml("#64748b");
        private readonly Color _hover = ColorTranslator.FromHtml("#f8fafc");
        private readonly Color _accent = ColorTranslator.FromHtml("#2563eb");
        private readonly Color _selectedBg = ColorTranslator.FromHtml("#f1f5ff");

        public UserDashboard(int tenantId)
        {
            _tenantId = tenantId;
            InitializeComponent();

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;

            SetupShell();
            Shown += (s, e) => NavigateToBrowse();
        }

        private void NavigateToBrowse()
        {
            NavigateTo(new RentAllSearch(this), "Browse Properties");
        }

        public void SetSidebarVisibility(bool visible)
        {
            _sidebar.Visible = visible;
        }

        private void SetupShell()
        {
            Controls.Clear();
            Size = new Size(1500, 900);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.None;
            DoubleBuffered = true;
            Text = "Tenant Portal";
            BackColor = _bgApp;

            // Root: [Sidebar | Main]
            _root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = _bgApp,
                ColumnCount = 2,
                RowCount = 1,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };
            _root.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 280));
            _root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            _root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            BuildSidebar();
            BuildMainArea();

            Controls.Add(_root);

            // Drag support
            AttachDragEvents(_header);
            AttachDragEvents(_lblPageTitle);
            AttachDragEvents(_sidebar);
        }

        private void BuildSidebar()
        {
            _sidebar = new System.Windows.Forms.Panel
            {
                Dock = DockStyle.Fill,
                BackColor = _bgSurface,
                Padding = new Padding(16, 16, 16, 16)
            };

            // Right border
            var borderRight = new System.Windows.Forms.Panel
            {
                Dock = DockStyle.Right,
                Width = 1,
                BackColor = _border
            };
            _sidebar.Controls.Add(borderRight);

            // Layout inside sidebar: header, menu, footer
            var sidebarGrid = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                ColumnCount = 1,
                RowCount = 3,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };
            sidebarGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 96));
            sidebarGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            sidebarGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 56));

            // Header panel
            var headerPanel = new System.Windows.Forms.Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent };

            var btnToggle = CreateIconButton(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "menu.png"));
            btnToggle.Margin = new Padding(0, 0, 0, 10);
            btnToggle.Click += (s, e) => ToggleSidebar();

            _lblName = new AntdUI.Label
            {
                Text = "Tenant Portal",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = _textMain,
                AutoSize = true,
                Location = new Point(0, 40)
            };

            _lblRole = new AntdUI.Label
            {
                Text = "Find your home",
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = _textSub,
                AutoSize = true,
                Location = new Point(0, 62)
            };

            headerPanel.Controls.Add(btnToggle);
            headerPanel.Controls.Add(_lblName);
            headerPanel.Controls.Add(_lblRole);

            // Menu
            _menuPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = Color.Transparent,
                Padding = new Padding(0, 8, 0, 8)
            };

            string assetsPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets");
            AddMenuItem("Browse Homes", System.IO.Path.Combine(assetsPath, "dashboard.png"), () => NavigateToBrowse(), isDefault: true);
            AddMenuItem("My Rentals", System.IO.Path.Combine(assetsPath, "properties.png"), () => NavigateTo(new MyRentals(_tenantId), "My Rentals"));
            AddMenuItem("Requests", System.IO.Path.Combine(assetsPath, "calendar.png"), () => NavigateTo(new TenantRequestsList(_tenantId), "Requests"));
            AddMenuItem("Payments", System.IO.Path.Combine(assetsPath, "payment.png"), () => NavigateTo(new TenantPaymentList(_tenantId), "Payments"));
            AddMenuItem("Settings", System.IO.Path.Combine(assetsPath, "settings.png"), () => NavigateTo(new Settings(_tenantId), "Account Settings"));

            // Footer (Sign out)
            _signOutPanel = new System.Windows.Forms.Panel
            {
                Dock = DockStyle.Fill,
                BackColor = _hover,
                Padding = new Padding(12, 10, 12, 10),
                Cursor = Cursors.Hand
            };

            var logoutIcon = new PictureBox
            {
                Size = new Size(18, 18),
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(8, 16),
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
                AutoSize = true,
                Location = new Point(36, 14),
                BackColor = Color.Transparent
            };

            _signOutPanel.Controls.Add(logoutIcon);
            _signOutPanel.Controls.Add(_lblSignOut);

            EventHandler signOutClick = (s, e) => Close();
            _signOutPanel.Click += signOutClick;
            logoutIcon.Click += signOutClick;
            _lblSignOut.Click += signOutClick;

            _signOutPanel.MouseEnter += (s, e) => _signOutPanel.BackColor = ColorTranslator.FromHtml("#eef2ff");
            _signOutPanel.MouseLeave += (s, e) => _signOutPanel.BackColor = _hover;

            sidebarGrid.Controls.Add(headerPanel, 0, 0);
            sidebarGrid.Controls.Add(_menuPanel, 0, 1);
            sidebarGrid.Controls.Add(_signOutPanel, 0, 2);

            _sidebar.Controls.Add(sidebarGrid);
            _root.Controls.Add(_sidebar, 0, 0);
        }

        private void BuildMainArea()
        {
            // Main grid: header row + content row
            _mainGrid = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = _bgApp,
                ColumnCount = 1,
                RowCount = 2,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };
            _mainGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 64));
            _mainGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            _header = new System.Windows.Forms.Panel
            {
                Dock = DockStyle.Fill,
                BackColor = _bgSurface,
                Padding = new Padding(24, 14, 16, 14)
            };

            // Header layout: Title (left) + window controls (right)
            var headerGrid = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Color.Transparent,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };
            headerGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            headerGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110));

            _lblPageTitle = new AntdUI.Label
            {
                Text = "Browse Properties",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml("#1e293b"),
                Dock = DockStyle.Fill,
                AutoSize = false
            };

            var windowBtns = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = Color.Transparent,
                Margin = Padding.Empty,
                Padding = new Padding(0, 0, 0, 0)
            };

            var btnMin = new AntdUI.Button
            {
                Text = "–",
                Size = new Size(45, 30),
                Type = TTypeMini.Success,
                Margin = new Padding(0, 0, 8, 0)
            };
            btnMin.Click += (s, e) => WindowState = FormWindowState.Minimized;

            var btnClose = new AntdUI.Button
            {
                Text = "✕",
                Size = new Size(45, 30),
                Type = TTypeMini.Error,
                Margin = new Padding(0)
            };
            btnClose.Click += (s, e) => Application.Exit();

            windowBtns.Controls.Add(btnMin);
            windowBtns.Controls.Add(btnClose);

            headerGrid.Controls.Add(_lblPageTitle, 0, 0);
            headerGrid.Controls.Add(windowBtns, 1, 0);

            _header.Controls.Add(headerGrid);

            _contentPanel = new System.Windows.Forms.Panel
            {
                Dock = DockStyle.Fill,
                BackColor = _bgApp,
                Padding = new Padding(24, 20, 24, 24)
            };

            _mainGrid.Controls.Add(_header, 0, 0);
            _mainGrid.Controls.Add(_contentPanel, 0, 1);

            _root.Controls.Add(_mainGrid, 1, 0);
        }

        private System.Windows.Forms.Panel CreateIconButton(string iconPath)
        {
            var btn = new System.Windows.Forms.Panel
            {
                Size = new Size(36, 36),
                BackColor = ColorTranslator.FromHtml("#f8fafc"),
                Cursor = Cursors.Hand,
                Margin = new Padding(0),
                Padding = new Padding(8)
            };

            var pic = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };
            if (System.IO.File.Exists(iconPath))
            {
                try { pic.Image = Image.FromFile(iconPath); } catch { }
            }

            btn.Controls.Add(pic);
            btn.MouseEnter += (s, e) => btn.BackColor = ColorTranslator.FromHtml("#eef2ff");
            btn.MouseLeave += (s, e) => btn.BackColor = ColorTranslator.FromHtml("#f8fafc");
            pic.Click += (s, e) => ToggleSidebar();

            return btn;
        }

        private void AddMenuItem(string text, string iconPath, Action onClick, bool isDefault = false)
        {
            var item = new System.Windows.Forms.Panel
            {
                Width = 240,
                Height = 44,
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                Margin = new Padding(0, 2, 0, 2),
                Padding = new Padding(10, 8, 10, 8)
            };

            // Subtle selection indicator (left bar)
            var indicator = new System.Windows.Forms.Panel
            {
                Width = 4,
                Dock = DockStyle.Left,
                BackColor = Color.Transparent
            };
            item.Controls.Add(indicator);

            var icon = new PictureBox
            {
                Size = new Size(18, 18),
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(14, 13),
                BackColor = Color.Transparent
            };
            if (System.IO.File.Exists(iconPath))
            {
                try { icon.Image = Image.FromFile(iconPath); } catch { }
            }

            var lbl = new AntdUI.Label
            {
                Text = text,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = ColorTranslator.FromHtml("#334155"),
                AutoSize = true,
                BackColor = Color.Transparent,
                Location = new Point(42, 11)
            };

            item.Controls.Add(icon);
            item.Controls.Add(lbl);

            void Activate()
            {
                if (_activeMenu != null)
                {
                    _activeMenu.BackColor = Color.Transparent;
                    foreach (Control c in _activeMenu.Controls)
                    {
                        if (c is System.Windows.Forms.Panel p && p.Dock == DockStyle.Left && p.Width == 4)
                            p.BackColor = Color.Transparent;
                        if (c is AntdUI.Label l)
                        {
                            l.ForeColor = ColorTranslator.FromHtml("#334155");
                            l.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                        }
                    }
                }

                _activeMenu = item;
                item.BackColor = _selectedBg;
                indicator.BackColor = _accent;
                lbl.ForeColor = _accent;
                lbl.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            }

            EventHandler handler = (s, e) =>
            {
                Activate();
                onClick();
            };

            item.Click += handler;
            icon.Click += handler;
            lbl.Click += handler;

            item.MouseEnter += (s, e) =>
            {
                if (_activeMenu != item) item.BackColor = _hover;
            };
            item.MouseLeave += (s, e) =>
            {
                if (_activeMenu != item) item.BackColor = Color.Transparent;
            };

            _menuPanel.Controls.Add(item);
            _menuHandlers[item] = onClick;

            if (isDefault)
            {
                Activate();
            }
        }

        private void ToggleSidebar()
        {
            isSidebarCollapsed = !isSidebarCollapsed;

            _root.ColumnStyles[0].Width = isSidebarCollapsed ? 76 : 280;

            bool showText = !isSidebarCollapsed;
            _lblName.Visible = showText;
            _lblRole.Visible = showText;
            _lblSignOut.Visible = showText;

            foreach (Control c in _menuPanel.Controls)
            {
                if (c is System.Windows.Forms.Panel p)
                {
                    // label is AntdUI.Label
                    foreach (Control child in p.Controls)
                    {
                        if (child is AntdUI.Label l) l.Visible = showText;
                        if (child is PictureBox pb)
                        {
                            // keep icon aligned
                            pb.Location = showText ? new Point(42, 13) : new Point(26, 13);
                        }
                    }

                    p.Width = showText ? 240 : 52;
                    p.Padding = showText ? new Padding(10, 8, 10, 8) : new Padding(10, 8, 10, 8);
                }
            }
        }

        private void NavigateTo(Form page, string title)
        {
            if (_currentPage != null)
            {
                try
                {
                    _contentPanel.Controls.Remove(_currentPage);
                    _currentPage.Close();
                    _currentPage.Dispose();
                }
                catch { }
                _currentPage = null;
            }

            _contentPanel.Controls.Clear();

            _currentPage = page;
            page.TopLevel = false;
            page.FormBorderStyle = FormBorderStyle.None;
            page.Dock = DockStyle.Fill;
            page.WindowState = FormWindowState.Normal;

            _contentPanel.Controls.Add(page);
            page.Show();

            _contentPanel.PerformLayout();
            page.PerformLayout();

            if (_lblPageTitle != null) _lblPageTitle.Text = title;
        }

        private void AttachDragEvents(Control ctrl)
        {
            ctrl.MouseDown += (s, e) => { dragging = true; dragCursorPoint = Cursor.Position; dragFormPoint = Location; };
            ctrl.MouseMove += (s, e) =>
            {
                if (dragging)
                {
                    Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                    Location = Point.Add(dragFormPoint, new Size(dif));
                }
            };
            ctrl.MouseUp += (s, e) => dragging = false;
        }
    }
}