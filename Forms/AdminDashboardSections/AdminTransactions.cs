using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using RentalSystemUI.Services;
using AntdUI;

namespace RentalSystemUI.Forms.AdminDashboardSections
{
    public class AdminTransactions : Form
    {
        private readonly AdminService _adminService = new AdminService();
        private FlowLayoutPanel _flow = null!;

        public AdminTransactions()
        {
            // Skip runtime code in designer
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return;
            }

            BuildUI();
            LoadTransactions();
        }

        private void BuildUI()
        {
            BackColor = ColorTranslator.FromHtml("#f6f7f8");
            Padding = new Padding(20);

            var headerWrap = new System.Windows.Forms.Panel { Dock = DockStyle.Top, Height = 80, BackColor = Color.Transparent };

            var header = new AntdUI.Label
            {
                Text = "Payment History",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                Dock = DockStyle.Fill,
                Height = 60
            };

            var btnRefresh = new AntdUI.Button
            {
                Text = "Refresh",
                Type = TTypeMini.Primary,
                BackColor = Color.FromArgb(22, 119, 255),
                ForeColor = Color.White,
                Radius = 8,
                Size = new Size(120, 44),
                Location = new Point(980, 18),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            btnRefresh.Click += (s, e) => LoadTransactions();

            headerWrap.Controls.Add(btnRefresh);
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

        private void LoadTransactions()
        {
            if (_flow == null) return;

            _flow.Controls.Clear();
            DataTable dt;

            try
            {
                dt = _adminService.GetAllTransactions();
            }
            catch (Exception)
            {
                _flow.Controls.Add(new AntdUI.Label { Text = "Failed to load transactions.", AutoSize = true, ForeColor = Color.Gray });
                return;
            }

            if (dt.Rows.Count == 0)
            {
                _flow.Controls.Add(new AntdUI.Label { Text = "No transactions found. Make sure bookings are approved and payments are submitted.", AutoSize = true, ForeColor = Color.Gray });
                return;
            }

            foreach (DataRow row in dt.Rows)
            {
                _flow.Controls.Add(CreateTransactionRow(row));
            }
        }

        private Control CreateTransactionRow(DataRow row)
        {
            string id = row["PaymentID"].ToString() ?? "";
            string amount = Convert.ToDecimal(row["Amount"]).ToString("N0");
            string method = row["Method"].ToString() ?? "";
            string status = row["Status"].ToString() ?? "";

            DateTime dt = DateTime.MinValue;
            try { dt = Convert.ToDateTime(row["PaymentDate"]); } catch { }
            string date = dt == DateTime.MinValue ? "N/A" : dt.ToString("dd MMM yyyy HH:mm");

            string payer = row["PayerName"].ToString() ?? "";
            string payerEmail = row.Table.Columns.Contains("PayerEmail") ? (row["PayerEmail"].ToString() ?? "") : "";
            string payerPhone = row.Table.Columns.Contains("PayerPhone") ? (row["PayerPhone"].ToString() ?? "") : "";

            var panel = new AntdUI.Panel
            {
                Size = new Size(1100, 90),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(15, 23, 42),
                Radius = 10,
                Margin = new Padding(0, 0, 0, 10),
                Padding = new Padding(15)
            };

            var lblId = new AntdUI.Label { Text = "#" + id, Width = 80, Height = 22, AutoSize = false, Location = new Point(15, 18), Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.FromArgb(15, 23, 42), BackColor = Color.White };

            var lblPayer = new AntdUI.Label { Text = payer, Width = 300, Height = 22, AutoSize = false, Location = new Point(100, 16), Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.FromArgb(15, 23, 42), BackColor = Color.White };
            var lblPayer2 = new AntdUI.Label { Text = $"{payerEmail}   {payerPhone}", Width = 340, Height = 20, AutoSize = false, Location = new Point(100, 44), Font = new Font("Segoe UI", 9), ForeColor = Color.FromArgb(100, 116, 139), BackColor = Color.White };

            var lblAmount = new AntdUI.Label { Text = "৳" + amount, Width = 160, Height = 22, AutoSize = false, Location = new Point(460, 30), Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.FromArgb(22, 119, 255), BackColor = Color.White };
            var lblMethod = new AntdUI.Label { Text = method.ToUpperInvariant(), Width = 120, Height = 22, AutoSize = false, Location = new Point(630, 30), Font = new Font("Segoe UI", 9), ForeColor = Color.FromArgb(15, 23, 42), BackColor = Color.White };
            var lblDate = new AntdUI.Label { Text = date, Width = 200, Height = 22, AutoSize = false, Location = new Point(770, 30), Font = new Font("Segoe UI", 9), ForeColor = Color.FromArgb(100, 116, 139), BackColor = Color.White };

            var ok = status.Equals("verified", StringComparison.OrdinalIgnoreCase);
            var lblStatus = new AntdUI.Label
            {
                Text = status.ToUpperInvariant(),
                Width = 110,
                Location = new Point(985, 28),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = ok ? Color.FromArgb(82, 196, 26) : Color.FromArgb(255, 77, 79),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.White
            };

            panel.Controls.Add(lblId);
            panel.Controls.Add(lblPayer);
            panel.Controls.Add(lblPayer2);
            panel.Controls.Add(lblAmount);
            panel.Controls.Add(lblMethod);
            panel.Controls.Add(lblDate);
            panel.Controls.Add(lblStatus);

            return panel;
        }
    }
}
