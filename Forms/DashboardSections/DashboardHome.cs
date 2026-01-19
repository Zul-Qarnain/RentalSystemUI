using System;
using System.Drawing;
using System.Windows.Forms;
using AntdUI;
using RentalSystemUI.Services;

namespace RentalSystemUI.Forms.DashboardSections
{
    public partial class DashboardHome : Form
    {
        private HomeownerDashboard _parent;
        private readonly LandlordService _landlordService = new LandlordService();

        public DashboardHome(HomeownerDashboard parent)
        {
            _parent = parent;
            
            // Designer Setup
            InitializeComponent();
            
            // Dynamic Data Loading
            LoadStats();
            LoadSplitContent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _parent.NavigateToProperties();
        }

        private void LoadStats()
        {
            // Get landlord ID from current session
            int landlordId = AppSession.CurrentUser?.UserID ?? 0;
            
            // Fetch real stats from database
            var stats = _landlordService.GetComprehensiveStats(landlordId);
            
            // Format values for display
            string earningsDisplay = $"৳{stats.TotalEarnings:N0}";
            string propsDisplay = stats.TotalProps.ToString();
            string requestsDisplay = stats.PendingReqs.ToString();
            string occupancyDisplay = $"{stats.OccupancyPercent}%";
            
            // Create stat cards with real data
            statsFlow.Controls.Add(CreateStatCard("Total Earnings", earningsDisplay, "All Time", Styles.Blue, "dollar"));
            statsFlow.Controls.Add(CreateStatCard("Properties", propsDisplay, "Active", Styles.OrangeBg, "home"));
            statsFlow.Controls.Add(CreateStatCard("Bookings", requestsDisplay, stats.PendingReqs == 1 ? "Pending" : "Pending", Styles.RedBg, "message"));
            statsFlow.Controls.Add(CreateStatCard("Occupancy", occupancyDisplay, $"{stats.ApprovedTenants} Tenants", Styles.GreenBg, "user"));
        }

        private PropertyService _propertyService = new PropertyService();

        private void LoadSplitContent()
        {
            int landlordId = AppSession.CurrentUser?.UserID ?? 0;

            // Left: Recent Properties
            AntdUI.Panel panelProps = CreateSectionPanel("Recent Properties");
            var props = _propertyService.GetPropertiesByLandlord(landlordId);
            
            if (props.Count == 0)
            {
                AntdUI.Label empty = new AntdUI.Label { Text = "No properties added yet.", ForeColor = Styles.TextGray, Location = new Point(20, 60), AutoSize = true };
                panelProps.Controls.Add(empty);
            }
            else
            {
                int y = 60;
                foreach(var p in props) // Show all, effectively scrolling
                {
                    if (y > 300) break; // Limit to a few for dashboard
                    AntdUI.Label lbl = new AntdUI.Label 
                    { 
                        Text = $"• {p.Title} (৳{p.RentAmount:N0}) - {p.Status}", 
                        ForeColor = Styles.TextGray, 
                        Location = new Point(20, y), 
                        AutoSize = true,
                        Font = Styles.Normal
                    };
                    panelProps.Controls.Add(lbl);
                    y += 30;
                }
                
                if (props.Count > 8)
                {
                     AntdUI.Label more = new AntdUI.Label { Text = $"...and {props.Count - 8} more", ForeColor = Styles.Blue, Location = new Point(20, y), AutoSize = true };
                     panelProps.Controls.Add(more);
                }
            }
            
            splitLayout.Controls.Add(panelProps, 0, 0);


            // Right: Recent Bookings
            AntdUI.Panel panelReqs = CreateSectionPanel("Recent Bookings");
            var bookings = _landlordService.GetBookings(landlordId);
            
            if (bookings.Count == 0)
            {
                AntdUI.Label empty = new AntdUI.Label { Text = "No bookings yet.", ForeColor = Styles.TextGray, Location = new Point(20, 60), AutoSize = true };
                panelReqs.Controls.Add(empty);
            }
            else
            {
                int y = 60;
                foreach(var b in bookings)
                {
                    if (y > 300) break;
                    AntdUI.Label lbl = new AntdUI.Label 
                    { 
                        Text = $"• {b.TenantName} requested '{b.PropertyTitle}' - {b.Status}", 
                        ForeColor = (b.Status == "Pending" ? Styles.OrangeTxt : Styles.TextGray), 
                        Location = new Point(20, y), 
                        AutoSize = true,
                        Font = Styles.Normal
                    };
                    panelReqs.Controls.Add(lbl);
                    y += 30;
                }
            }
            
            splitLayout.Controls.Add(panelReqs, 1, 0);
        }
        private AntdUI.Panel CreateStatCard(string title, string value, string badge, Color iconBg, string icon)
        {
            AntdUI.Panel card = new AntdUI.Panel
            {
                Size = new Size(260, 110),
                BackColor = Color.White,
                Radius = 15,
                Shadow = 5,
                Margin = new Padding(0, 0, 20, 0)
            };

            AntdUI.Button ico = new AntdUI.Button
            {
                IconSvg = icon,
                Location = new Point(15, 25),
                Size = new Size(50, 50),
                Radius = 25,
                BackColor = iconBg, 
                ForeColor = Styles.Blue, 
                Type = TTypeMini.Default,
                BorderWidth = 0
            };

            AntdUI.Label lblTitle = new AntdUI.Label { Text = title, ForeColor = Styles.TextGray, Location = new Point(80, 30), AutoSize = true, Font = Styles.Small };
            AntdUI.Label lblValue = new AntdUI.Label { Text = value, ForeColor = Styles.DarkBlue, Location = new Point(80, 50), AutoSize = true, Font = Styles.Header };
            
            card.Controls.Add(lblValue);
            card.Controls.Add(lblTitle);
            card.Controls.Add(ico);

            return card;
        }

        private AntdUI.Panel CreateSectionPanel(string title)
        {
            AntdUI.Panel p = new AntdUI.Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Radius = 15,
                Shadow = 6,
                Margin = new Padding(10)
            };
            
            AntdUI.Label lbl = new AntdUI.Label { Text = title, Font = Styles.CardTitle, ForeColor = Styles.DarkBlue, Location = new Point(20, 20), AutoSize = true };
            p.Controls.Add(lbl);

            // Placeholder content
            AntdUI.Label place = new AntdUI.Label { Text = "Loading data...", ForeColor = Styles.TextGray, Location = new Point(20, 60), AutoSize = true };
            p.Controls.Add(place);

            return p;
        }
    }
}
