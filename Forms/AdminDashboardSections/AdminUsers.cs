using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using RentalSystemUI.Services;
using AntdUI;

namespace RentalSystemUI.Forms.AdminDashboardSections
{
    public class AdminUsers : Form
    {
        private readonly AdminService _adminService = new AdminService();
        private FlowLayoutPanel _flow = null!;
        private AntdUI.Input _txtSearch = null!;
        private AntdUI.Button _btnRefresh = null!;

        private DataTable? _allUsers;

        public AdminUsers()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void InitializeComponent()
        {
            BackColor = ColorTranslator.FromHtml("#f6f7f8");
            Padding = new Padding(20);

            var headerWrap = new System.Windows.Forms.Panel { Dock = DockStyle.Top, Height = 115, BackColor = Color.Transparent };

            var header = new AntdUI.Label
            {
                Text = "User Management",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 60
            };

            var searchRow = new System.Windows.Forms.Panel { Dock = DockStyle.Top, Height = 55 };

            _txtSearch = new AntdUI.Input
            {
                PlaceholderText = "Search by name, email, or phone...",
                Radius = 8,
                Size = new Size(520, 44),
                Location = new Point(0, 5)
            };

            _btnRefresh = new AntdUI.Button
            {
                Text = "Refresh",
                Type = TTypeMini.Primary,
                BackColor = Color.FromArgb(22, 119, 255),
                ForeColor = Color.White,
                Radius = 8,
                Size = new Size(120, 44),
                Location = new Point(540, 5)
            };

            _txtSearch.TextChanged += (s, e) => ApplyFilter();
            _btnRefresh.Click += (s, e) => LoadUsers();

            searchRow.Controls.Add(_txtSearch);
            searchRow.Controls.Add(_btnRefresh);

            headerWrap.Controls.Add(searchRow);
            headerWrap.Controls.Add(header);

            _flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true
            };

            Controls.Add(_flow);
            Controls.Add(headerWrap);
        }

        private void LoadUsers()
        {
            _flow.Controls.Clear();

            _allUsers = _adminService.GetAllUsers();
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            _flow.Controls.Clear();

            if (_allUsers == null)
            {
                _flow.Controls.Add(new AntdUI.Label { Text = "No users found.", AutoSize = true, ForeColor = Color.Gray });
                return;
            }

            if (_allUsers.Rows.Count == 0)
            {
                _flow.Controls.Add(new AntdUI.Label { Text = "No users found in database.", AutoSize = true, ForeColor = Color.Gray });
                return;
            }

            var q = (_txtSearch?.Text ?? string.Empty).Trim();

            var rows = _allUsers.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(q))
            {
                var ql = q.ToLowerInvariant();
                rows = rows.Where(r =>
                    (r["FullName"]?.ToString() ?? string.Empty).ToLowerInvariant().Contains(ql) ||
                    (r["Email"]?.ToString() ?? string.Empty).ToLowerInvariant().Contains(ql) ||
                    (r["Phone"]?.ToString() ?? string.Empty).ToLowerInvariant().Contains(ql));
            }

            var list = rows.ToList();
            if (list.Count == 0)
            {
                _flow.Controls.Add(new AntdUI.Label { Text = "No matching users.", AutoSize = true, ForeColor = Color.Gray });
                return;
            }

            foreach (DataRow row in list)
            {
                _flow.Controls.Add(CreateUserRow(row));
            }
        }

        private Control CreateUserRow(DataRow row)
        {
            int userId = Convert.ToInt32(row["UserID"]);
            string name = row["FullName"].ToString() ?? "";
            string email = row["Email"].ToString() ?? "";
            string phone = row["Phone"].ToString() ?? "";
            string type = row["UserType"].ToString() ?? "";
            bool isActive = Convert.ToBoolean(row["IsActive"]);

            var panel = new AntdUI.Panel
            {
                Size = new Size(1100, 78),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(15, 23, 42),
                Radius = 10,
                Margin = new Padding(0, 0, 0, 10),
                Padding = new Padding(15)
            };

            var lblId = new AntdUI.Label { Text = "#" + userId, Width = 60, Height = 22, AutoSize = false, Location = new Point(15, 28), Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.FromArgb(2, 6, 23) };
            var lblName = new AntdUI.Label { Text = name, Width = 220, Height = 22, AutoSize = false, Location = new Point(80, 16), Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.FromArgb(15, 23, 42), BackColor = Color.White };
            var lblEmail = new AntdUI.Label { Text = email, Width = 310, Height = 20, AutoSize = false, Location = new Point(80, 42), Font = new Font("Segoe UI", 9), ForeColor = Color.FromArgb(100, 116, 139), BackColor = Color.White };
            var lblPhone = new AntdUI.Label { Text = phone, Width = 170, Height = 20, AutoSize = false, Location = new Point(405, 42), Font = new Font("Segoe UI", 9), ForeColor = Color.FromArgb(100, 116, 139), BackColor = Color.White };

            var lblType = new AntdUI.Label
            {
                Text = type.ToUpperInvariant(),
                Width = 120,
                Height = 22,
                AutoSize = false,
                Location = new Point(610, 28),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = GetTypeColor(type),
                BackColor = Color.White
            };

            var lblActive = new AntdUI.Label
            {
                Text = isActive ? "ACTIVE" : "DISABLED",
                Width = 110,
                Height = 22,
                AutoSize = false,
                Location = new Point(740, 28),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = isActive ? Color.FromArgb(82, 196, 26) : Color.FromArgb(255, 77, 79),
                BackColor = Color.White
            };

            var btnDelete = new AntdUI.Button
            {
                Text = "Delete",
                Type = TTypeMini.Primary,
                BackColor = Color.FromArgb(255, 77, 79),
                ForeColor = Color.White,
                Size = new Size(90, 34),
                Location = new Point(995, 22),
                Radius = 8
            };

            btnDelete.Click += (s, e) =>
            {
                if (userId == AppSession.CurrentUser?.UserID)
                {
                    AntdUI.Message.error(this, "You cannot delete yourself!");
                    return;
                }

                if (MessageBox.Show($"Are you sure you want to delete {name}? This will remove all their property listings, bookings, and transaction history. This action cannot be undone.", "Delete User", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (_adminService.DeleteUser(userId))
                    {
                        AntdUI.Message.success(this, "User deleted successfully.");
                        LoadUsers();
                    }
                    else
                    {
                        AntdUI.Message.error(this, "Failed to delete user.");
                    }
                }
            };

            panel.Controls.Add(lblId);
            panel.Controls.Add(lblName);
            panel.Controls.Add(lblEmail);
            panel.Controls.Add(lblPhone);
            panel.Controls.Add(lblType);
            panel.Controls.Add(lblActive);
            panel.Controls.Add(btnDelete);

            return panel;
        }

        private Color GetTypeColor(string type)
        {
            switch ((type ?? string.Empty).ToLowerInvariant())
            {
                case "superadmin": return Color.FromArgb(114, 46, 209);
                case "landlord": return Color.FromArgb(82, 196, 26);
                case "tenant": return Color.FromArgb(22, 119, 255);
                default: return Color.Gray;
            }
        }
    }
}
