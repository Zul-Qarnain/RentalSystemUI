
namespace RentalSystemUI.Forms.DashboardSections
{
    partial class Settings
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitle = new AntdUI.Label();
            contentFlow = new System.Windows.Forms.FlowLayoutPanel();
            profileCard = new AntdUI.Panel();
            lblProfileTitle = new AntdUI.Label();
            lblFullName = new AntdUI.Label();
            _inputName = new AntdUI.Input();
            lblEmail = new AntdUI.Label();
            _inputEmail = new AntdUI.Input();
            lblPhone = new AntdUI.Label();
            _inputPhone = new AntdUI.Input();
            btnSave = new AntdUI.Button();
            passwordCard = new AntdUI.Panel();
            lblPassTitle = new AntdUI.Label();
            lblCurrentPass = new AntdUI.Label();
            _inputOldPassword = new AntdUI.Input();
            lblNewPass = new AntdUI.Label();
            _inputNewPassword = new AntdUI.Input();
            btnPass = new AntdUI.Button();
            contentFlow.SuspendLayout();
            profileCard.SuspendLayout();
            passwordCard.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)67)), ((int)((byte)24)), ((int)((byte)255)));
            lblTitle.Location = new System.Drawing.Point(0, 0);
            lblTitle.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            lblTitle.Name = "lblTitle";
            lblTitle.Padding = new System.Windows.Forms.Padding(29, 42, 0, 0);
            lblTitle.Size = new System.Drawing.Size(1714, 133);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Account Settings";
            // 
            // contentFlow
            // 
            contentFlow.AutoScroll = true;
            contentFlow.Controls.Add(profileCard);
            contentFlow.Controls.Add(passwordCard);
            contentFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            contentFlow.Location = new System.Drawing.Point(0, 133);
            contentFlow.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            contentFlow.Name = "contentFlow";
            contentFlow.Padding = new System.Windows.Forms.Padding(29, 33, 29, 33);
            contentFlow.Size = new System.Drawing.Size(1714, 1093);
            contentFlow.TabIndex = 1;
            // 
            // profileCard
            // 
            profileCard.BackColor = System.Drawing.Color.White;
            profileCard.Controls.Add(lblProfileTitle);
            profileCard.Controls.Add(lblFullName);
            profileCard.Controls.Add(_inputName);
            profileCard.Controls.Add(lblEmail);
            profileCard.Controls.Add(_inputEmail);
            profileCard.Controls.Add(lblPhone);
            profileCard.Controls.Add(_inputPhone);
            profileCard.Controls.Add(btnSave);
            profileCard.Location = new System.Drawing.Point(29, 33);
            profileCard.Margin = new System.Windows.Forms.Padding(0, 0, 34, 40);
            profileCard.Name = "profileCard";
            profileCard.Radius = 20;
            profileCard.Shadow = 5;
            profileCard.Size = new System.Drawing.Size(714, 700);
            profileCard.TabIndex = 0;
            // 
            // lblProfileTitle
            // 
            lblProfileTitle.AutoSizeMode = AntdUI.TAutoSize.Auto;
            lblProfileTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            lblProfileTitle.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)67)), ((int)((byte)24)), ((int)((byte)255)));
            lblProfileTitle.Location = new System.Drawing.Point(34, 40);
            lblProfileTitle.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            lblProfileTitle.Name = "lblProfileTitle";
            lblProfileTitle.Size = new System.Drawing.Size(290, 43);
            lblProfileTitle.TabIndex = 0;
            lblProfileTitle.Text = "Profile Information";
            // 
            // lblFullName
            // 
            lblFullName.AutoSizeMode = AntdUI.TAutoSize.Auto;
            lblFullName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblFullName.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)163)), ((int)((byte)174)), ((int)((byte)208)));
            lblFullName.Location = new System.Drawing.Point(34, 233);
            lblFullName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new System.Drawing.Size(96, 27);
            lblFullName.TabIndex = 2;
            lblFullName.Text = "Full Name";
            // 
            // _inputName
            // 
            _inputName.BackColor = System.Drawing.Color.FromArgb(((int)((byte)252)), ((int)((byte)253)), ((int)((byte)254)));
            _inputName.BorderColor = System.Drawing.Color.FromArgb(((int)((byte)224)), ((int)((byte)229)), ((int)((byte)242)));
            _inputName.Font = new System.Drawing.Font("Segoe UI", 10F);
            _inputName.Location = new System.Drawing.Point(34, 275);
            _inputName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            _inputName.Name = "_inputName";
            _inputName.Radius = 10;
            _inputName.Size = new System.Drawing.Size(646, 67);
            _inputName.TabIndex = 3;
            // 
            // lblEmail
            // 
            lblEmail.AutoSizeMode = AntdUI.TAutoSize.Auto;
            lblEmail.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblEmail.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)163)), ((int)((byte)174)), ((int)((byte)208)));
            lblEmail.Location = new System.Drawing.Point(34, 350);
            lblEmail.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new System.Drawing.Size(132, 27);
            lblEmail.TabIndex = 4;
            lblEmail.Text = "Email Address";
            // 
            // _inputEmail
            // 
            _inputEmail.BackColor = System.Drawing.Color.FromArgb(((int)((byte)252)), ((int)((byte)253)), ((int)((byte)254)));
            _inputEmail.BorderColor = System.Drawing.Color.FromArgb(((int)((byte)224)), ((int)((byte)229)), ((int)((byte)242)));
            _inputEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            _inputEmail.Location = new System.Drawing.Point(34, 392);
            _inputEmail.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            _inputEmail.Name = "_inputEmail";
            _inputEmail.Radius = 10;
            _inputEmail.Size = new System.Drawing.Size(646, 67);
            _inputEmail.TabIndex = 5;
            // 
            // lblPhone
            // 
            lblPhone.AutoSizeMode = AntdUI.TAutoSize.Auto;
            lblPhone.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblPhone.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)163)), ((int)((byte)174)), ((int)((byte)208)));
            lblPhone.Location = new System.Drawing.Point(34, 467);
            lblPhone.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new System.Drawing.Size(143, 27);
            lblPhone.TabIndex = 6;
            lblPhone.Text = "Phone Number";
            // 
            // _inputPhone
            // 
            _inputPhone.BackColor = System.Drawing.Color.FromArgb(((int)((byte)252)), ((int)((byte)253)), ((int)((byte)254)));
            _inputPhone.BorderColor = System.Drawing.Color.FromArgb(((int)((byte)224)), ((int)((byte)229)), ((int)((byte)242)));
            _inputPhone.Font = new System.Drawing.Font("Segoe UI", 10F);
            _inputPhone.Location = new System.Drawing.Point(34, 508);
            _inputPhone.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            _inputPhone.Name = "_inputPhone";
            _inputPhone.Radius = 10;
            _inputPhone.Size = new System.Drawing.Size(646, 67);
            _inputPhone.TabIndex = 7;
            // 
            // btnSave
            // 
            btnSave.BackColor = System.Drawing.Color.FromArgb(((int)((byte)67)), ((int)((byte)24)), ((int)((byte)255)));
            btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnSave.Location = new System.Drawing.Point(34, 600);
            btnSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btnSave.Name = "btnSave";
            btnSave.Radius = 8;
            btnSave.Size = new System.Drawing.Size(646, 67);
            btnSave.TabIndex = 8;
            btnSave.Text = "Save Changes";
            btnSave.Type = AntdUI.TTypeMini.Primary;
            btnSave.Click += OnSaveProfileClick;
            // 
            // passwordCard
            // 
            passwordCard.BackColor = System.Drawing.Color.White;
            passwordCard.Controls.Add(lblPassTitle);
            passwordCard.Controls.Add(lblCurrentPass);
            passwordCard.Controls.Add(_inputOldPassword);
            passwordCard.Controls.Add(lblNewPass);
            passwordCard.Controls.Add(_inputNewPassword);
            passwordCard.Controls.Add(btnPass);
            passwordCard.Location = new System.Drawing.Point(777, 33);
            passwordCard.Margin = new System.Windows.Forms.Padding(0, 0, 0, 40);
            passwordCard.Name = "passwordCard";
            passwordCard.Radius = 20;
            passwordCard.Shadow = 5;
            passwordCard.Size = new System.Drawing.Size(571, 533);
            passwordCard.TabIndex = 1;
            // 
            // lblPassTitle
            // 
            lblPassTitle.AutoSizeMode = AntdUI.TAutoSize.Auto;
            lblPassTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            lblPassTitle.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)67)), ((int)((byte)24)), ((int)((byte)255)));
            lblPassTitle.Location = new System.Drawing.Point(34, 40);
            lblPassTitle.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            lblPassTitle.Name = "lblPassTitle";
            lblPassTitle.Size = new System.Drawing.Size(265, 43);
            lblPassTitle.TabIndex = 0;
            lblPassTitle.Text = "Change Password";
            // 
            // lblCurrentPass
            // 
            lblCurrentPass.AutoSizeMode = AntdUI.TAutoSize.Auto;
            lblCurrentPass.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblCurrentPass.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)163)), ((int)((byte)174)), ((int)((byte)208)));
            lblCurrentPass.Location = new System.Drawing.Point(34, 133);
            lblCurrentPass.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            lblCurrentPass.Name = "lblCurrentPass";
            lblCurrentPass.Size = new System.Drawing.Size(166, 27);
            lblCurrentPass.TabIndex = 1;
            lblCurrentPass.Text = "Current Password";
            // 
            // _inputOldPassword
            // 
            _inputOldPassword.BackColor = System.Drawing.Color.FromArgb(((int)((byte)252)), ((int)((byte)253)), ((int)((byte)254)));
            _inputOldPassword.BorderColor = System.Drawing.Color.FromArgb(((int)((byte)224)), ((int)((byte)229)), ((int)((byte)242)));
            _inputOldPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            _inputOldPassword.Location = new System.Drawing.Point(34, 175);
            _inputOldPassword.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            _inputOldPassword.Name = "_inputOldPassword";
            _inputOldPassword.Radius = 10;
            _inputOldPassword.Size = new System.Drawing.Size(503, 67);
            _inputOldPassword.TabIndex = 2;
            _inputOldPassword.UseSystemPasswordChar = true;
            // 
            // lblNewPass
            // 
            lblNewPass.AutoSizeMode = AntdUI.TAutoSize.Auto;
            lblNewPass.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblNewPass.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)163)), ((int)((byte)174)), ((int)((byte)208)));
            lblNewPass.Location = new System.Drawing.Point(34, 258);
            lblNewPass.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            lblNewPass.Name = "lblNewPass";
            lblNewPass.Size = new System.Drawing.Size(138, 27);
            lblNewPass.TabIndex = 3;
            lblNewPass.Text = "New Password";
            // 
            // _inputNewPassword
            // 
            _inputNewPassword.BackColor = System.Drawing.Color.FromArgb(((int)((byte)252)), ((int)((byte)253)), ((int)((byte)254)));
            _inputNewPassword.BorderColor = System.Drawing.Color.FromArgb(((int)((byte)224)), ((int)((byte)229)), ((int)((byte)242)));
            _inputNewPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            _inputNewPassword.Location = new System.Drawing.Point(34, 300);
            _inputNewPassword.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            _inputNewPassword.Name = "_inputNewPassword";
            _inputNewPassword.Radius = 10;
            _inputNewPassword.Size = new System.Drawing.Size(503, 67);
            _inputNewPassword.TabIndex = 4;
            _inputNewPassword.UseSystemPasswordChar = true;
            // 
            // btnPass
            // 
            btnPass.BorderWidth = 1F;
            btnPass.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnPass.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)67)), ((int)((byte)24)), ((int)((byte)255)));
            btnPass.Location = new System.Drawing.Point(34, 417);
            btnPass.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btnPass.Name = "btnPass";
            btnPass.Radius = 8;
            btnPass.Size = new System.Drawing.Size(503, 67);
            btnPass.TabIndex = 5;
            btnPass.Text = "Update Password";
            btnPass.Click += OnChangePasswordClick;
            // 
            // Settings
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(((int)((byte)244)), ((int)((byte)247)), ((int)((byte)254)));
            ClientSize = new System.Drawing.Size(1714, 1226);
            Controls.Add(contentFlow);
            Controls.Add(lblTitle);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            Text = "Settings";
            contentFlow.ResumeLayout(false);
            profileCard.ResumeLayout(false);
            profileCard.PerformLayout();
            passwordCard.ResumeLayout(false);
            passwordCard.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private AntdUI.Label lblTitle;
        private System.Windows.Forms.FlowLayoutPanel contentFlow;
        private AntdUI.Panel profileCard;
        private AntdUI.Label lblProfileTitle;
        private AntdUI.Label lblFullName;
        private AntdUI.Input _inputName;
        private AntdUI.Label lblEmail;
        private AntdUI.Input _inputEmail;
        private AntdUI.Label lblPhone;
        private AntdUI.Input _inputPhone;
        private AntdUI.Button btnSave;
        private AntdUI.Panel passwordCard;
        private AntdUI.Label lblPassTitle;
        private AntdUI.Label lblCurrentPass;
        private AntdUI.Input _inputOldPassword;
        private AntdUI.Label lblNewPass;
        private AntdUI.Input _inputNewPassword;
        private AntdUI.Button btnPass;
    }
}
