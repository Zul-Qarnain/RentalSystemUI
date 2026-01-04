using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using AntdUI;
using RentalSystemUI.Controllers;

namespace RentalSystemUI.Forms
{
    public partial class Form1 : Form
    {
        // Database & Helper
        private DatabaseHelper db = new DatabaseHelper();

        // UI State Variables
        private string selectedRole = "Tenant";
        private bool dragging = false;
        private Point dragCursorPoint, dragFormPoint;

        public Form1()
        {
            InitializeComponent();
            SetupVisuals();

            // Dragging Logic
            AttachDragEvents(this);
            if (panel1 != null) AttachDragEvents(panel1);
            if (panel2 != null) AttachDragEvents(panel2);
            if (pictureBox1 != null) AttachDragEvents(pictureBox1);

            // Initial State
            if (pnlLogin != null) pnlLogin.Visible = true;
            if (pnlSignup != null) pnlSignup.Visible = false;
        }

        private void SetupVisuals()
        {
            if (pictureBox1 != null)
            {
                // Fix Transparency
                if (lblMain != null) { lblMain.Parent = pictureBox1; lblMain.BackColor = Color.Transparent; }
                if (lblBlue != null) { lblBlue.Parent = pictureBox1; lblBlue.BackColor = Color.Transparent; }
                if (lblSub != null) { lblSub.Parent = pictureBox1; lblSub.BackColor = Color.Transparent; }

                // Adjust positions
                if (lblMain != null) lblMain.Location = new Point(36, 220);
                if (lblBlue != null) lblBlue.Location = new Point(36, 265);
                if (lblSub != null) lblSub.Location = new Point(36, 330);
            }
        }

        // ============================================
        //  LOGIN LOGIC (With App Close Fix)
        // ============================================
        private void btnSignIn_Click(object sender, EventArgs e)
        {
            string email = txtLoginEmail.Text.Trim();
            string pass = txtLoginPass.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pass))
            {
                AntdUI.Message.error(this, "Please fill in all fields.");
                return;
            }

