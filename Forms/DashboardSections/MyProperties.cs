using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using AntdUI;
using RentalSystemUI.Services;

namespace RentalSystemUI.Forms.DashboardSections
{
    public partial class MyProperties : Form
    {
        private readonly int _landlordId;
        private readonly PropertyService _propertyService;
        private bool _isTenant; // New flag
        private HomeownerDashboard? _parent;

        public MyProperties(int landlordId = 1, HomeownerDashboard? parent = null, bool isTenant = false)
        {
            _landlordId = landlordId;
            _parent = parent;
            _isTenant = isTenant;
            _propertyService = new PropertyService();
            
            InitializeComponent();
            
            if (_isTenant)
            {
                // Hide Add Property button if it exists (assuming it is on the form)
                // Use Find or try/catch if not sure of the name, but usually it is btnAddProperty
                try { 
                    Control[] matches = this.Controls.Find("btnAddProperty", true);
                    if (matches.Length > 0) matches[0].Visible = false;
                } catch {}
            }

            // Re-attach event handlers that Designer can't easily inline if they were lambdas
            _propertiesFlow.Resize += (s, e) => RecalculatePadding();
            
            LoadProperties();
        }

        private void RecalculatePadding()
        {
            if (_propertiesFlow == null || _propertiesFlow.Controls.Count == 0) return;

            int cardWidth = 340 + 20; // Width + Margin
            int availableWidth = _propertiesFlow.ClientSize.Width;
            
            int columns = Math.Max(1, availableWidth / cardWidth);
            int totalContentWidth = columns * cardWidth;
            
            // Center the grid block
            int leftPadding = Math.Max(0, (availableWidth - totalContentWidth) / 2);
            
            if (_propertiesFlow.Padding.Left != leftPadding)
            {
                _propertiesFlow.Padding = new Padding(leftPadding, 20, 0, 20);
            }
        }

        private void LoadProperties()
        {
            if (_propertiesFlow == null) return;
            _propertiesFlow.Controls.Clear();

            List<PropertyModel> properties;
            if (_isTenant)
                properties = _propertyService.GetRentedProperties(_landlordId); // _landlordId is used as UserId
            else
                properties = _propertyService.GetPropertiesByLandlord(AppSession.CurrentUser?.UserID ?? _landlordId);

            if (properties.Count == 0)
            {
                AntdUI.Label lblEmpty = new AntdUI.Label
                {
                    Text = _isTenant ? "You have no accepted rentals." : "You have not added any properties yet.",
                    Font = Styles.SubHeader,
                    ForeColor = Styles.TextGray,
                    AutoSize = true,
                    Padding = new Padding(20)
                };
                _propertiesFlow.Controls.Add(lblEmpty);
                return;
            }

            foreach (var property in properties)
            {
                _propertiesFlow.Controls.Add(CreatePropertyCard(property));
            }
            
            RecalculatePadding();
        }

        private AntdUI.Panel CreatePropertyCard(PropertyModel property)
        {
            AntdUI.Panel card = new AntdUI.Panel 
            { 
                Width = 340, 
                Height = 340, 
                BackColor = Color.White, 
                Radius = 20, 
                Shadow = 10, 
                Margin = new Padding(0, 0, 20, 25) 
            };

            // Image Area
            PictureBox pic = new PictureBox 
            { 
                Size = new Size(340, 180), 
                Location = new Point(0, 0), 
                BackColor = Styles.LightBlue, 
                SizeMode = PictureBoxSizeMode.StretchImage 
            };

            if (!string.IsNullOrEmpty(property.FirstImagePath) && System.IO.File.Exists(property.FirstImagePath))
            {
                try { pic.Image = Image.FromFile(property.FirstImagePath); } catch { }
            }
            else
            {
                // Placeholder when no DB image exists
                try
                {
                    var placeholder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "placeholder.png");
                    if (System.IO.File.Exists(placeholder))
                    {
                        pic.Image = Image.FromFile(placeholder);
                    }
                }
                catch { }
            }

            // Status Badge
            AntdUI.Button badge = new AntdUI.Button 
            { 
                Text = property.Status, 
                Location = new Point(20, 20), 
                Size = new Size(90, 30), 
                Radius = 15,
                BorderWidth = 0,
                Font = Styles.Small,
                BackColor = Styles.White,
                ForeColor = Styles.DarkBlue
            };
            if(property.Status == "Available") { badge.ForeColor = Styles.GreenTxt; badge.BackColor = Styles.GreenBg; }
            else if (property.Status == "Rented") { badge.ForeColor = Styles.Blue; badge.BackColor = Styles.LightBlue; }
            else { badge.ForeColor = Styles.OrangeTxt; badge.BackColor = Styles.OrangeBg; }

            pic.Controls.Add(badge); 

            // Content Area
            int contentY = 190;

            AntdUI.Label lblTitle = new AntdUI.Label { Text = property.Title, Font = Styles.CardTitle, ForeColor = Styles.DarkBlue, Location = new Point(20, contentY), AutoSize = true };
            AntdUI.Label lblPrice = new AntdUI.Label { Text = $"${property.RentAmount:N0}", Font = Styles.Bold, ForeColor = Styles.Blue, Location = new Point(240, contentY + 2), AutoSize = true, Anchor = AnchorStyles.Top | AnchorStyles.Right };
            AntdUI.Label lblAddr = new AntdUI.Label { Text = $"{property.Address}", Font = Styles.Normal, ForeColor = Styles.TextGray, Location = new Point(20, contentY + 35), AutoSize = true };

            // Manage Button
            AntdUI.Button btnManage = new AntdUI.Button 
            { 
                Text = "View Property", 
                Type = TTypeMini.Default, 
                Ghost = true,
                ForeColor = Styles.Blue,
                BorderWidth = 1,
                Location = new Point(20, 285), 
                Size = new Size(300, 35),
                Radius = 8,
                Cursor = Cursors.Hand
            };
            btnManage.Click += (s, e) => OnManagePropertyClick(property.PropertyID);

            card.Controls.Add(btnManage);
            card.Controls.Add(lblAddr);
            card.Controls.Add(lblPrice);
            card.Controls.Add(lblTitle);
            card.Controls.Add(badge);
            card.Controls.Add(pic);

            badge.Location = new Point(20, 20);
            badge.BringToFront();

            return card;
        }

        private void OnAddPropertyClick(object? sender, EventArgs e)
        {
            using (var form = new RentalSystemUI.Forms.AddPropertyForm(_landlordId))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadProperties();
                }
            }
        }

        private void OnManagePropertyClick(int propertyId)
        {
            if (_isTenant)
            {
                // For Tenant: Open Details View (Read Only)
                // We need to find the parent dashboard or just open details
                // Since MyProperties is inside UserDashboard, we can try to find it
                // Or for simplicity, launch PropertyDetails form directly for now
                var details = new RentalSystemUI.Forms.PropertyDetails(propertyId);
                details.ShowDialog();
            }
            else
            {
                // For Landlord: Edit Mode
                using (var form = new RentalSystemUI.Forms.AddPropertyForm(_landlordId, propertyId))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadProperties();
                    }
                }
            }
        }
    }
}
