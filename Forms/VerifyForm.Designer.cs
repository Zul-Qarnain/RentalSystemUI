namespace RentalSystemUI.Forms
{
    partial class VerifyForm
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
            this.components = new System.ComponentModel.Container();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtInput = new AntdUI.Input();
            this.btnConfirm = new AntdUI.Button();
            this.btnCancel = new AntdUI.Button();
            this.lblTimer = new System.Windows.Forms.Label();
            this.linkResend = new System.Windows.Forms.LinkLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(150, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Verification";
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(25, 70);
            this.txtInput.Name = "txtInput";
            this.txtInput.PlaceholderText = "Enter details...";
            this.txtInput.Radius = 6;
            this.txtInput.Size = new System.Drawing.Size(330, 45);
            this.txtInput.TabIndex = 1;
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnConfirm.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnConfirm.ForeColor = System.Drawing.Color.White;
            this.btnConfirm.Location = new System.Drawing.Point(195, 170);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Radius = 6;
            this.btnConfirm.Size = new System.Drawing.Size(160, 45);
            this.btnConfirm.TabIndex = 2;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.Type = AntdUI.TTypeMini.Primary;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.ForeColor = System.Drawing.Color.Gray;
            this.btnCancel.Location = new System.Drawing.Point(25, 170);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Radius = 6;
            this.btnCancel.Size = new System.Drawing.Size(120, 45);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Type = AntdUI.TTypeMini.Default;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblTimer
            // 
            this.lblTimer.AutoSize = true;
            this.lblTimer.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTimer.ForeColor = System.Drawing.Color.Red;
            this.lblTimer.Location = new System.Drawing.Point(25, 125);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(125, 20);
            this.lblTimer.TabIndex = 4;
            this.lblTimer.Text = "Resend in: 00:60";
            // 
            // linkResend
            // 
            this.linkResend.AutoSize = true;
            this.linkResend.Enabled = false;
            this.linkResend.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.linkResend.Location = new System.Drawing.Point(260, 125);
            this.linkResend.Name = "linkResend";
            this.linkResend.Size = new System.Drawing.Size(101, 20);
            this.linkResend.TabIndex = 5;
            this.linkResend.TabStop = true;
            this.linkResend.Text = "Resend Code";
            this.linkResend.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkResend_LinkClicked);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // VerifyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(380, 240);
            this.Controls.Add(this.linkResend);
            this.Controls.Add(this.lblTimer);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "VerifyForm";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private AntdUI.Input txtInput;
        private AntdUI.Button btnConfirm;
        private AntdUI.Button btnCancel;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.LinkLabel linkResend;
        private System.Windows.Forms.Timer timer1;
    }
}