            if (db.ValidateUser(email, pass, out string role, out string name))
            {
                AntdUI.Message.success(this, $"Welcome back, {name}!");

                // --- OPEN SEARCH & CLOSE LOGIN PROPERLY ---
                RentAllSearch searchForm = new RentAllSearch();
                searchForm.FormClosed += (s, args) => this.Close(); // Kills process when closed
                searchForm.Show();
                this.Hide();
            }
            else
            {
                AntdUI.Message.error(this, "Invalid Email or Password.");
            }
        }

        // ============================================
        //  FORGOT PASSWORD (Using VerifyForm)
        // ============================================
        private async void lblForgot_Click(object sender, EventArgs e)
        {
            // 1. Get Email using Custom Form
            string email = "";
            using (var form = new VerifyForm("Reset Password", "Enter your email"))
            {
                if (form.ShowDialog() != DialogResult.OK) return;
                email = form.InputValue;
            }

            if (!db.UserExists(email, ""))
            {
                AntdUI.Message.error(this, "Email not found.");
                return;
            }

            // 2. Send OTP
            string otp = new Random().Next(100000, 999999).ToString();
            AntdUI.Message.info(this, "Sending Verification Code...");

            bool sent = await EmailHelper.SendOtp(email, otp);
            if (!sent)
            {
                AntdUI.Message.error(this, "Failed to send email.");
                return;
            }

            // 3. Verify OTP (With Timer & Resend Support)
            using (var verifyForm = new VerifyForm("Verify Email", "Enter the 6-digit code", false, true))
            {
                // Handle Resend Logic
                verifyForm.ResendRequested += async (s, args) => {
                    otp = new Random().Next(100000, 999999).ToString();
                    await EmailHelper.SendOtp(email, otp);
                    AntdUI.Message.success(this, "New code sent!");
                };

                if (verifyForm.ShowDialog() != DialogResult.OK) return;

                if (verifyForm.InputValue.Trim() == otp)
                {
                    // 4. Get New Password
                    using (var passForm = new VerifyForm("New Password", "Enter new password", true, false))
                    {
                        if (passForm.ShowDialog() == DialogResult.OK)
                        {
                            db.UpdatePassword(email, passForm.InputValue);
                            AntdUI.Message.success(this, "Password Reset! Please Login.");
                        }
                    }
                }
                else
                {
                    AntdUI.Message.error(this, "Incorrect OTP.");
                }
            }
        }

        // ============================================
        //  SIGNUP LOGIC (Using VerifyForm)
        // ============================================
        private async void BtnSignup_Click(object sender, EventArgs e)
        {
            string name = txtSignupName.Text.Trim();
            string email = txtSignupEmail.Text.Trim();
            string phone = txtSignupPhone.Text.Trim();
            string pass = txtSignupPass.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pass) || string.IsNullOrEmpty(phone))
            {
                AntdUI.Message.error(this, "Fill all fields.");
                return;
            }
            if (!chkAgree.Checked)
            {
                AntdUI.Message.warn(this, "Agree to Terms.");
                return;
            }

            if (db.UserExists(email, phone))
            {
                AntdUI.Message.error(this, "Account already exists.");
                return;
            }

            // Send OTP
            string otp = new Random().Next(100000, 999999).ToString();
            AntdUI.Message.info(this, "Sending Verification Code...");

            bool sent = await EmailHelper.SendOtp(email, otp);
            if (!sent)
            {
                AntdUI.Message.error(this, "Could not send verification email.");
                return;
            }

            // Verify OTP using Custom Form
            using (var form = new VerifyForm("Verify Account", $"Enter code sent to {email}", false, true))
            {
                // Handle Resend Logic
                form.ResendRequested += async (s, args) => {
                    otp = new Random().Next(100000, 999999).ToString();
                    await EmailHelper.SendOtp(email, otp);
                    AntdUI.Message.success(this, "New code sent!");
                };

                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (form.InputValue.Trim() == otp)
                    {
                        // Register
                        if (db.RegisterUser(name, email, phone, pass, selectedRole))
                        {
                            AntdUI.Message.success(this, "Account Created! Login now.");
                            SwitchToLogin(null, null);
                        }
                        else
                        {
                            AntdUI.Message.error(this, "Database Error.");
                        }
                    }
                    else
                    {
                        AntdUI.Message.error(this, "Invalid Code.");
                    }
                }
            }
        }

        // --- Role Toggle ---
        private void RoleButton_Click(object sender, EventArgs e)
        {
            var btn = sender as AntdUI.Button;
            if (btn == null) return;

            if (btn == btnRoleTenant)
            {
                selectedRole = "Tenant";
                btnRoleTenant.Type = AntdUI.TTypeMini.Primary; btnRoleTenant.ForeColor = Color.White;
                btnRoleLandlord.Type = AntdUI.TTypeMini.Default; btnRoleLandlord.ForeColor = Color.Gray;
            }
            else
            {
                selectedRole = "Landlord";
                btnRoleLandlord.Type = AntdUI.TTypeMini.Primary; btnRoleLandlord.ForeColor = Color.White;
                btnRoleTenant.Type = AntdUI.TTypeMini.Default; btnRoleTenant.ForeColor = Color.Gray;
            }
        }

        // --- Navigation ---
        private void SwitchToSignup(object? sender, EventArgs? e)
        {
            if (pnlLogin != null) pnlLogin.Visible = false;
            if (pnlSignup != null) pnlSignup.Visible = true;
        }
        private void SwitchToLogin(object? sender, EventArgs? e)
        {
            if (pnlSignup != null) pnlSignup.Visible = false;
            if (pnlLogin != null) pnlLogin.Visible = true;
        }

        // --- Window & Dummy Events ---
        private void btnClose_Click(object sender, EventArgs e) { Application.Exit(); }
        private void btnMinimize_Click(object sender, EventArgs e) { this.WindowState = FormWindowState.Minimized; }
        private void btnGoogle_Click(object sender, EventArgs e) { AntdUI.Message.info(this, "Google Clicked"); }
        private void btnSignupFB_Click(object sender, EventArgs e) { AntdUI.Message.info(this, "Facebook Clicked"); }
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }

        // ============================================
        //  ATTACH DRAG EVENTS
        // ============================================
        private void AttachDragEvents(Control c)
        {
            c.MouseDown += (s, e) => { dragging = true; dragCursorPoint = Cursor.Position; dragFormPoint = this.Location; };
            c.MouseMove += (s, e) => { if (dragging) { Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint)); this.Location = Point.Add(dragFormPoint, new Size(dif)); } };
            c.MouseUp += (s, e) => dragging = false;
        }

        // Designer required methods
        private void Form_MouseDown(object sender, MouseEventArgs e) { dragging = true; dragCursorPoint = Cursor.Position; dragFormPoint = this.Location; }
        private void Form_MouseMove(object sender, MouseEventArgs e) { if (dragging) { Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint)); this.Location = Point.Add(dragFormPoint, new Size(dif)); } }
        private void Form_MouseUp(object sender, MouseEventArgs e) { dragging = false; }
        private void label6_Click(object sender, EventArgs e) { SwitchToSignup(null, null); }
    }
}