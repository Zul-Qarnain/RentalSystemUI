using System;
using System.Drawing;
using System.Windows.Forms;
using AntdUI;

namespace RentalSystemUI.Forms.DashboardSections
{
    public class MyProperties : UserControl
    {
        public MyProperties()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = ColorTranslator.FromHtml("#f6f7f8");
            InitializeUI();
        }

        private void InitializeUI()
        {
            // Header: Title + Add Button
            System.Windows.Forms.Panel header = new System.Windows.Forms.Panel { Dock = DockStyle.Top, Height = 60, Padding = new Padding(0, 0, 20, 0) };
            
            AntdUI.Label lblTitle = new AntdUI.Label 
            { 
                Text = "My Properties", 
                Font = new Font("Segoe UI", 16, FontStyle.Bold), 
                ForeColor = ColorTranslator.FromHtml("#0f172a"), 
                Location = new Point(0, 10), 
                AutoSize = true 
            };
            
            AntdUI.Button btnAdd = new AntdUI.Button 
            { 
                Text = "Add Property", 
                IconSvg = "plus", 
                Type = TTypeMini.Primary, 
                Back = ColorTranslator.FromHtml("#1677ff"), 
                Location = new Point(1000, 10), // Approximate right alignment, ideally used Anchor or flow
                Size = new Size(140, 40), 
                Radius = 8 
            };
            // btnAdd.Click += ... (Logic to open add form)

            header.Controls.Add(btnAdd);
            header.Controls.Add(lblTitle);
            this.Controls.Add(header);

            // Grid/List of Properties
            FlowLayoutPanel flow = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoScroll = true, Padding = new Padding(0, 10, 0, 0) };
            
            // Sample Data
            flow.Controls.Add(CreatePropertyCard("Sunset Apartments, Unit 4B", "123 Kuril, Dhaka", "$1,265/mo", "Available", "properties.png"));
            flow.Controls.Add(CreatePropertyCard("Lakeside Cabin", "Gulshan 2, Dhaka", "$2,500/mo", "Rented", "home.png"));
            flow.Controls.Add(CreatePropertyCard("Urban Loft 204", "Banani, Dhaka", "$1,800/mo", "Maintenance", "dashboard.png"));

            this.Controls.Add(flow);
        }

        private AntdUI.Panel CreatePropertyCard(string title, string address, string price, string status, string imgName)
        {
            AntdUI.Panel card = new AntdUI.Panel 
            { 
                Width = 350, 
                Height = 350, // Slightly taller styling
                BackColor = Color.White, 
                Radius = 12, 
                Shadow = 3, 
                Margin = new Padding(0, 0, 20, 20) 
            };
            
            // Image Placeholder (Top Half)
            PictureBox pic = new PictureBox 
            { 
                Size = new Size(350, 180), 
                Location = new Point(0, 0), 
                BackColor = Color.LightGray, 
                SizeMode = PictureBoxSizeMode.StretchImage 
            };
            // Try load asset
            string assetsPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets");
            string imgPath = System.IO.Path.Combine(assetsPath, imgName);
            if(System.IO.File.Exists(imgPath))
            {
                try { pic.Image = Image.FromFile(imgPath); } catch {}
            }
            
            // Info (Bottom Half)
            AntdUI.Label lblPrice = new AntdUI.Label { Text = price, Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = ColorTranslator.FromHtml("#1677ff"), Location = new Point(16, 190), AutoSize = true };
            AntdUI.Label lblTitle = new AntdUI.Label { Text = title, Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = ColorTranslator.FromHtml("#0f172a"), Location = new Point(16, 220), AutoSize = true };
            AntdUI.Label lblAddr = new AntdUI.Label { Text = address, Font = new Font("Segoe UI", 9), ForeColor = Color.Gray, Location = new Point(16, 245), AutoSize = true };

            AntdUI.Tag tagStatus = new AntdUI.Tag { Text = status, Location = new Point(16, 275) };
            switch(status)
            {
                case "Available": tagStatus.Type = TTypeMini.Success; break;
                case "Rented": tagStatus.Type = TTypeMini.Info; break;
                case "Maintenance": tagStatus.Type = TTypeMini.Warn; break;
            }

            // Actions
            AntdUI.Button btnEdit = new AntdUI.Button { Text = "Manage", Type = TTypeMini.Default, Location = new Point(240, 275), Size = new Size(90, 30) };

            card.Controls.Add(btnEdit);
            card.Controls.Add(tagStatus);
            card.Controls.Add(lblAddr);
            card.Controls.Add(lblTitle);
            card.Controls.Add(lblPrice);
            card.Controls.Add(pic);
            
            return card;
        }
    }
}
