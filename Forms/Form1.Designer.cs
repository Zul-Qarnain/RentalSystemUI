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
            panel1 = new Panel();
            lblSub = new Label();
            lblBlue = new Label();
            lblMain = new Label();
            pictureBox1 = new PictureBox();
            panel2 = new Panel();
            btnMinimize = new AntdUI.Button();
            btnClose = new AntdUI.Button();
            pnlSignup = new Panel();
            btnSignupGoogle = new AntdUI.Button();
            btnSignupFB = new AntdUI.Button();
            lblSignupOr = new Label();
            btnSignup = new AntdUI.Button();
            chkAgree = new AntdUI.Checkbox();
            txtSignupPass = new AntdUI.Input();
            txtSignupEmail = new AntdUI.Input();
            txtSignupPhone = new AntdUI.Input();
            txtSignupName = new AntdUI.Input();
            panelRoles = new Panel();
            btnRoleTenant = new AntdUI.Button();
            btnRoleLandlord = new AntdUI.Button();
            lblLoginLink = new Label();
            lblSignupTitle = new Label();
            pnlLogin = new Panel();
            btnFacebook = new AntdUI.Button();
            btnGoogle = new AntdUI.Button();
            labelOr = new Label();
            label6 = new Label();
            btnSignIn = new AntdUI.Button();
            checkbox1 = new AntdUI.Checkbox();
            txtLoginPass = new AntdUI.Input();
            txtLoginEmail = new AntdUI.Input();
            label4 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel2.SuspendLayout();
            pnlSignup.SuspendLayout();
            panelRoles.SuspendLayout();
            pnlLogin.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(17, 24, 39);
            panel1.Controls.Add(lblSub);
            panel1.Controls.Add(lblBlue);
            panel1.Controls.Add(lblMain);
            panel1.Controls.Add(pictureBox1);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(450, 650);
            panel1.TabIndex = 0;
            // 
            // lblSub
            // 
            lblSub.AutoSize = true;
            lblSub.BackColor = Color.Transparent;
            lblSub.Font = new Font("Segoe UI", 10F);
            lblSub.ForeColor = Color.LightGray;
            lblSub.Location = new Point(0, 354);
            lblSub.Name = "lblSub";
            lblSub.Size = new Size(330, 56);
            lblSub.TabIndex = 0;
            lblSub.Text = "The all-in-one solution for landlords,\ntenants, and property managers.";
            // 
            // lblBlue
            // 
            lblBlue.AutoSize = true;
            lblBlue.BackColor = Color.Transparent;
            lblBlue.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblBlue.ForeColor = Color.FromArgb(59, 130, 246);
            lblBlue.Location = new Point(3, 251);
            lblBlue.Name = "lblBlue";
            lblBlue.Size = new Size(212, 54);
            lblBlue.TabIndex = 1;
            lblBlue.Text = "with ease.";
            // 
            // lblMain
            // 
            lblMain.AutoSize = true;
            lblMain.BackColor = Color.Transparent;
            lblMain.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblMain.ForeColor = Color.White;
            lblMain.Location = new Point(0, 180);
            lblMain.Name = "lblMain";
            lblMain.Size = new Size(383, 54);
            lblMain.TabIndex = 2;
            lblMain.Text = "Manage properties";
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(450, 650);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(btnMinimize);
            panel2.Controls.Add(btnClose);
            panel2.Controls.Add(pnlSignup);
            panel2.Controls.Add(pnlLogin);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(450, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(550, 650);
            panel2.TabIndex = 0;
            // 
            // btnMinimize
            // 
            btnMinimize.Location = new Point(450, 0);
            btnMinimize.Name = "btnMinimize";
            btnMinimize.Size = new Size(45, 37);
            btnMinimize.TabIndex = 0;
            btnMinimize.Text = "--";
            btnMinimize.Type = AntdUI.TTypeMini.Success;
            btnMinimize.Click += btnMinimize_Click;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(500, 0);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(45, 37);
            btnClose.TabIndex = 1;
            btnClose.Text = "✕";
            btnClose.Type = AntdUI.TTypeMini.Error;
            btnClose.Click += btnClose_Click;
            // 
            // pnlSignup
            // 
            pnlSignup.BackColor = Color.White;
            pnlSignup.Controls.Add(btnSignupGoogle);
            pnlSignup.Controls.Add(btnSignupFB);
            pnlSignup.Controls.Add(lblSignupOr);
            pnlSignup.Controls.Add(btnSignup);
            pnlSignup.Controls.Add(chkAgree);
            pnlSignup.Controls.Add(txtSignupPass);
            pnlSignup.Controls.Add(txtSignupEmail);
            pnlSignup.Controls.Add(txtSignupPhone);
            pnlSignup.Controls.Add(txtSignupName);
            pnlSignup.Controls.Add(panelRoles);
            pnlSignup.Controls.Add(lblLoginLink);
            pnlSignup.Controls.Add(lblSignupTitle);
            pnlSignup.Location = new Point(65, 40);
            pnlSignup.Name = "pnlSignup";
            pnlSignup.Size = new Size(420, 580);
            pnlSignup.TabIndex = 2;
            pnlSignup.Visible = false;
            // 
            // btnSignupGoogle
            // 
            btnSignupGoogle.BackColor = Color.DarkCyan;
            btnSignupGoogle.Icon = (Image)resources.GetObject("btnSignupGoogle.Icon");
            btnSignupGoogle.Location = new Point(26, 470);
            btnSignupGoogle.Name = "btnSignupGoogle";
            btnSignupGoogle.Size = new Size(171, 40);
            btnSignupGoogle.TabIndex = 0;
            btnSignupGoogle.Text = "Google";
            btnSignupGoogle.Type = AntdUI.TTypeMini.Primary;
            // 
            // btnSignupFB
            // 
            btnSignupFB.Icon = (Image)resources.GetObject("btnSignupFB.Icon");
            btnSignupFB.Location = new Point(200, 468);
            btnSignupFB.Name = "btnSignupFB";
            btnSignupFB.Size = new Size(186, 40);
            btnSignupFB.TabIndex = 1;
            btnSignupFB.Text = "Facebook";
            btnSignupFB.Type = AntdUI.TTypeMini.Primary;
            btnSignupFB.Click += btnSignupFB_Click;
            // 
            // lblSignupOr
            // 
            lblSignupOr.AutoSize = true;
            lblSignupOr.ForeColor = Color.Gray;
            lblSignupOr.Location = new Point(155, 440);
            lblSignupOr.Name = "lblSignupOr";
            lblSignupOr.Size = new Size(143, 25);
            lblSignupOr.TabIndex = 2;
            lblSignupOr.Text = "Or continue with";
            // 
            // btnSignup
            // 
            btnSignup.Location = new Point(0, 380);
            btnSignup.Name = "btnSignup";
            btnSignup.Radius = 8;
            btnSignup.Size = new Size(420, 45);
            btnSignup.TabIndex = 3;
            btnSignup.Text = "Create Account";
            btnSignup.Type = AntdUI.TTypeMini.Primary;
            btnSignup.Click += BtnSignup_Click;
            // 
            // chkAgree
            // 
            chkAgree.Location = new Point(5, 340);
            chkAgree.Name = "chkAgree";
            chkAgree.Size = new Size(400, 30);
            chkAgree.TabIndex = 4;
            chkAgree.Text = "I agree to Terms of Service";
            // 
            // txtSignupPass
            // 
            txtSignupPass.Location = new Point(0, 290);
            txtSignupPass.Name = "txtSignupPass";
            txtSignupPass.PlaceholderText = "Password";
            txtSignupPass.Radius = 8;
            txtSignupPass.Size = new Size(420, 40);
            txtSignupPass.Suffix = (Image)resources.GetObject("txtSignupPass.Suffix");
            txtSignupPass.TabIndex = 5;
            txtSignupPass.UseSystemPasswordChar = true;
            // 
            // txtSignupEmail
            // 
            txtSignupEmail.Location = new Point(0, 240);
            txtSignupEmail.Name = "txtSignupEmail";
            txtSignupEmail.PlaceholderText = "Email Address";
            txtSignupEmail.Radius = 8;
            txtSignupEmail.Size = new Size(420, 40);
            txtSignupEmail.TabIndex = 6;
            // 
            // txtSignupPhone
            // 
            txtSignupPhone.Location = new Point(0, 190);
            txtSignupPhone.Name = "txtSignupPhone";
            txtSignupPhone.PlaceholderText = "Phone Number";
            txtSignupPhone.Radius = 8;
            txtSignupPhone.Size = new Size(420, 40);
            txtSignupPhone.TabIndex = 7;
            // 
            // txtSignupName
            // 
            txtSignupName.Location = new Point(0, 140);
            txtSignupName.Name = "txtSignupName";
            txtSignupName.PlaceholderText = "Full Name";
            txtSignupName.Radius = 8;
            txtSignupName.Size = new Size(420, 40);
            txtSignupName.TabIndex = 8;
            // 
            // panelRoles
            // 
            panelRoles.BackColor = Color.FromArgb(245, 245, 245);
            panelRoles.Controls.Add(btnRoleTenant);
            panelRoles.Controls.Add(btnRoleLandlord);
            panelRoles.Location = new Point(0, 80);
            panelRoles.Name = "panelRoles";
            panelRoles.Size = new Size(420, 45);
            panelRoles.TabIndex = 9;
            // 
            // btnRoleTenant
            // 
            btnRoleTenant.Dock = DockStyle.Left;
            btnRoleTenant.Location = new Point(0, 0);
            btnRoleTenant.Name = "btnRoleTenant";
            btnRoleTenant.Size = new Size(210, 45);
            btnRoleTenant.TabIndex = 0;
            btnRoleTenant.Text = "Tenant";
            btnRoleTenant.Type = AntdUI.TTypeMini.Primary;
            btnRoleTenant.Click += RoleButton_Click;
            // 
            // btnRoleLandlord
            // 
            btnRoleLandlord.Dock = DockStyle.Right;
            btnRoleLandlord.ForeColor = Color.Gray;
            btnRoleLandlord.Location = new Point(210, 0);
            btnRoleLandlord.Name = "btnRoleLandlord";
            btnRoleLandlord.Size = new Size(210, 45);
            btnRoleLandlord.TabIndex = 1;
            btnRoleLandlord.Text = "Landlord";
            btnRoleLandlord.Click += RoleButton_Click;
            // 
            // lblLoginLink
            // 
            lblLoginLink.AutoSize = true;
            lblLoginLink.Cursor = Cursors.Hand;
            lblLoginLink.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblLoginLink.ForeColor = Color.FromArgb(37, 99, 235);
            lblLoginLink.Location = new Point(172, 5);
            lblLoginLink.Name = "lblLoginLink";
            lblLoginLink.Size = new Size(233, 25);
            lblLoginLink.TabIndex = 10;
            lblLoginLink.Text = "Already a member? Log in";
            lblLoginLink.Click += SwitchToLogin;
            // 
            // lblSignupTitle
            // 
            lblSignupTitle.AutoSize = true;
            lblSignupTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblSignupTitle.Location = new Point(0, 19);
            lblSignupTitle.Name = "lblSignupTitle";
            lblSignupTitle.Size = new Size(276, 48);
            lblSignupTitle.TabIndex = 11;
            lblSignupTitle.Text = "Create Account";
            // 
            // pnlLogin
            // 
            pnlLogin.BackColor = Color.White;
            pnlLogin.Controls.Add(btnFacebook);
            pnlLogin.Controls.Add(btnGoogle);
            pnlLogin.Controls.Add(labelOr);
            pnlLogin.Controls.Add(label6);
            pnlLogin.Controls.Add(btnSignIn);
            pnlLogin.Controls.Add(checkbox1);
            pnlLogin.Controls.Add(txtLoginPass);
            pnlLogin.Controls.Add(txtLoginEmail);
            pnlLogin.Controls.Add(label4);
            pnlLogin.Location = new Point(65, 40);
            pnlLogin.Name = "pnlLogin";
            pnlLogin.Size = new Size(420, 520);
            pnlLogin.TabIndex = 3;
            // 
            // btnFacebook
            // 
            btnFacebook.Icon = (Image)resources.GetObject("btnFacebook.Icon");
            btnFacebook.Location = new Point(215, 70);
            btnFacebook.Name = "btnFacebook";
            btnFacebook.Size = new Size(205, 45);
            btnFacebook.TabIndex = 0;
            btnFacebook.Text = "Facebook";
            btnFacebook.Type = AntdUI.TTypeMini.Primary;
            // 
            // btnGoogle
            // 
            btnGoogle.BackColor = Color.DarkCyan;
            btnGoogle.BackgroundImageLayout = AntdUI.TFit.Contain;
            btnGoogle.Icon = (Image)resources.GetObject("btnGoogle.Icon");
            btnGoogle.Location = new Point(0, 70);
            btnGoogle.Name = "btnGoogle";
            btnGoogle.Size = new Size(205, 45);
            btnGoogle.TabIndex = 1;
            btnGoogle.Text = "Google";
            btnGoogle.Type = AntdUI.TTypeMini.Primary;
            // 
            // labelOr
            // 
            labelOr.AutoSize = true;
            labelOr.ForeColor = Color.Gray;
            labelOr.Location = new Point(155, 130);
            labelOr.Name = "labelOr";
            labelOr.Size = new Size(143, 25);
            labelOr.TabIndex = 2;
            labelOr.Text = "Or continue with";
            // 
            // label6
            // 
            label6.Cursor = Cursors.Hand;
            label6.ForeColor = Color.Gray;
            label6.Location = new Point(0, 380);
            label6.Name = "label6";
            label6.Size = new Size(420, 27);
            label6.TabIndex = 3;
            label6.Text = "Don't have an account? Sign up";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            label6.Click += SwitchToSignup;
            // 
            // btnSignIn
            // 
            btnSignIn.Location = new Point(0, 320);
            btnSignIn.Name = "btnSignIn";
            btnSignIn.Radius = 8;
            btnSignIn.Size = new Size(420, 50);
            btnSignIn.TabIndex = 4;
            btnSignIn.Text = "Sign In";
            btnSignIn.Type = AntdUI.TTypeMini.Primary;
            btnSignIn.Click += btnSignIn_Click;
            // 
            // checkbox1
            // 
            checkbox1.Location = new Point(5, 275);
            checkbox1.Name = "checkbox1";
            checkbox1.Size = new Size(192, 34);
            checkbox1.TabIndex = 5;
            checkbox1.Text = "Remember me";
            // 
            // txtLoginPass
            // 
            txtLoginPass.Location = new Point(0, 220);
            txtLoginPass.Name = "txtLoginPass";
            txtLoginPass.PlaceholderText = "Password";
            txtLoginPass.Radius = 8;
            txtLoginPass.Size = new Size(420, 45);
            txtLoginPass.Suffix = (Image)resources.GetObject("txtLoginPass.Suffix");
            txtLoginPass.TabIndex = 6;
            txtLoginPass.UseSystemPasswordChar = true;
            // 
            // txtLoginEmail
            // 
            txtLoginEmail.Location = new Point(0, 160);
            txtLoginEmail.Name = "txtLoginEmail";
            txtLoginEmail.PlaceholderText = "Email Address";
            txtLoginEmail.Radius = 8;
            txtLoginEmail.Size = new Size(420, 45);
            txtLoginEmail.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            label4.Location = new Point(0, 0);
            label4.Name = "label4";
            label4.Size = new Size(297, 54);
            label4.TabIndex = 8;
            label4.Text = "Welcome Back";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 650);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel2.ResumeLayout(false);
            pnlSignup.ResumeLayout(false);
            pnlSignup.PerformLayout();
            panelRoles.ResumeLayout(false);
            pnlLogin.ResumeLayout(false);
            pnlLogin.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        // Controls
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblMain;
        private System.Windows.Forms.Label lblBlue;
        private System.Windows.Forms.Label lblSub;
        private System.Windows.Forms.Panel panel2;
        private AntdUI.Button btnMinimize;
        private AntdUI.Button btnClose;

        // Login Controls
        private System.Windows.Forms.Panel pnlLogin;
        private System.Windows.Forms.Label label6;
        private AntdUI.Button btnSignIn;
        private AntdUI.Checkbox checkbox1;
        private AntdUI.Input txtLoginPass;
        private AntdUI.Input txtLoginEmail;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelOr;
        private AntdUI.Button btnFacebook;
        private AntdUI.Button btnGoogle;

        // Signup Controls (NOW IN DESIGNER!)
        private System.Windows.Forms.Panel pnlSignup;
        private System.Windows.Forms.Label lblSignupTitle;
        private System.Windows.Forms.Label lblLoginLink;
        private System.Windows.Forms.Panel panelRoles;
        private AntdUI.Button btnRoleTenant;
        private AntdUI.Button btnRoleLandlord;
        private AntdUI.Input txtSignupName;
        private AntdUI.Input txtSignupPhone;
        private AntdUI.Input txtSignupEmail;
        private AntdUI.Input txtSignupPass;
        private AntdUI.Checkbox chkAgree;
        private AntdUI.Button btnSignup;
        private System.Windows.Forms.Label lblSignupOr;
        private AntdUI.Button btnSignupGoogle;
        private AntdUI.Button btnSignupFB;
    }
}