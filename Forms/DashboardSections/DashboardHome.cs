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
            FlowLayoutPanel mainFlow = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, WrapContents = false, AutoScroll = true, Padding = new Padding(0, 0, 20, 20) };
            this.Controls.Add(mainFlow);

            // 1. ALERT BANNER
            // bg-amber-50 ( #fffbeb ) border-amber-100 ( #fef3c7 )
            AntdUI.Panel alertPanel = new AntdUI.Panel { Width = 1100, Height = 80, Radius = 12, BackColor = ColorTranslator.FromHtml("#fffbeb"), BorderColor = ColorTranslator.FromHtml("#fef3c7"), BorderWidth = 1, Margin = new Padding(0, 0, 0, 24) };
            
            // Icon
            AntdUI.Button alertIcon = new AntdUI.Button { IconSvg = "warning", BackColor = ColorTranslator.FromHtml("#fef3c7"), ForeColor = ColorTranslator.FromHtml("#d97706"), Size = new Size(40, 40), Radius = 20, Location = new Point(16, 20), Type = TTypeMini.Default, BorderWidth = 0 };
            
            // Text
            AntdUI.Label alertTitle = new AntdUI.Label { Text = "Action Required", Font = new Font("Manrope", 10, FontStyle.Bold), ForeColor = ColorTranslator.FromHtml("#0f172a"), Location = new Point(70, 18), AutoSize = true };
            AntdUI.Label alertDesc = new AntdUI.Label { Text = "You have 2 maintenance requests pending approval over 48 hours.", Font = new Font("Manrope", 9, FontStyle.Regular), ForeColor = ColorTranslator.FromHtml("#475569"), Location = new Point(70, 40), AutoSize = true };
            
            // Action Button
            AntdUI.Button alertBtn = new AntdUI.Button { Text = "Review Requests", Font = new Font("Manrope", 9, FontStyle.Bold), BackColor = Color.White, ForeColor = ColorTranslator.FromHtml("#334155"), Location = new Point(930, 20), Size = new Size(140, 40), Radius = 8, BorderWidth = 1 }; // Removed BorderColor

            alertPanel.Controls.Add(alertBtn);
            alertPanel.Controls.Add(alertDesc);
            alertPanel.Controls.Add(alertTitle);
            alertPanel.Controls.Add(alertIcon);
            mainFlow.Controls.Add(alertPanel);

            // 2. STATS GRID (4 Cards)
            // Available: 1420 - 260(Sidebar) - 64(Pad) = ~1100px. 
            // 4 Cards * 260 = 1040 + 48(Margins) = 1088. Fits.
            FlowLayoutPanel statsFlow = new FlowLayoutPanel { Width = 1100, Height = 190, FlowDirection = FlowDirection.LeftToRight, WrapContents = false, Margin = new Padding(0, 0, 0, 24) };
            
            var stats = _service.GetStats(_landlordId);

            // ... (Cards) ...
            


            // Blue Card (Total Properties)
            statsFlow.Controls.Add(CreateStatCard(
                "Total Properties", stats.TotalProps + " Units", "+2 this month", 
                "apartment", ColorTranslator.FromHtml("#eff6ff"), ColorTranslator.FromHtml("#137fec"), 
                ColorTranslator.FromHtml("#10b981") // Green Trend
            ));

            // Yellow Card (Pending)
            statsFlow.Controls.Add(CreateStatCard(
                "Pending Requests", stats.PendingReqs + " Pending", "Needs Action", 
                "file-text", ColorTranslator.FromHtml("#fffbeb"), ColorTranslator.FromHtml("#d97706"), 
                ColorTranslator.FromHtml("#d97706") // Amber Trend
            ));

            // Green Card (Earnings)
            statsFlow.Controls.Add(CreateStatCard(
                "Monthly Earnings", "$" + stats.MonthlyEarnings.ToString("N0"), "+12% vs last month", 
                "dollar", ColorTranslator.FromHtml("#ecfdf5"), ColorTranslator.FromHtml("#10b981"), 
                ColorTranslator.FromHtml("#10b981") // Green Trend
            ));

            // Red Card (Unpaid)
            statsFlow.Controls.Add(CreateStatCard(
                "Unpaid Payments", "$" + stats.Unpaid.ToString("N0"), "2 Tenants overdue", // Fixed UnpaidCount
                "warning", ColorTranslator.FromHtml("#fff1f2"), ColorTranslator.FromHtml("#f43f5e"), 
                ColorTranslator.FromHtml("#f43f5e") // Rose Trend
            ));

            mainFlow.Controls.Add(statsFlow);

            // 3. WIDGETS SECTION (Split Layout)
            FlowLayoutPanel widgetsFlow = new FlowLayoutPanel { Width = 1100, Height = 420, FlowDirection = FlowDirection.LeftToRight, WrapContents = false };
            
            // Latest Requests Widget
            widgetsFlow.Controls.Add(CreateListWidget("Latest Tenant Requests"));
            
            // Latest Payments Widget
            widgetsFlow.Controls.Add(CreateListWidget("Latest Payments", true));

            mainFlow.Controls.Add(widgetsFlow);
        }

        private AntdUI.Panel CreateStatCard(string title, string mainVal, string subVal, string icon, Color bgIcon, Color fgIcon, Color fgTrend)
        {
            // Width 260 to fit 4 in a row
            AntdUI.Panel p = new AntdUI.Panel { Width = 260, Height = 170, BackColor = Color.White, Radius = 12, Shadow = 4, Margin = new Padding(0, 0, 16, 0) };
            
            // Header Row
            AntdUI.Button btnIcon = new AntdUI.Button { IconSvg = icon, BackColor = bgIcon, ForeColor = fgIcon, Size = new Size(36, 36), Radius = 8, Location = new Point(20, 20), Type = TTypeMini.Default, BorderWidth = 0 };
            AntdUI.Label lblTitle = new AntdUI.Label { Text = title, Font = new Font("Manrope", 9, FontStyle.Bold), ForeColor = ColorTranslator.FromHtml("#64748b"), Location = new Point(66, 28), AutoSize = true };
            
            // Content
            AntdUI.Label lblMain = new AntdUI.Label { Text = mainVal, Font = new Font("Manrope", 20, FontStyle.Bold), ForeColor = ColorTranslator.FromHtml("#0f172a"), Location = new Point(20, 70), AutoSize = true };
             AntdUI.Label lblSub = new AntdUI.Label { Text = subVal, Font = new Font("Manrope", 9, FontStyle.Bold), ForeColor = fgTrend, Location = new Point(20, 110), AutoSize = true };

            p.Controls.Add(lblSub);
            p.Controls.Add(lblMain);
            p.Controls.Add(lblTitle);
            p.Controls.Add(btnIcon);
            return p;
        }
        private AntdUI.Panel CreateListWidget(string title, bool isPayment = false)
        {
            // Width 520 to fit 2 in a row (520*2 = 1040 + margin < 1100)
            AntdUI.Panel widget = new AntdUI.Panel { Width = 520, Height = 400, BackColor = Color.White, Radius = 12, Shadow = 4, Margin = new Padding(0, 0, 16, 0) };
            
            // Header
            AntdUI.Label lbl = new AntdUI.Label { Text = title, Font = new Font("Manrope", 12, FontStyle.Bold), Location = new Point(24, 24), AutoSize = true, ForeColor = ColorTranslator.FromHtml("#0f172a") };
            AntdUI.Button btnView = new AntdUI.Button { Text = isPayment ? "View History" : "View All", ForeColor = ColorTranslator.FromHtml("#137fec"), Location = new Point(460, 20), Type = TTypeMini.Default, BorderWidth = 0, BackColor = Color.Transparent, Font = new Font("Manrope", 9, FontStyle.Bold) };
            
            // Divider
            System.Windows.Forms.Panel div = new System.Windows.Forms.Panel { Size = new Size(520, 1), BackColor = ColorTranslator.FromHtml("#f1f5f9"), Location = new Point(0, 60) };
            
            widget.Controls.Add(div);
            // ...
            widget.Controls.Add(btnView);
            widget.Controls.Add(lbl);

            // Mock List Items
            FlowLayoutPanel listFlow = new FlowLayoutPanel { Location = new Point(0, 61), Size = new Size(520, 330), FlowDirection = FlowDirection.TopDown };
            
            if (isPayment) {
                 listFlow.Controls.Add(CreatePaymentItem("Unit 101 - Rent", "Oct 24, 2023", "$1,200", true));
                 listFlow.Controls.Add(CreatePaymentItem("Unit 305 - Deposit", "Oct 24, 2023", "$1,450", false));
                 listFlow.Controls.Add(CreatePaymentItem("Unit 204 - Rent", "Oct 23, 2023", "$1,150", true));
            } else {
                 listFlow.Controls.Add(CreateRequestItem("Leak in Unit 402", "Urgent", "#be123c", "#ffe4e6", "John Doe")); // rose
                 listFlow.Controls.Add(CreateRequestItem("Key replacement", "Low Priority", "#475569", "#f1f5f9", "Sarah Smith")); // slate
                 listFlow.Controls.Add(CreateRequestItem("Broken window lock", "Medium", "#b45309", "#fef3c7", "Mike Ross")); // amber
            }

            widget.Controls.Add(listFlow);

            return widget;
        }

        private Control CreateRequestItem(string title, string badge, string badgeFg, string badgeBg, string name)
        {
            System.Windows.Forms.Panel p = new System.Windows.Forms.Panel { Size = new Size(520, 80) };
            // Avatar
            AntdUI.Avatar av = new AntdUI.Avatar { Text = name.Substring(0,1), Size = new Size(36, 36), BackColor = Color.LightGray, Location = new Point(24, 22) };
            
            // Text
            AntdUI.Label t = new AntdUI.Label { Text = title, Font = new Font("Manrope", 10, FontStyle.Bold), Location = new Point(72, 20), AutoSize = true };
            AntdUI.Label n = new AntdUI.Label { Text = name + " • 2 hrs ago", Font = new Font("Manrope", 8), ForeColor = Color.Gray, Location = new Point(72, 44), AutoSize = true };
            
            // Badge
            AntdUI.Button b = new AntdUI.Button { Text = badge, ForeColor = ColorTranslator.FromHtml(badgeFg), BackColor = ColorTranslator.FromHtml(badgeBg), Location = new Point(250, 20), Size = new Size(90, 24), Radius = 4, Type = TTypeMini.Default, BorderWidth = 0, Font = new Font("Manrope", 8, FontStyle.Bold) };
            
            // Arrow
            AntdUI.Button arr = new AntdUI.Button { IconSvg = "arrow-right", BackColor = ColorTranslator.FromHtml("#eff6ff"), ForeColor = ColorTranslator.FromHtml("#137fec"), Size = new Size(32, 32), Radius = 8, Location = new Point(470, 24), Type = TTypeMini.Default, BorderWidth = 0 };
            // ... (rest of CreateRequestItem)

            p.Controls.Add(arr);
            p.Controls.Add(b);
            p.Controls.Add(n);
            p.Controls.Add(t);
            p.Controls.Add(av);
            return p;
        }

        private Control CreatePaymentItem(string title, string date, string amount, bool success)
        {
            System.Windows.Forms.Panel p = new System.Windows.Forms.Panel { Size = new Size(520, 80) };
            
            // Icon
            AntdUI.Button icon = new AntdUI.Button { IconSvg = success ? "dollar" : "bank", BackColor = success ? ColorTranslator.FromHtml("#ecfdf5") : ColorTranslator.FromHtml("#eff6ff"), ForeColor = success ? ColorTranslator.FromHtml("#10b981") : ColorTranslator.FromHtml("#137fec"), Size = new Size(40, 40), Radius = 20, Location = new Point(24, 20), Type = TTypeMini.Default, BorderWidth = 0 };
            
            // Text
            AntdUI.Label t = new AntdUI.Label { Text = title, Font = new Font("Manrope", 10, FontStyle.Bold), Location = new Point(80, 20), AutoSize = true };
            AntdUI.Label d = new AntdUI.Label { Text = date, Font = new Font("Manrope", 8), ForeColor = Color.Gray, Location = new Point(80, 44), AutoSize = true };
            
            // Amount
            AntdUI.Label amt = new AntdUI.Label { Text = amount, Font = new Font("Manrope", 12, FontStyle.Bold), Location = new Point(400, 20), AutoSize = true, ForeColor = ColorTranslator.FromHtml("#0f172a") };
             AntdUI.Label sts = new AntdUI.Label { Text = success ? "● Success" : "● Processing", Font = new Font("Manrope", 8, FontStyle.Bold), ForeColor = success ? ColorTranslator.FromHtml("#10b981") : ColorTranslator.FromHtml("#137fec"), Location = new Point(400, 44), AutoSize = true };

            p.Controls.Add(sts);
            p.Controls.Add(amt);
            p.Controls.Add(d);
            p.Controls.Add(t);
            p.Controls.Add(icon);
            return p;
        }
    }
}
