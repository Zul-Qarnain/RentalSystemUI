
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

        private void InitializeComponent()
        {
            this.lblTitle = new AntdUI.Label();
            this.contentFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.profileCard = new AntdUI.Panel();
            this.lblProfileTitle = new AntdUI.Label();
            this.avatar = new AntdUI.Avatar();
            this.lblFullName = new AntdUI.Label();
            this._inputName = new AntdUI.Input();
            this.lblEmail = new AntdUI.Label();
            this._inputEmail = new AntdUI.Input();
            this.lblPhone = new AntdUI.Label();
            this._inputPhone = new AntdUI.Input();
            this.btnSave = new AntdUI.Button();
            this.passwordCard = new AntdUI.Panel();
            this.lblPassTitle = new AntdUI.Label();
            this.lblCurrentPass = new AntdUI.Label();
            this._inputOldPassword = new AntdUI.Input();
            this.lblNewPass = new AntdUI.Label();
            this._inputNewPassword = new AntdUI.Input();
            this.btnPass = new AntdUI.Button();
            this.contentFlow.SuspendLayout();
            this.profileCard.SuspendLayout();
            this.passwordCard.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(24)))), ((int)(((byte)(255)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(20, 25, 0, 0);
            this.lblTitle.Size = new System.Drawing.Size(1200, 80);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Account Settings";
            // 
            // contentFlow
            // 
            this.contentFlow.AutoScroll = true;
            this.contentFlow.Controls.Add(this.profileCard);
            this.contentFlow.Controls.Add(this.passwordCard);
            this.contentFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentFlow.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.contentFlow.Location = new System.Drawing.Point(0, 80);
            this.contentFlow.Name = "contentFlow";
            this.contentFlow.Padding = new System.Windows.Forms.Padding(20);
            this.contentFlow.Size = new System.Drawing.Size(1200, 720);
            this.contentFlow.TabIndex = 1;
            // 
            // profileCard
            // 
            this.profileCard.BackColor = System.Drawing.Color.White;
            this.profileCard.Controls.Add(this.lblProfileTitle);
            this.profileCard.Controls.Add(this.avatar);
            this.profileCard.Controls.Add(this.lblFullName);
            this.profileCard.Controls.Add(this._inputName);
            this.profileCard.Controls.Add(this.lblEmail);
            this.profileCard.Controls.Add(this._inputEmail);
            this.profileCard.Controls.Add(this.lblPhone);
            this.profileCard.Controls.Add(this._inputPhone);
            this.profileCard.Controls.Add(this.btnSave);
            this.profileCard.Location = new System.Drawing.Point(23, 23);
            this.profileCard.Margin = new System.Windows.Forms.Padding(0, 0, 24, 24);
            this.profileCard.Name = "profileCard";
            this.profileCard.Radius = 20;
            this.profileCard.Shadow = 5;
            this.profileCard.Size = new System.Drawing.Size(500, 420);
            this.profileCard.TabIndex = 0;
            // 
            // lblProfileTitle
            // 
            this.lblProfileTitle.AutoSize = true;
            this.lblProfileTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblProfileTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(24)))), ((int)(((byte)(255)))));
            this.lblProfileTitle.Location = new System.Drawing.Point(24, 24);
            this.lblProfileTitle.Name = "lblProfileTitle";
            this.lblProfileTitle.Size = new System.Drawing.Size(217, 30);
            this.lblProfileTitle.TabIndex = 0;
            this.lblProfileTitle.Text = "Profile Information";
            // 
            // avatar
            // 
            this.avatar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.avatar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(24)))), ((int)(((byte)(255)))));
            this.avatar.Location = new System.Drawing.Point(220, 60);
            this.avatar.Name = "avatar";
            this.avatar.Size = new System.Drawing.Size(60, 60);
            this.avatar.TabIndex = 1;
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblFullName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(174)))), ((int)(((byte)(208)))));
            this.lblFullName.Location = new System.Drawing.Point(24, 140);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(76, 19);
            this.lblFullName.TabIndex = 2;
            this.lblFullName.Text = "Full Name";
            // 
            // _inputName
            // 
            this._inputName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(253)))), ((int)(((byte)(254)))));
            this._inputName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(229)))), ((int)(((byte)(242)))));
            this._inputName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._inputName.Location = new System.Drawing.Point(24, 165);
            this._inputName.Name = "_inputName";
            this._inputName.Radius = 10;
            this._inputName.Size = new System.Drawing.Size(452, 40);
            this._inputName.TabIndex = 3;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(174)))), ((int)(((byte)(208)))));
            this.lblEmail.Location = new System.Drawing.Point(24, 210);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(103, 19);
            this.lblEmail.TabIndex = 4;
            this.lblEmail.Text = "Email Address";
            // 
            // _inputEmail
            // 
            this._inputEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(253)))), ((int)(((byte)(254)))));
            this._inputEmail.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(229)))), ((int)(((byte)(242)))));
            this._inputEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._inputEmail.Location = new System.Drawing.Point(24, 235);
            this._inputEmail.Name = "_inputEmail";
            this._inputEmail.Radius = 10;
            this._inputEmail.Size = new System.Drawing.Size(452, 40);
            this._inputEmail.TabIndex = 5;
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(174)))), ((int)(((byte)(208)))));
            this.lblPhone.Location = new System.Drawing.Point(24, 280);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(110, 19);
            this.lblPhone.TabIndex = 6;
            this.lblPhone.Text = "Phone Number";
            // 
            // _inputPhone
            // 
            this._inputPhone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(253)))), ((int)(((byte)(254)))));
            this._inputPhone.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(229)))), ((int)(((byte)(242)))));
            this._inputPhone.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._inputPhone.Location = new System.Drawing.Point(24, 305);
            this._inputPhone.Name = "_inputPhone";
            this._inputPhone.Radius = 10;
            this._inputPhone.Size = new System.Drawing.Size(452, 40);
            this._inputPhone.TabIndex = 7;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(24)))), ((int)(((byte)(255)))));
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.Location = new System.Drawing.Point(24, 360);
            this.btnSave.Name = "btnSave";
            this.btnSave.Radius = 8;
            this.btnSave.Size = new System.Drawing.Size(452, 40);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save Changes";
            this.btnSave.Type = AntdUI.TTypeMini.Primary;
            this.btnSave.Click += new System.EventHandler(this.OnSaveProfileClick);
            // 
            // passwordCard
            // 
            this.passwordCard.BackColor = System.Drawing.Color.White;
            this.passwordCard.Controls.Add(this.lblPassTitle);
            this.passwordCard.Controls.Add(this.lblCurrentPass);
            this.passwordCard.Controls.Add(this._inputOldPassword);
            this.passwordCard.Controls.Add(this.lblNewPass);
            this.passwordCard.Controls.Add(this._inputNewPassword);
            this.passwordCard.Controls.Add(this.btnPass);
            this.passwordCard.Location = new System.Drawing.Point(547, 23);
            this.passwordCard.Margin = new System.Windows.Forms.Padding(0, 0, 0, 24);
            this.passwordCard.Name = "passwordCard";
            this.passwordCard.Radius = 20;
            this.passwordCard.Shadow = 5;
            this.passwordCard.Size = new System.Drawing.Size(400, 320);
            this.passwordCard.TabIndex = 1;
            // 
            // lblPassTitle
            // 
            this.lblPassTitle.AutoSize = true;
            this.lblPassTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblPassTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(24)))), ((int)(((byte)(255)))));
            this.lblPassTitle.Location = new System.Drawing.Point(24, 24);
            this.lblPassTitle.Name = "lblPassTitle";
            this.lblPassTitle.Size = new System.Drawing.Size(193, 30);
            this.lblPassTitle.TabIndex = 0;
            this.lblPassTitle.Text = "Change Password";
            // 
            // lblCurrentPass
            // 
            this.lblCurrentPass.AutoSize = true;
            this.lblCurrentPass.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCurrentPass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(174)))), ((int)(((byte)(208)))));
            this.lblCurrentPass.Location = new System.Drawing.Point(24, 80);
            this.lblCurrentPass.Name = "lblCurrentPass";
            this.lblCurrentPass.Size = new System.Drawing.Size(125, 19);
            this.lblCurrentPass.TabIndex = 1;
            this.lblCurrentPass.Text = "Current Password";
            // 
            // _inputOldPassword
            // 
            this._inputOldPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(253)))), ((int)(((byte)(254)))));
            this._inputOldPassword.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(229)))), ((int)(((byte)(242)))));
            this._inputOldPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._inputOldPassword.Location = new System.Drawing.Point(24, 105);
            this._inputOldPassword.Name = "_inputOldPassword";
            this._inputOldPassword.Radius = 10;
            this._inputOldPassword.Size = new System.Drawing.Size(352, 40);
            this._inputOldPassword.TabIndex = 2;
            this._inputOldPassword.UseSystemPasswordChar = true;
            // 
            // lblNewPass
            // 
            this.lblNewPass.AutoSize = true;
            this.lblNewPass.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNewPass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(174)))), ((int)(((byte)(208)))));
            this.lblNewPass.Location = new System.Drawing.Point(24, 155);
            this.lblNewPass.Name = "lblNewPass";
            this.lblNewPass.Size = new System.Drawing.Size(107, 19);
            this.lblNewPass.TabIndex = 3;
            this.lblNewPass.Text = "New Password";
            // 
            // _inputNewPassword
            // 
            this._inputNewPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(253)))), ((int)(((byte)(254)))));
            this._inputNewPassword.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(229)))), ((int)(((byte)(242)))));
            this._inputNewPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._inputNewPassword.Location = new System.Drawing.Point(24, 180);
            this._inputNewPassword.Name = "_inputNewPassword";
            this._inputNewPassword.Radius = 10;
            this._inputNewPassword.Size = new System.Drawing.Size(352, 40);
            this._inputNewPassword.TabIndex = 4;
            this._inputNewPassword.UseSystemPasswordChar = true;
            // 
            // btnPass
            // 
            this.btnPass.BorderWidth = 1F;
            this.btnPass.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnPass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(24)))), ((int)(((byte)(255)))));
            this.btnPass.Location = new System.Drawing.Point(24, 250);
            this.btnPass.Name = "btnPass";
            this.btnPass.Radius = 8;
            this.btnPass.Size = new System.Drawing.Size(352, 40);
            this.btnPass.TabIndex = 5;
            this.btnPass.Text = "Update Password";
            this.btnPass.Type = AntdUI.TTypeMini.Default;
            this.btnPass.Click += new System.EventHandler(this.OnChangePasswordClick);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.contentFlow);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Settings";
            this.Text = "Settings";
            this.contentFlow.ResumeLayout(false);
            this.profileCard.ResumeLayout(false);
            this.profileCard.PerformLayout();
            this.passwordCard.ResumeLayout(false);
            this.passwordCard.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.Label lblTitle;
        private System.Windows.Forms.FlowLayoutPanel contentFlow;
        private AntdUI.Panel profileCard;
        private AntdUI.Label lblProfileTitle;
        private AntdUI.Avatar avatar;
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
