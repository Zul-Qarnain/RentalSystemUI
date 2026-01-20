using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using RentalSystemUI.Services;
using AntdUI;

namespace RentalSystemUI.Forms.AdminDashboardSections
{
    public class AdminHome : Form
    {
        private readonly AdminService _adminService = new AdminService();
        private FlowLayoutPanel _statsPanel = null!;

        public AdminHome()
        {
            // Skip runtime code in designer
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return;
            }

            BuildUI();
            LoadStats();
        }

        private void BuildUI()
        {
            BackColor = ColorTranslator.FromHtml("#f6f7f8");
            Padding = new Padding(0);

            var root = new System.Windows.Forms.Panel { Dock = DockStyle.Fill, BackColor = BackColor, Padding = new Padding(20) };

            var header = new AntdUI.Label
            {
                Text = "Admin Overview",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 54,
                ForeColor = Color.FromArgb(15, 23, 42),
                TextAlign = ContentAlignment.MiddleLeft
            };

            var sub = new AntdUI.Label
            {
                Text = "System summary and key metrics",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Dock = DockStyle.Top,
                Height = 28,
                ForeColor = Color.FromArgb(100, 116, 139),
                TextAlign = ContentAlignment.MiddleLeft
            };

            _statsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                AutoScroll = true,
                WrapContents = true,
                Padding = new Padding(0, 10, 0, 0)
            };

            root.Controls.Add(_statsPanel);
            root.Controls.Add(sub);
            root.Controls.Add(header);

            Controls.Add(root);
        }

        private void LoadStats()
        {
            if (_statsPanel == null) return;

            var stats = _adminService.GetStats();
            _statsPanel.Controls.Clear();

            string GetStatString(string key)
            {
                if (stats == null) return "0";
                if (!stats.TryGetValue(key, out var v) || v == null) return "0";
                return v.ToString() ?? "0";
            }

            _statsPanel.Controls.Add(CreateStatCard("Total Users", GetStatString("TotalUsers"), Color.FromArgb(22, 119, 255)));
            _statsPanel.Controls.Add(CreateStatCard("Landlords", GetStatString("TotalLandlords"), Color.FromArgb(82, 196, 26)));
            _statsPanel.Controls.Add(CreateStatCard("Tenants", GetStatString("TotalTenants"), Color.FromArgb(250, 173, 20)));
            _statsPanel.Controls.Add(CreateStatCard("Transactions", GetStatString("TotalTransactions"), Color.FromArgb(114, 46, 209)));

            decimal revenue = 0m;
            if (stats != null && stats.TryGetValue("TotalRevenue", out var revObj) && revObj != null)
            {
                try { revenue = Convert.ToDecimal(revObj); } catch { }
            }
            _statsPanel.Controls.Add(CreateStatCard("Total Revenue", $"৳{revenue:N0}", Color.FromArgb(245, 34, 45)));
        }

        private Control CreateStatCard(string title, string value, Color color)
        {
            var card = new AntdUI.Panel
            {
                Size = new Size(260, 140),
                BackColor = Color.White,
                Padding = new Padding(20),
                Radius = 14,
                Margin = new Padding(0, 0, 18, 18)
            };

            var lblTitle = new AntdUI.Label
            {
                Text = title,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(100, 116, 139),
                Dock = DockStyle.Top,
                Height = 28
            };

            var lblValue = new AntdUI.Label
            {
                Text = value ?? "0",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = color,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };

            card.Controls.Add(lblValue);
            card.Controls.Add(lblTitle);

            return card;
        }
    }
}
