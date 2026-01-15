using System;
using System.Drawing;
using System.Windows.Forms;
using AntdUI;
using RentalSystemUI.Services;

namespace RentalSystemUI.Forms.DashboardSections
{
    public class DashboardHome : UserControl
    {
        private LandlordService _service = new LandlordService();
        private int _landlordId = 1;

        public DashboardHome()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = ColorTranslator.FromHtml("#f6f7f8"); // Matches Shell
            InitializeUI();
        }

        private void InitializeUI()
        {
            // Reduced bottom padding to avoid scroll
            FlowLayoutPanel mainFlow = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, WrapContents = false, AutoScroll = true, Padding = new Padding(0, 0, 10, 10) };
            this.Controls.Add(mainFlow);

            // 1. ALERT BANNER (Compact)
            // Height 80 -> 60
            AntdUI.Panel alertPanel = new AntdUI.Panel { Width = 1100, Height = 60, Radius = 12, BackColor = ColorTranslator.FromHtml("#fffbeb"), BorderColor = ColorTranslator.FromHtml("#fef3c7"), BorderWidth = 1, Margin = new Padding(0, 0, 0, 16) };
            
            // Icon
            AntdUI.Button alertIcon = new AntdUI.Button { IconSvg = "warning", BackColor = ColorTranslator.FromHtml("#fef3c7"), ForeColor = ColorTranslator.FromHtml("#d97706"), Size = new Size(32, 32), Radius = 16, Location = new Point(16, 14), Type = TTypeMini.Default, BorderWidth = 0 };
            
            // Text
            AntdUI.Label alertTitle = new AntdUI.Label { Text = "Action Required", Font = new Font("Manrope", 9, FontStyle.Bold), ForeColor = ColorTranslator.FromHtml("#0f172a"), Location = new Point(60, 12), AutoSize = true };
            AntdUI.Label alertDesc = new AntdUI.Label { Text = "You have 2 maintenance requests pending approval.", Font = new Font("Manrope", 9, FontStyle.Regular), ForeColor = ColorTranslator.FromHtml("#475569"), Location = new Point(60, 32), AutoSize = true };
            
            // Action Button
            AntdUI.Button alertBtn = new AntdUI.Button { Text = "Review", Font = new Font("Manrope", 9, FontStyle.Bold), BackColor = Color.White, ForeColor = ColorTranslator.FromHtml("#334155"), Location = new Point(980, 12), Size = new Size(100, 36), Radius = 8, BorderWidth = 1 };

            alertPanel.Controls.Add(alertBtn);
            alertPanel.Controls.Add(alertDesc);
            alertPanel.Controls.Add(alertTitle);
            alertPanel.Controls.Add(alertIcon);
            mainFlow.Controls.Add(alertPanel);

            // 2. STATS GRID (4 Cards)
            // Window 1366 - Sidebar 250 - Padding 48 = 1068px available.
            // 4 * 260 = 1040. Fits well.
            // Height reduced 190 -> 150
            FlowLayoutPanel statsFlow = new FlowLayoutPanel { Width = 1080, Height = 150, FlowDirection = FlowDirection.LeftToRight, WrapContents = false, Margin = new Padding(0, 0, 0, 16) };
            
            var stats = _service.GetStats(_landlordId);

            statsFlow.Controls.Add(CreateStatCard(
                "Total Properties", stats.TotalProps + " Units", "+2 this month", 
                "apartment", ColorTranslator.FromHtml("#eff6ff"), ColorTranslator.FromHtml("#137fec"), 
                ColorTranslator.FromHtml("#10b981") 
            ));

            statsFlow.Controls.Add(CreateStatCard(
                "Pending Requests", stats.PendingReqs + " Pending", "Needs Action", 
                "file-text", ColorTranslator.FromHtml("#fffbeb"), ColorTranslator.FromHtml("#d97706"), 
                ColorTranslator.FromHtml("#d97706") 
            ));

            statsFlow.Controls.Add(CreateStatCard(
                "Monthly Earnings", "$" + stats.MonthlyEarnings.ToString("N0"), "+12% vs last month", 
                "dollar", ColorTranslator.FromHtml("#ecfdf5"), ColorTranslator.FromHtml("#10b981"), 
                ColorTranslator.FromHtml("#10b981") 
            ));

            statsFlow.Controls.Add(CreateStatCard(
                "Unpaid Payments", "$" + stats.Unpaid.ToString("N0"), "2 Tenants overdue",
                "warning", ColorTranslator.FromHtml("#fff1f2"), ColorTranslator.FromHtml("#f43f5e"), 
                ColorTranslator.FromHtml("#f43f5e") 
            ));

