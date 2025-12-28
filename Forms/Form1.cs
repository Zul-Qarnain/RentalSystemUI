using System;
using System.Drawing;
using System.Windows.Forms;
using AntdUI;

// IMPORTANT: This namespace must match your project structure.
// If your Designer file says "namespace RentalSystemUI", change this line to match it.
namespace RentalSystemUI.Forms
{
    public partial class Form1 : Form
    {
        // Logic Variables
        private string selectedRole = "Tenant";
        private bool dragging = false;
        private Point dragCursorPoint, dragFormPoint;

        public Form1()
        {
            InitializeComponent();

            // 1. SETUP VISUALS (Transparency & Blue Text)
            SetupVisuals();

            // 2. INITIAL STATE
            pnlLogin.Visible = true;
            pnlSignup.Visible = false;
        }

        private void SetupVisuals()
        {
            if (pictureBox1 != null)
            {
                // Fix Transparency for labels on the left
                lblMain.Parent = pictureBox1;
                lblBlue.Parent = pictureBox1;
                lblSub.Parent = pictureBox1;

                lblMain.BackColor = Color.Transparent;
                lblBlue.BackColor = Color.Transparent;
                lblSub.BackColor = Color.Transparent;

                // Adjust positions slightly to ensure they stay correct
                lblMain.Location = new Point(36, 220);
                lblBlue.Location = new Point(36, 265);
                lblSub.Location = new Point(36, 330);
            }
        }

        // ==========================================
        // NAVIGATION
        // ==========================================
        private void SwitchToSignup(object sender, EventArgs e)
        {
            pnlLogin.Visible = false;
            pnlSignup.Visible = true;
            pnlSignup.BringToFront();
        }

        private void SwitchToLogin(object sender, EventArgs e)
        {
            pnlSignup.Visible = false;
            pnlLogin.Visible = true;
            pnlLogin.BringToFront();
        }

        // ==========================================
        // MAIN BUTTON LOGIC
        // ==========================================
        private void btnSignIn_Click(object sender, EventArgs e)
        {
            AntdUI.Message.success(this, "Login Successful!");
        }

        private void BtnSignup_Click(object sender, EventArgs e)
        {
            if (!chkAgree.Checked)
            {
                AntdUI.Message.warn(this, "Please agree to the Terms.");
                return;
            }
            AntdUI.Message.success(this, $"Created {selectedRole} Account for {txtSignupName.Text}");
            SwitchToLogin(null, null);
        }

        private void RoleButton_Click(object sender, EventArgs e)
        {
            var btn = sender as AntdUI.Button;
            if (btn == null) return;

            if (btn == btnRoleTenant)
            {
                selectedRole = "Tenant";
                btnRoleTenant.Type = AntdUI.TTypeMini.Primary; // Blue
                btnRoleLandlord.Type = AntdUI.TTypeMini.Default; // Gray
                btnRoleLandlord.ForeColor = Color.Gray;
                btnRoleTenant.ForeColor = Color.White;
            }
            else
            {
                selectedRole = "Landlord";
                btnRoleLandlord.Type = AntdUI.TTypeMini.Primary; // Blue
                btnRoleTenant.Type = AntdUI.TTypeMini.Default; // Gray
                btnRoleTenant.ForeColor = Color.Gray;
                btnRoleLandlord.ForeColor = Color.White;
            }
        }

        // ==========================================
        // SOCIAL BUTTONS (Fixes the specific error)
        // ==========================================
        private void btnGoogle_Click(object sender, EventArgs e)
        {
            AntdUI.Message.info(this, "Google Login Clicked");
        }

        private void btnSignupFB_Click(object sender, EventArgs e)
        {
            AntdUI.Message.info(this, "Facebook Signup Clicked");
        }

        // ==========================================
        // WINDOW & DRAGGING
        // ==========================================
        private void btnClose_Click(object sender, EventArgs e) { Application.Exit(); }
        private void btnMinimize_Click(object sender, EventArgs e) { this.WindowState = FormWindowState.Minimized; }

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void Form_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        // ==========================================
        // DUMMY HANDLERS (Required by Designer)
        // ==========================================
        // These exist because the Designer has linked them. 
        // If you delete these, the Designer will crash.
        private void label4_Click(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { } // Often double-clicked by mistake
        private void pictureBox1_Click(object sender, EventArgs e) { }
    }
}