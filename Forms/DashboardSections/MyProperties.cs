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
        private HomeownerDashboard? _parent;

        public MyProperties(int landlordId = 1, HomeownerDashboard? parent = null)
        {
            _landlordId = landlordId;
            _parent = parent;
            _propertyService = new PropertyService();
            
            InitializeComponent();
            
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

            List<PropertyModel> properties = _propertyService.GetPropertiesByLandlord(_landlordId);

            if (properties.Count == 0)
            {
                AntdUI.Label lblEmpty = new AntdUI.Label{ Text = "No properties found.", Font = Styles.SubHeader, ForeColor = Styles.TextGray, AutoSize = true, Padding = new Padding(20) };
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
                Text = "Manage Property", 
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
