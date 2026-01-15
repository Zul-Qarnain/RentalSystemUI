using System;
using System.Drawing;
using System.Windows.Forms;
using AntdUI;

namespace RentalSystemUI.Forms.DashboardSections
{
    public class Settings : UserControl
    {
        public Settings()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;
            InitializeUI();
        }

        private void InitializeUI()
        {
            AntdUI.Label lblTitle = new AntdUI.Label { Text = "Account Settings", Font = new Font("Segoe UI", 16, FontStyle.Bold), Dock = DockStyle.Top, Height = 60, Padding = new Padding(20, 15, 0, 0) };
            this.Controls.Add(lblTitle);

            AntdUI.Panel panel = new AntdUI.Panel { Location = new Point(20, 80), Size = new Size(500, 400), Radius = 8, Shadow = 5 };
            
            // Profile Pic Placeholder
            AntdUI.Avatar avatar = new AntdUI.Avatar { Text = "User", Location = new Point(210, 20), Width = 80, Height = 80 };
            panel.Controls.Add(avatar);

            int y = 120;
            panel.Controls.Add(CreateField("Full Name", "Shihab Mahamud", y));
            y += 70;
            panel.Controls.Add(CreateField("Email Address", "landlord@example.com", y));
            y += 70;
            panel.Controls.Add(CreateField("Password", "********", y, true));

            AntdUI.Button btnSave = new AntdUI.Button { Text = "Save Changes", Type = TTypeMini.Primary, Location = new Point(20, 340), Size = new Size(460, 40) };
            btnSave.Click += (s, e) => {
                 if (this.FindForm() is Form f) AntdUI.Message.success(f, "Settings Updated Successfully!");
            };
            panel.Controls.Add(btnSave);

            this.Controls.Add(panel);
        }

        private Control CreateField(string label, string value, int y, bool isPass = false)
        {
            System.Windows.Forms.Panel p = new System.Windows.Forms.Panel { Location = new Point(20, y), Size = new Size(460, 60) };
            p.Controls.Add(new AntdUI.Label { Text = label, Location = new Point(0, 0), AutoSize = true, ForeColor = Color.Gray });
            
            AntdUI.Input input = new AntdUI.Input { Text = value, Location = new Point(0, 25), Size = new Size(460, 35) };
            if (isPass) input.UseSystemPasswordChar = true;
            
            p.Controls.Add(input);
            return p;
        }
    }
}
