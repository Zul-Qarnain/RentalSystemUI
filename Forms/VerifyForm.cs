using System;
using System.Drawing;
using System.Windows.Forms;
using AntdUI;

namespace RentalSystemUI.Forms
{
    public partial class VerifyForm : Form
    {
        public string InputValue { get; private set; } = "";
        public event EventHandler? ResendRequested;
        private int timeLeft = 60;

        // Constructor accepting 4 arguments
        public VerifyForm(string title, string placeholder, bool isPassword = false, bool enableTimer = false)
        {
            InitializeComponent();

            // Safe UI setting
            if (lblTitle != null) lblTitle.Text = title;
            if (txtInput != null)
            {
                txtInput.PlaceholderText = placeholder;
                if (isPassword) txtInput.UseSystemPasswordChar = true;
            }

            // Timer Logic
            if (enableTimer)
            {
                if (timer1 != null) timer1.Start();
                if (linkResend != null)
                {
                    linkResend.Enabled = false;
                    linkResend.LinkColor = Color.Gray;
                    linkResend.Visible = true;
                }
                if (lblTimer != null) lblTimer.Visible = true;
            }
            else
            {
                if (lblTimer != null) lblTimer.Visible = false;
                if (linkResend != null) linkResend.Visible = false;
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtInput.Text))
            {
                AntdUI.Message.error(this, "Field cannot be empty.");
                return;
            }

            InputValue = txtInput.Text.Trim(); // Store input
            this.DialogResult = DialogResult.OK; // Important: This tells Form1 "Success"
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                timeLeft--;
                lblTimer.Text = $"Resend in: 00:{timeLeft:D2}";
            }
            else
            {
                timer1.Stop();
                lblTimer.Text = "Code expired?";
                linkResend.Enabled = true;
                linkResend.LinkColor = Color.FromArgb(37, 99, 235);
            }
        }

        private void linkResend_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            timeLeft = 60;
            timer1.Start();
            linkResend.Enabled = false;
            linkResend.LinkColor = Color.Gray;
            ResendRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}