            mainFlow.Controls.Add(statsFlow);

            // 3. WIDGETS SECTION (Split Layout)
            // Width 1080 to accommodate wider widgets
            // Height reduced 420 -> 340
            FlowLayoutPanel widgetsFlow = new FlowLayoutPanel { Width = 1080, Height = 350, FlowDirection = FlowDirection.LeftToRight, WrapContents = false };
            
            widgetsFlow.Controls.Add(CreateListWidget("Latest Tenant Requests"));
            widgetsFlow.Controls.Add(CreateListWidget("Latest Payments", true));

            mainFlow.Controls.Add(widgetsFlow);
        }

        private AntdUI.Panel CreateStatCard(string title, string mainVal, string subVal, string icon, Color bgIcon, Color fgIcon, Color fgTrend)
        {
            // Width 260 (wider), Height 140 (compact)
            AntdUI.Panel p = new AntdUI.Panel { Width = 260, Height = 140, BackColor = Color.White, Radius = 12, Shadow = 4, Margin = new Padding(0, 0, 10, 0) };
            
            AntdUI.Button btnIcon = new AntdUI.Button { IconSvg = icon, BackColor = bgIcon, ForeColor = fgIcon, Size = new Size(32, 32), Radius = 8, Location = new Point(20, 20), Type = TTypeMini.Default, BorderWidth = 0 };
            AntdUI.Label lblTitle = new AntdUI.Label { Text = title, Font = new Font("Manrope", 9, FontStyle.Bold), ForeColor = ColorTranslator.FromHtml("#64748b"), Location = new Point(60, 26), AutoSize = true };
            
            // Compact vertical spacing
            AntdUI.Label lblMain = new AntdUI.Label { Text = mainVal, Font = new Font("Manrope", 16, FontStyle.Bold), ForeColor = ColorTranslator.FromHtml("#0f172a"), Location = new Point(20, 65), AutoSize = true };
             AntdUI.Label lblSub = new AntdUI.Label { Text = subVal, Font = new Font("Manrope", 8, FontStyle.Bold), ForeColor = fgTrend, Location = new Point(20, 95), AutoSize = true };

            p.Controls.Add(lblSub);
            p.Controls.Add(lblMain);
            p.Controls.Add(lblTitle);
            p.Controls.Add(btnIcon);
            return p;
        }
        private AntdUI.Panel CreateListWidget(string title, bool isPayment = false)
        {
            // Width 530 (wider), Height 340 (compact)
            AntdUI.Panel widget = new AntdUI.Panel { Width = 530, Height = 340, BackColor = Color.White, Radius = 12, Shadow = 4, Margin = new Padding(0, 0, 16, 0) };
            
            AntdUI.Label lbl = new AntdUI.Label { Text = title, Font = new Font("Manrope", 11, FontStyle.Bold), Location = new Point(24, 20), AutoSize = true, ForeColor = ColorTranslator.FromHtml("#0f172a") };
            AntdUI.Button btnView = new AntdUI.Button { Text = isPayment ? "View History" : "View All", ForeColor = ColorTranslator.FromHtml("#137fec"), Location = new Point(410, 16), Type = TTypeMini.Default, BorderWidth = 0, BackColor = Color.Transparent, Font = new Font("Manrope", 8, FontStyle.Bold) };
            
            System.Windows.Forms.Panel div = new System.Windows.Forms.Panel { Size = new Size(530, 1), BackColor = ColorTranslator.FromHtml("#f1f5f9"), Location = new Point(0, 50) };
            
            widget.Controls.Add(div);
            widget.Controls.Add(btnView);
            widget.Controls.Add(lbl);

            // List Flow Height reduced 
            FlowLayoutPanel listFlow = new FlowLayoutPanel { Location = new Point(0, 51), Size = new Size(530, 280), FlowDirection = FlowDirection.TopDown };
            
            if (isPayment) {
                 listFlow.Controls.Add(CreatePaymentItem("Unit 101 - Rent", "Oct 24", "$1,200", true));
                 listFlow.Controls.Add(CreatePaymentItem("Unit 305 - Deposit", "Oct 24", "$1,450", false));
                 listFlow.Controls.Add(CreatePaymentItem("Unit 204 - Rent", "Oct 23", "$1,150", true));
                 listFlow.Controls.Add(CreatePaymentItem("Unit 102 - Fee", "Oct 22", "$50", true)); // Added 4th item
            } else {
                 listFlow.Controls.Add(CreateRequestItem("Leak in Unit 402", "Urgent", "#be123c", "#ffe4e6", "John Doe")); 
                 listFlow.Controls.Add(CreateRequestItem("Key replacement", "Low Priority", "#475569", "#f1f5f9", "Sarah Smith")); 
                 listFlow.Controls.Add(CreateRequestItem("Broken window lock", "Medium", "#b45309", "#fef3c7", "Mike Ross")); 
                 listFlow.Controls.Add(CreateRequestItem("AC Maintenance", "Low", "#10b981", "#ecfdf5", "Admin")); // Added 4th item
            }

            widget.Controls.Add(listFlow);

            return widget;
        }

        private Control CreateRequestItem(string title, string badge, string badgeFg, string badgeBg, string name)
        {
            // Height 80 -> 60
            System.Windows.Forms.Panel p = new System.Windows.Forms.Panel { Size = new Size(530, 60) };
            AntdUI.Avatar av = new AntdUI.Avatar { Text = name.Substring(0,1), Size = new Size(32, 32), BackColor = Color.LightGray, Location = new Point(24, 14) };
            
            AntdUI.Label t = new AntdUI.Label { Text = title, Font = new Font("Manrope", 9, FontStyle.Bold), Location = new Point(66, 12), AutoSize = true };
            AntdUI.Label n = new AntdUI.Label { Text = name + " • 2h ago", Font = new Font("Manrope", 7), ForeColor = Color.Gray, Location = new Point(66, 32), AutoSize = true };
            
            AntdUI.Button b = new AntdUI.Button { Text = badge, ForeColor = ColorTranslator.FromHtml(badgeFg), BackColor = ColorTranslator.FromHtml(badgeBg), Location = new Point(280, 18), Size = new Size(80, 22), Radius = 4, Type = TTypeMini.Default, BorderWidth = 0, Font = new Font("Manrope", 7, FontStyle.Bold) };
            
            AntdUI.Button arr = new AntdUI.Button { IconSvg = "arrow-right", BackColor = ColorTranslator.FromHtml("#eff6ff"), ForeColor = ColorTranslator.FromHtml("#137fec"), Size = new Size(28, 28), Radius = 6, Location = new Point(480, 16), Type = TTypeMini.Default, BorderWidth = 0 };
            
            p.Controls.Add(arr);
            p.Controls.Add(b);
            p.Controls.Add(n);
            p.Controls.Add(t);
            p.Controls.Add(av);
            return p;
        }

        private Control CreatePaymentItem(string title, string date, string amount, bool success)
        {
            // Height 80 -> 60
            System.Windows.Forms.Panel p = new System.Windows.Forms.Panel { Size = new Size(530, 60) };
            
            AntdUI.Button icon = new AntdUI.Button { IconSvg = success ? "dollar" : "bank", BackColor = success ? ColorTranslator.FromHtml("#ecfdf5") : ColorTranslator.FromHtml("#eff6ff"), ForeColor = success ? ColorTranslator.FromHtml("#10b981") : ColorTranslator.FromHtml("#137fec"), Size = new Size(32, 32), Radius = 16, Location = new Point(24, 14), Type = TTypeMini.Default, BorderWidth = 0 };
            
            AntdUI.Label t = new AntdUI.Label { Text = title, Font = new Font("Manrope", 9, FontStyle.Bold), Location = new Point(66, 12), AutoSize = true };
            AntdUI.Label d = new AntdUI.Label { Text = date, Font = new Font("Manrope", 7), ForeColor = Color.Gray, Location = new Point(66, 32), AutoSize = true };
            
            AntdUI.Label amt = new AntdUI.Label { Text = amount, Font = new Font("Manrope", 10, FontStyle.Bold), Location = new Point(360, 12), AutoSize = true, ForeColor = ColorTranslator.FromHtml("#0f172a") };
             AntdUI.Label sts = new AntdUI.Label { Text = success ? "● Success" : "● Processing", Font = new Font("Manrope", 7, FontStyle.Bold), ForeColor = success ? ColorTranslator.FromHtml("#10b981") : ColorTranslator.FromHtml("#137fec"), Location = new Point(360, 32), AutoSize = true };

            p.Controls.Add(sts);
            p.Controls.Add(amt);
            p.Controls.Add(d);
            p.Controls.Add(t);
            p.Controls.Add(icon);
            return p;
        }
    }
}
