using System;
using System.Drawing;
using System.Windows.Forms;
using AntdUI;

namespace RentalSystemUI.Forms
{
    public partial class Form1 : Form
    {
        // State Variables
        private string selectedRole = "Tenant";
        private bool dragging = false;
        private Point dragCursorPoint, dragFormPoint;

        public Form1()
        {
            InitializeComponent();

            // 1. SETUP LEFT PANEL (Blue Text & Transparency)
            SetupLeftPanel();

            // 2. SETUP DRAGGING
            AttachDragEvents(this);
            AttachDragEvents(panel1);
            AttachDragEvents(panel2);
            if (pictureBox1 != null) AttachDragEvents(pictureBox1);

            // 3. ENSURE INITIAL STATE
            if (pnlLogin != null) pnlLogin.Visible = true;
            if (pnlSignup != null) pnlSignup.Visible = false;
        }

        // ============================================
        // 1. VISUAL SETUP
        // ============================================
        private void SetupLeftPanel()
        {
            if (pictureBox1 == null) return;

            // Fix Transparency by parenting labels to the PictureBox
            // This allows the image to show through the text background
            if (lblMain != null)
            {
                lblMain.Parent = pictureBox1;
                lblMain.BackColor = Color.Transparent;
                lblMain.Location = new Point(36, 220); // Force position
            }

            if (lblBlue != null)
            {
                lblBlue.Parent = pictureBox1;
                lblBlue.BackColor = Color.Transparent;
                lblBlue.ForeColor = Color.FromArgb(59, 130, 246); // Bright Blue
                lblBlue.Location = new Point(36, 265);
            }

            if (lblSub != null)
            {
                lblSub.Parent = pictureBox1;
                lblSub.BackColor = Color.Transparent;
                lblSub.Location = new Point(36, 330);
            }
        }

        // ============================================
        // 2. BUTTON CLICKS & LOGIC
        // ============================================

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            // Simple validation
            if (string.IsNullOrEmpty(txtLoginEmail.Text) || string.IsNullOrEmpty(txtLoginPass.Text))
            {
                AntdUI.Message.error(this, "Please fill in email and password.");
                return;
            }
            AntdUI.Message.success(this, "Login Successful!");
        }

        private void BtnSignup_Click(object sender, EventArgs e)
        {
            // Signup Validation
            if (string.IsNullOrEmpty(txtSignupName.Text) ||
                string.IsNullOrEmpty(txtSignupEmail.Text) ||
                string.IsNullOrEmpty(txtSignupPass.Text) ||
                string.IsNullOrEmpty(txtSignupPhone.Text))
            {
                AntdUI.Message.error(this, "Please fill in all fields.");
                return;
            }

            if (!chkAgree.Checked)
            {
                AntdUI.Message.warn(this, "You must agree to the Terms.");
                return;
            }

            AntdUI.Message.success(this, $"Created {selectedRole} Account for {txtSignupName.Text}!\nPhone: {txtSignupPhone.Text}");
            SwitchToLogin(this, EventArgs.Empty);
        }

        // Switch between Tenant and Landlord
        private void RoleButton_Click(object sender, EventArgs e)
        {
            var btn = sender as AntdUI.Button;
            if (btn == null) return;

            if (btn == btnRoleTenant)
            {
                selectedRole = "Tenant";
                // Make Tenant Blue
                btnRoleTenant.Type = AntdUI.TTypeMini.Primary;
                btnRoleTenant.ForeColor = Color.White;
                // Make Landlord Gray
                btnRoleLandlord.Type = AntdUI.TTypeMini.Default;
                btnRoleLandlord.ForeColor = Color.Gray;
            }
            else
            {
                selectedRole = "Landlord";
                // Make Landlord Blue
                btnRoleLandlord.Type = AntdUI.TTypeMini.Primary;
                btnRoleLandlord.ForeColor = Color.White;
                // Make Tenant Gray
                btnRoleTenant.Type = AntdUI.TTypeMini.Default;
                btnRoleTenant.ForeColor = Color.Gray;
            }
        }

        // ============================================
        // 3. NAVIGATION (SWITCH PANELS)
        // ============================================
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

        // ============================================
        // 4. WINDOW & DRAGGING
        // ============================================
        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void AttachDragEvents(Control c)
        {
            c.MouseDown += (s, e) => { dragging = true; dragCursorPoint = Cursor.Position; dragFormPoint = this.Location; };
            c.MouseMove += (s, e) => { if (dragging) { Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint)); this.Location = Point.Add(dragFormPoint, new Size(dif)); } };
            c.MouseUp += (s, e) => dragging = false;
        }

        // --- EMPTY EVENT HANDLERS (Required by Designer) ---
        // These exist so the designer doesn't crash if it looks for them.
        private void Form_MouseDown(object sender, MouseEventArgs e) { }
        private void Form_MouseMove(object sender, MouseEventArgs e) { }
        private void Form_MouseUp(object sender, MouseEventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }

        private void btnSignupFB_Click(object sender, EventArgs e)
        {

        }
    }
}