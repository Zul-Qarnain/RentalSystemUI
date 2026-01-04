namespace RentalSystemUI.Forms
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSub = new System.Windows.Forms.Label();
            this.lblBlue = new System.Windows.Forms.Label();
            this.lblMain = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnMinimize = new AntdUI.Button();
            this.btnClose = new AntdUI.Button();
            this.pnlSignup = new System.Windows.Forms.Panel();
            this.btnSignupGoogle = new AntdUI.Button();
            this.btnSignupFB = new AntdUI.Button();
            this.lblSignupOr = new System.Windows.Forms.Label();
            this.btnSignup = new AntdUI.Button();
            this.chkAgree = new AntdUI.Checkbox();
            this.txtSignupPass = new AntdUI.Input();
            this.txtSignupEmail = new AntdUI.Input();
            this.txtSignupPhone = new AntdUI.Input();
            this.txtSignupName = new AntdUI.Input();
            this.panelRoles = new System.Windows.Forms.Panel();
            this.btnRoleTenant = new AntdUI.Button();
            this.btnRoleLandlord = new AntdUI.Button();
            this.lblLoginLink = new System.Windows.Forms.Label();
            this.lblSignupTitle = new System.Windows.Forms.Label();
            this.pnlLogin = new System.Windows.Forms.Panel();
            this.lblForgot = new System.Windows.Forms.Label();
            this.btnFacebook = new AntdUI.Button();
            this.btnGoogle = new AntdUI.Button();
            this.labelOr = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSignIn = new AntdUI.Button();
            this.checkbox1 = new AntdUI.Checkbox();
            this.txtLoginPass = new AntdUI.Input();
            this.txtLoginEmail = new AntdUI.Input();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.pnlSignup.SuspendLayout();
            this.panelRoles.SuspendLayout();
            this.pnlLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.panel1.Controls.Add(this.lblSub);
            this.panel1.Controls.Add(this.lblBlue);
            this.panel1.Controls.Add(this.lblMain);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(450, 650);
            this.panel1.TabIndex = 0;
            // 
            // lblSub
            // 
            this.lblSub.AutoSize = true;
            this.lblSub.BackColor = System.Drawing.Color.Transparent;
            this.lblSub.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSub.ForeColor = System.Drawing.Color.LightGray;
            this.lblSub.Location = new System.Drawing.Point(36, 330);
            this.lblSub.Name = "lblSub";
            this.lblSub.Size = new System.Drawing.Size(330, 56);
            this.lblSub.TabIndex = 0;
            this.lblSub.Text = "The all-in-one solution for landlords,\ntenants, and property managers.";
            // 
            // lblBlue
            // 
            this.lblBlue.AutoSize = true;
            this.lblBlue.BackColor = System.Drawing.Color.Transparent;
            this.lblBlue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblBlue.ForeColor = System.Drawing.Color.FromArgb(59, 130, 246);
            this.lblBlue.Location = new System.Drawing.Point(36, 265);
            this.lblBlue.Name = "lblBlue";
            this.lblBlue.Size = new System.Drawing.Size(212, 54);
            this.lblBlue.TabIndex = 1;
            this.lblBlue.Text = "with ease.";
            // 
            // lblMain
            // 
            this.lblMain.AutoSize = true;
            this.lblMain.BackColor = System.Drawing.Color.Transparent;
            this.lblMain.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblMain.ForeColor = System.Drawing.Color.White;
            this.lblMain.Location = new System.Drawing.Point(36, 220);
            this.lblMain.Name = "lblMain";
            this.lblMain.Size = new System.Drawing.Size(383, 54);
            this.lblMain.TabIndex = 2;
            this.lblMain.Text = "Manage properties";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(450, 650);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.btnMinimize);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.pnlSignup);
            this.panel2.Controls.Add(this.pnlLogin);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(450, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(550, 650);
            this.panel2.TabIndex = 0;
            this.panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            this.panel2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form_MouseMove);
            this.panel2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form_MouseUp);
            // 
            // btnMinimize
            // 
            this.btnMinimize.Location = new System.Drawing.Point(450, 0);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(45, 37);
            this.btnMinimize.TabIndex = 0;
            this.btnMinimize.Text = "--";
            this.btnMinimize.Type = AntdUI.TTypeMini.Success;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(500, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(45, 37);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "✕";
            this.btnClose.Type = AntdUI.TTypeMini.Error;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pnlSignup
            // 
            this.pnlSignup.BackColor = System.Drawing.Color.White;
            this.pnlSignup.Controls.Add(this.btnSignupGoogle);
            this.pnlSignup.Controls.Add(this.btnSignupFB);
            this.pnlSignup.Controls.Add(this.lblSignupOr);
            this.pnlSignup.Controls.Add(this.btnSignup);
            this.pnlSignup.Controls.Add(this.chkAgree);
            this.pnlSignup.Controls.Add(this.txtSignupPass);
            this.pnlSignup.Controls.Add(this.txtSignupEmail);
            this.pnlSignup.Controls.Add(this.txtSignupPhone);
            this.pnlSignup.Controls.Add(this.txtSignupName);
            this.pnlSignup.Controls.Add(this.panelRoles);
            this.pnlSignup.Controls.Add(this.lblLoginLink);
            this.pnlSignup.Controls.Add(this.lblSignupTitle);
            this.pnlSignup.Location = new System.Drawing.Point(65, 40);
            this.pnlSignup.Name = "pnlSignup";
            this.pnlSignup.Size = new System.Drawing.Size(420, 580);
            this.pnlSignup.TabIndex = 2;
            this.pnlSignup.Visible = false;
            // 
            // btnSignupGoogle
            // 
            this.btnSignupGoogle.BackColor = System.Drawing.Color.DarkCyan;
            this.btnSignupGoogle.Location = new System.Drawing.Point(0, 470);
            this.btnSignupGoogle.Name = "btnSignupGoogle";
            this.btnSignupGoogle.Size = new System.Drawing.Size(205, 40);
            this.btnSignupGoogle.TabIndex = 0;
            this.btnSignupGoogle.Text = "Google";
            this.btnSignupGoogle.Type = AntdUI.TTypeMini.Default;
            this.btnSignupGoogle.Click += new System.EventHandler(this.btnGoogle_Click);
            // 
            // btnSignupFB
            // 
            this.btnSignupFB.Location = new System.Drawing.Point(215, 470);
            this.btnSignupFB.Name = "btnSignupFB";
            this.btnSignupFB.Size = new System.Drawing.Size(205, 40);
            this.btnSignupFB.TabIndex = 1;
            this.btnSignupFB.Text = "Facebook";
            this.btnSignupFB.Type = AntdUI.TTypeMini.Default;
            this.btnSignupFB.Click += new System.EventHandler(this.btnSignupFB_Click);
            // 
            // lblSignupOr
            // 
            this.lblSignupOr.AutoSize = true;
            this.lblSignupOr.ForeColor = System.Drawing.Color.Gray;
            this.lblSignupOr.Location = new System.Drawing.Point(155, 440);
            this.lblSignupOr.Name = "lblSignupOr";
            this.lblSignupOr.Size = new System.Drawing.Size(143, 25);
            this.lblSignupOr.TabIndex = 2;
            this.lblSignupOr.Text = "Or continue with";
            // 
            // btnSignup
            // 
            this.btnSignup.Location = new System.Drawing.Point(0, 380);
            this.btnSignup.Name = "btnSignup";
            this.btnSignup.Radius = 8;
            this.btnSignup.Size = new System.Drawing.Size(420, 45);
            this.btnSignup.TabIndex = 3;
            this.btnSignup.Text = "Create Account";
            this.btnSignup.Type = AntdUI.TTypeMini.Primary;
            this.btnSignup.Click += new System.EventHandler(this.BtnSignup_Click);
            // 
            // chkAgree
            // 
            this.chkAgree.Location = new System.Drawing.Point(5, 340);
            this.chkAgree.Name = "chkAgree";
            this.chkAgree.Size = new System.Drawing.Size(400, 30);
            this.chkAgree.TabIndex = 4;
            this.chkAgree.Text = "I agree to Terms of Service";
            // 
            // txtSignupPass
            // 
            this.txtSignupPass.Location = new System.Drawing.Point(0, 290);
            this.txtSignupPass.Name = "txtSignupPass";
            this.txtSignupPass.PlaceholderText = "Password";
            this.txtSignupPass.Radius = 8;
            this.txtSignupPass.Size = new System.Drawing.Size(420, 40);
            this.txtSignupPass.TabIndex = 5;
            this.txtSignupPass.UseSystemPasswordChar = true;
            // 
            // txtSignupEmail
            // 
            this.txtSignupEmail.Location = new System.Drawing.Point(0, 240);
            this.txtSignupEmail.Name = "txtSignupEmail";
            this.txtSignupEmail.PlaceholderText = "Email Address";
            this.txtSignupEmail.Radius = 8;
            this.txtSignupEmail.Size = new System.Drawing.Size(420, 40);
            this.txtSignupEmail.TabIndex = 6;
            // 
            // txtSignupPhone
            // 
            this.txtSignupPhone.Location = new System.Drawing.Point(0, 190);
            this.txtSignupPhone.Name = "txtSignupPhone";
            this.txtSignupPhone.PlaceholderText = "Phone Number";
            this.txtSignupPhone.Radius = 8;
            this.txtSignupPhone.Size = new System.Drawing.Size(420, 40);
            this.txtSignupPhone.TabIndex = 7;
            // 
            // txtSignupName
            // 
            this.txtSignupName.Location = new System.Drawing.Point(0, 140);
            this.txtSignupName.Name = "txtSignupName";
            this.txtSignupName.PlaceholderText = "Full Name";
            this.txtSignupName.Radius = 8;
            this.txtSignupName.Size = new System.Drawing.Size(420, 40);
            this.txtSignupName.TabIndex = 8;
            // 
            // panelRoles
            // 
            this.panelRoles.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.panelRoles.Controls.Add(this.btnRoleTenant);
            this.panelRoles.Controls.Add(this.btnRoleLandlord);
            this.panelRoles.Location = new System.Drawing.Point(0, 80);
            this.panelRoles.Name = "panelRoles";
            this.panelRoles.Size = new System.Drawing.Size(420, 45);
            this.panelRoles.TabIndex = 9;
            // 
            // btnRoleTenant
            // 
            this.btnRoleTenant.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnRoleTenant.Location = new System.Drawing.Point(0, 0);
            this.btnRoleTenant.Name = "btnRoleTenant";
            this.btnRoleTenant.Size = new System.Drawing.Size(210, 45);
            this.btnRoleTenant.TabIndex = 0;
            this.btnRoleTenant.Text = "Tenant";
            this.btnRoleTenant.Type = AntdUI.TTypeMini.Primary;
            this.btnRoleTenant.Click += new System.EventHandler(this.RoleButton_Click);
            // 
            // btnRoleLandlord
            // 
            this.btnRoleLandlord.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRoleLandlord.ForeColor = System.Drawing.Color.Gray;
            this.btnRoleLandlord.Location = new System.Drawing.Point(210, 0);
            this.btnRoleLandlord.Name = "btnRoleLandlord";
            this.btnRoleLandlord.Size = new System.Drawing.Size(210, 45);
            this.btnRoleLandlord.TabIndex = 1;
            this.btnRoleLandlord.Text = "Landlord";
            this.btnRoleLandlord.Click += new System.EventHandler(this.RoleButton_Click);
            // 
            // lblLoginLink
            // 
            this.lblLoginLink.AutoSize = true;
            this.lblLoginLink.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblLoginLink.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblLoginLink.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.lblLoginLink.Location = new System.Drawing.Point(172, 5);
            this.lblLoginLink.Name = "lblLoginLink";
            this.lblLoginLink.Size = new System.Drawing.Size(233, 25);
            this.lblLoginLink.TabIndex = 10;
            this.lblLoginLink.Text = "Already a member? Log in";
            this.lblLoginLink.Click += new System.EventHandler(this.SwitchToLogin);
            // 
            // lblSignupTitle
            // 
            this.lblSignupTitle.AutoSize = true;
            this.lblSignupTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblSignupTitle.Location = new System.Drawing.Point(0, 19);
            this.lblSignupTitle.Name = "lblSignupTitle";
            this.lblSignupTitle.Size = new System.Drawing.Size(276, 48);
            this.lblSignupTitle.TabIndex = 11;
            this.lblSignupTitle.Text = "Create Account";
            // 
            // pnlLogin
            // 
            this.pnlLogin.BackColor = System.Drawing.Color.White;
            this.pnlLogin.Controls.Add(this.lblForgot);
            this.pnlLogin.Controls.Add(this.btnFacebook);
            this.pnlLogin.Controls.Add(this.btnGoogle);
            this.pnlLogin.Controls.Add(this.labelOr);
            this.pnlLogin.Controls.Add(this.label6);
            this.pnlLogin.Controls.Add(this.btnSignIn);
            this.pnlLogin.Controls.Add(this.checkbox1);
            this.pnlLogin.Controls.Add(this.txtLoginPass);
            this.pnlLogin.Controls.Add(this.txtLoginEmail);
            this.pnlLogin.Controls.Add(this.label4);
            this.pnlLogin.Location = new System.Drawing.Point(65, 40);
            this.pnlLogin.Name = "pnlLogin";
            this.pnlLogin.Size = new System.Drawing.Size(420, 520);
            this.pnlLogin.TabIndex = 3;
            // 
            // lblForgot
            // 
            this.lblForgot.AutoSize = true;
            this.lblForgot.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblForgot.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblForgot.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lblForgot.Location = new System.Drawing.Point(245, 282);
            this.lblForgot.Name = "lblForgot";
            this.lblForgot.Size = new System.Drawing.Size(164, 25);
            this.lblForgot.TabIndex = 9;
            this.lblForgot.Text = "Forgot Password?";
            this.lblForgot.Click += new System.EventHandler(this.lblForgot_Click);
            // 
            // btnFacebook
            // 
            this.btnFacebook.Location = new System.Drawing.Point(215, 70);
            this.btnFacebook.Name = "btnFacebook";
            this.btnFacebook.Size = new System.Drawing.Size(205, 45);
            this.btnFacebook.TabIndex = 0;
            this.btnFacebook.Text = "Facebook";
            this.btnFacebook.Type = AntdUI.TTypeMini.Default;
            this.btnFacebook.Click += new System.EventHandler(this.btnSignupFB_Click);
            // 
            // btnGoogle
            // 
            this.btnGoogle.BackColor = System.Drawing.Color.DarkCyan;
            this.btnGoogle.Location = new System.Drawing.Point(0, 70);
            this.btnGoogle.Name = "btnGoogle";
            this.btnGoogle.Size = new System.Drawing.Size(205, 45);
            this.btnGoogle.TabIndex = 1;
            this.btnGoogle.Text = "Google";
            this.btnGoogle.Type = AntdUI.TTypeMini.Default;
            this.btnGoogle.Click += new System.EventHandler(this.btnGoogle_Click);
            // 
            // labelOr
            // 
            this.labelOr.AutoSize = true;
            this.labelOr.ForeColor = System.Drawing.Color.Gray;
            this.labelOr.Location = new System.Drawing.Point(155, 130);
            this.labelOr.Name = "labelOr";
            this.labelOr.Size = new System.Drawing.Size(143, 25);
            this.labelOr.TabIndex = 2;
            this.labelOr.Text = "Or continue with";
            // 
            // label6
            // 
            this.label6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label6.ForeColor = System.Drawing.Color.Gray;
            this.label6.Location = new System.Drawing.Point(0, 380);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(420, 27);
            this.label6.TabIndex = 3;
            this.label6.Text = "Don't have an account? Sign up";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label6.Click += new System.EventHandler(this.SwitchToSignup);
            // 
            // btnSignIn
            // 
            this.btnSignIn.Location = new System.Drawing.Point(0, 320);
            this.btnSignIn.Name = "btnSignIn";
            this.btnSignIn.Radius = 8;
            this.btnSignIn.Size = new System.Drawing.Size(420, 50);
            this.btnSignIn.TabIndex = 4;
            this.btnSignIn.Text = "Sign In";
            this.btnSignIn.Type = AntdUI.TTypeMini.Primary;
            this.btnSignIn.Click += new System.EventHandler(this.btnSignIn_Click);
            // 
            // checkbox1
            // 
            this.checkbox1.Location = new System.Drawing.Point(5, 275);
            this.checkbox1.Name = "checkbox1";
            this.checkbox1.Size = new System.Drawing.Size(192, 34);
            this.checkbox1.TabIndex = 5;
            this.checkbox1.Text = "Remember me";
            // 
            // txtLoginPass
            // 
            this.txtLoginPass.Location = new System.Drawing.Point(0, 220);
            this.txtLoginPass.Name = "txtLoginPass";
            this.txtLoginPass.PlaceholderText = "Password";
            this.txtLoginPass.Radius = 8;
            this.txtLoginPass.Size = new System.Drawing.Size(420, 45);
            this.txtLoginPass.TabIndex = 6;
            this.txtLoginPass.UseSystemPasswordChar = true;
            // 
            // txtLoginEmail
            // 
            this.txtLoginEmail.Location = new System.Drawing.Point(0, 160);
            this.txtLoginEmail.Name = "txtLoginEmail";
            this.txtLoginEmail.PlaceholderText = "Email Address";
            this.txtLoginEmail.Radius = 8;
            this.txtLoginEmail.Size = new System.Drawing.Size(420, 45);
            this.txtLoginEmail.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(297, 54);
            this.label4.TabIndex = 8;
            this.label4.Text = "Welcome Back";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 650);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.pnlSignup.ResumeLayout(false);
            this.pnlSignup.PerformLayout();
            this.panelRoles.ResumeLayout(false);
            this.pnlLogin.ResumeLayout(false);
            this.pnlLogin.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        // Controls
        private System.Windows.Forms.Panel panel1; private System.Windows.Forms.PictureBox pictureBox1; private System.Windows.Forms.Label lblMain; private System.Windows.Forms.Label lblBlue; private System.Windows.Forms.Label lblSub;
        private System.Windows.Forms.Panel panel2; private AntdUI.Button btnMinimize; private AntdUI.Button btnClose;

        // Login Controls
        private System.Windows.Forms.Panel pnlLogin; private System.Windows.Forms.Label label6; private AntdUI.Button btnSignIn; private AntdUI.Checkbox checkbox1; private AntdUI.Input txtLoginPass; private AntdUI.Input txtLoginEmail; private System.Windows.Forms.Label label4; private System.Windows.Forms.Label labelOr; private AntdUI.Button btnFacebook; private AntdUI.Button btnGoogle; private System.Windows.Forms.Label lblForgot;

        // Signup Controls (NOW IN DESIGNER!)
        private System.Windows.Forms.Panel pnlSignup; private System.Windows.Forms.Label lblSignupTitle; private System.Windows.Forms.Label lblLoginLink; private System.Windows.Forms.Panel panelRoles; private AntdUI.Button btnRoleTenant; private AntdUI.Button btnRoleLandlord; private AntdUI.Input txtSignupName; private AntdUI.Input txtSignupPhone; private AntdUI.Input txtSignupEmail; private AntdUI.Input txtSignupPass; private AntdUI.Checkbox chkAgree; private AntdUI.Button btnSignup; private System.Windows.Forms.Label lblSignupOr; private AntdUI.Button btnSignupGoogle; private AntdUI.Button btnSignupFB;
    }
}