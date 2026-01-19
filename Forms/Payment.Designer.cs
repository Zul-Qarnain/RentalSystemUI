namespace RentalSystemUI.Forms
{
    partial class Payment
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
            pnlBackground = new Panel();
            cardDetails = new AntdUI.Panel();
            btnCancel = new AntdUI.Button();
            btnConfirm = new AntdUI.Button();
            inputTrxId = new AntdUI.Input();
            inputSenderPhone = new AntdUI.Input();
            selMobileMethod = new AntdUI.Select();
            lblTrx = new AntdUI.Label();
            lblSender = new AntdUI.Label();
            lblSelectMethod = new AntdUI.Label();
            btnMobileBanking = new AntdUI.Button();
            btnEpay = new AntdUI.Button();
            lblPayMethod = new AntdUI.Label();
            cardSummary = new AntdUI.Panel();
            lblOwnerPhone = new AntdUI.Label();
            lblTotal = new AntdUI.Label();
            lblDuration = new AntdUI.Label();
            lblMonthlyRent = new AntdUI.Label();
            lblPropertyTitle = new AntdUI.Label();
            lblSubHeader = new AntdUI.Label();
            lblHeader = new AntdUI.Label();
            pnlNav = new Panel();
            btnClose = new AntdUI.Button();
            lblLogo = new AntdUI.Label();
            pnlBackground.SuspendLayout();
            cardDetails.SuspendLayout();
            cardSummary.SuspendLayout();
            pnlNav.SuspendLayout();
            SuspendLayout();
            // 
            // pnlBackground
            // 
            pnlBackground.BackColor = Color.FromArgb(248, 249, 250);
            pnlBackground.Controls.Add(cardDetails);
            pnlBackground.Controls.Add(cardSummary);
            pnlBackground.Controls.Add(lblSubHeader);
            pnlBackground.Controls.Add(lblHeader);
            pnlBackground.Controls.Add(pnlNav);
            pnlBackground.Dock = DockStyle.Fill;
            pnlBackground.Location = new Point(0, 0);
            pnlBackground.Name = "pnlBackground";
            pnlBackground.Size = new Size(1100, 800);
            pnlBackground.TabIndex = 0;
            // 
            // cardDetails
            // 
            cardDetails.Back = Color.White;
            cardDetails.Controls.Add(btnCancel);
            cardDetails.Controls.Add(btnConfirm);
            cardDetails.Controls.Add(inputTrxId);
            cardDetails.Controls.Add(inputSenderPhone);
            cardDetails.Controls.Add(selMobileMethod);
            cardDetails.Controls.Add(lblTrx);
            cardDetails.Controls.Add(lblSender);
            cardDetails.Controls.Add(lblSelectMethod);
            cardDetails.Controls.Add(btnMobileBanking);
            cardDetails.Controls.Add(btnEpay);
            cardDetails.Controls.Add(lblPayMethod);
            cardDetails.Location = new Point(470, 230);
            cardDetails.Name = "cardDetails";
            cardDetails.Radius = 12;
            cardDetails.Shadow = 10;
            cardDetails.Size = new Size(550, 500);
            cardDetails.TabIndex = 0;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.White;
            btnCancel.BorderWidth = 1F;
            btnCancel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnCancel.ForeColor = Color.FromArgb(100, 116, 139);
            btnCancel.Location = new Point(30, 430);
            btnCancel.Name = "btnCancel";
            btnCancel.Radius = 10;
            btnCancel.Size = new Size(150, 50);
            btnCancel.TabIndex = 10;
            btnCancel.Text = "Cancel";
            btnCancel.Type = AntdUI.TTypeMini.Default;
            // 
            // btnConfirm
            // 
            btnConfirm.DefaultBack = Color.FromArgb(22, 119, 255);
            btnConfirm.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnConfirm.ForeColor = Color.White;
            btnConfirm.Location = new Point(200, 430);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Radius = 10;
            btnConfirm.Size = new Size(320, 50);
            btnConfirm.TabIndex = 9;
            btnConfirm.Text = "Confirm Payment";
            btnConfirm.Type = AntdUI.TTypeMini.Primary;
            // 
            // inputTrxId
            // 
            inputTrxId.Location = new Point(30, 355);
            inputTrxId.Name = "inputTrxId";
            inputTrxId.Radius = 8;
            inputTrxId.Size = new Size(490, 45);
            inputTrxId.TabIndex = 8;
            // 
            // inputSenderPhone
            // 
            inputSenderPhone.Location = new Point(30, 275);
            inputSenderPhone.Name = "inputSenderPhone";
            inputSenderPhone.Radius = 8;
            inputSenderPhone.Size = new Size(490, 45);
            inputSenderPhone.TabIndex = 7;
            // 
            // selMobileMethod
            // 
            selMobileMethod.Items.AddRange(new object[] { "Bkash", "Nagad" });
            selMobileMethod.Location = new Point(30, 200);
            selMobileMethod.Name = "selMobileMethod";
            selMobileMethod.PlaceholderText = "Select Method";
            selMobileMethod.Size = new Size(490, 40);
            selMobileMethod.TabIndex = 6;
            // 
            // lblTrx
            // 
            lblTrx.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTrx.ForeColor = Color.FromArgb(15, 23, 42);
            lblTrx.Location = new Point(30, 330);
            lblTrx.Name = "lblTrx";
            lblTrx.Size = new Size(490, 25);
            lblTrx.TabIndex = 5;
            lblTrx.Text = "Transaction ID";
            // 
            // lblSender
            // 
            lblSender.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSender.ForeColor = Color.FromArgb(15, 23, 42);
            lblSender.Location = new Point(30, 250);
            lblSender.Name = "lblSender";
            lblSender.Size = new Size(490, 25);
            lblSender.TabIndex = 4;
            lblSender.Text = "Sender Phone Number";
            // 
            // lblSelectMethod
            // 
            lblSelectMethod.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSelectMethod.ForeColor = Color.FromArgb(15, 23, 42);
            lblSelectMethod.Location = new Point(30, 175);
            lblSelectMethod.Name = "lblSelectMethod";
            lblSelectMethod.Size = new Size(490, 25);
            lblSelectMethod.TabIndex = 3;
            lblSelectMethod.Text = "Select Method";
            // 
            // btnMobileBanking
            // 
            btnMobileBanking.BackColor = Color.FromArgb(22, 119, 255);
            btnMobileBanking.ForeColor = Color.White;
            btnMobileBanking.Location = new Point(180, 95);
            btnMobileBanking.Name = "btnMobileBanking";
            btnMobileBanking.Radius = 10;
            btnMobileBanking.Size = new Size(340, 55);
            btnMobileBanking.TabIndex = 2;
            btnMobileBanking.Text = "Mobile Banking";
            btnMobileBanking.Type = AntdUI.TTypeMini.Primary;
            // 
            // btnEpay
            // 
            btnEpay.BackColor = Color.FromArgb(248, 250, 252);
            btnEpay.Enabled = false;
            btnEpay.ForeColor = Color.FromArgb(148, 163, 184);
            btnEpay.Location = new Point(30, 95);
            btnEpay.Name = "btnEpay";
            btnEpay.Radius = 10;
            btnEpay.Size = new Size(140, 55);
            btnEpay.TabIndex = 1;
            btnEpay.Text = "ePay\n(Coming Soon)";
            btnEpay.Type = AntdUI.TTypeMini.Default;
            // 
            // lblPayMethod
            // 
            lblPayMethod.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblPayMethod.ForeColor = Color.FromArgb(15, 23, 42);
            lblPayMethod.Location = new Point(30, 30);
            lblPayMethod.Name = "lblPayMethod";
            lblPayMethod.Size = new Size(490, 30);
            lblPayMethod.TabIndex = 0;
            lblPayMethod.Text = "Payment Method";
            // 
            // cardSummary
            // 
            cardSummary.Back = Color.White;
            cardSummary.Controls.Add(lblOwnerPhone);
            cardSummary.Controls.Add(lblTotal);
            cardSummary.Controls.Add(lblDuration);
            cardSummary.Controls.Add(lblMonthlyRent);
            cardSummary.Controls.Add(lblPropertyTitle);
            cardSummary.Location = new Point(60, 230);
            cardSummary.Name = "cardSummary";
            cardSummary.Radius = 12;
            cardSummary.Shadow = 10;
            cardSummary.Size = new Size(380, 500);
            cardSummary.TabIndex = 1;
            // 
            // lblOwnerPhone
            // 
            lblOwnerPhone.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblOwnerPhone.ForeColor = Color.FromArgb(22, 119, 255);
            lblOwnerPhone.Location = new Point(20, 215);
            lblOwnerPhone.Name = "lblOwnerPhone";
            lblOwnerPhone.Size = new Size(340, 60);
            lblOwnerPhone.TabIndex = 4;
            lblOwnerPhone.Text = "Send money to this number:\n01XXXXXXXXX";
            // 
            // lblTotal
            // 
            lblTotal.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTotal.ForeColor = Color.FromArgb(22, 119, 255);
            lblTotal.Location = new Point(20, 320);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(340, 40);
            lblTotal.TabIndex = 3;
            lblTotal.Text = "Total: ৳0";
            // 
            // lblDuration
            // 
            lblDuration.ForeColor = Color.FromArgb(100, 116, 139);
            lblDuration.Location = new Point(20, 175);
            lblDuration.Name = "lblDuration";
            lblDuration.Size = new Size(340, 25);
            lblDuration.TabIndex = 2;
            lblDuration.Text = "Duration: 0 month(s)";
            // 
            // lblMonthlyRent
            // 
            lblMonthlyRent.ForeColor = Color.FromArgb(100, 116, 139);
            lblMonthlyRent.Location = new Point(20, 145);
            lblMonthlyRent.Name = "lblMonthlyRent";
            lblMonthlyRent.Size = new Size(340, 25);
            lblMonthlyRent.TabIndex = 1;
            lblMonthlyRent.Text = "Monthly Rent: ৳0";
            // 
            // lblPropertyTitle
            // 
            lblPropertyTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblPropertyTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblPropertyTitle.Location = new Point(20, 30);
            lblPropertyTitle.Name = "lblPropertyTitle";
            lblPropertyTitle.Size = new Size(340, 90);
            lblPropertyTitle.TabIndex = 0;
            lblPropertyTitle.Text = "Property";
            // 
            // lblSubHeader
            // 
            lblSubHeader.ForeColor = Color.FromArgb(100, 116, 139);
            lblSubHeader.Location = new Point(64, 170);
            lblSubHeader.Name = "lblSubHeader";
            lblSubHeader.Size = new Size(800, 30);
            lblSubHeader.TabIndex = 2;
            lblSubHeader.Text = "Mobile banking checkout for your booking.";
            // 
            // lblHeader
            // 
            lblHeader.Font = new Font("Segoe UI", 26F, FontStyle.Bold);
            lblHeader.Location = new Point(60, 110);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(800, 60);
            lblHeader.TabIndex = 3;
            lblHeader.Text = "Complete Your Payment";
            // 
            // pnlNav
            // 
            pnlNav.BackColor = Color.White;
            pnlNav.Controls.Add(btnClose);
            pnlNav.Controls.Add(lblLogo);
            pnlNav.Dock = DockStyle.Top;
            pnlNav.Location = new Point(0, 0);
            pnlNav.Name = "pnlNav";
            pnlNav.Size = new Size(1100, 70);
            pnlNav.TabIndex = 4;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.White;
            btnClose.BorderWidth = 0F;
            btnClose.ForeColor = Color.FromArgb(100, 116, 139);
            btnClose.Location = new Point(1040, 16);
            btnClose.Name = "btnClose";
            btnClose.Radius = 8;
            btnClose.Size = new Size(40, 40);
            btnClose.TabIndex = 0;
            btnClose.Text = "✕";
            btnClose.Type = AntdUI.TTypeMini.Default;
            // 
            // lblLogo
            // 
            lblLogo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblLogo.Location = new Point(30, 15);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(400, 40);
            lblLogo.TabIndex = 1;
            lblLogo.Text = "Payment";
            // 
            // Payment
            // 
            ClientSize = new Size(1100, 800);
            Controls.Add(pnlBackground);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Payment";
            StartPosition = FormStartPosition.CenterScreen;
            pnlBackground.ResumeLayout(false);
            cardDetails.ResumeLayout(false);
            cardSummary.ResumeLayout(false);
            pnlNav.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlBackground;
        private System.Windows.Forms.Panel pnlNav;
        private AntdUI.Label lblLogo;
        private AntdUI.Button btnClose;
        private AntdUI.Label lblHeader;
        private AntdUI.Label lblSubHeader;
        private AntdUI.Panel cardSummary;
        private AntdUI.Panel cardDetails;

        private AntdUI.Label lblPropertyTitle;
        private AntdUI.Label lblMonthlyRent;
        private AntdUI.Label lblDuration;
        private AntdUI.Label lblTotal;
        private AntdUI.Label lblOwnerPhone;

        private AntdUI.Label lblPayMethod;
        private AntdUI.Button btnEpay;
        private AntdUI.Button btnMobileBanking;
        private AntdUI.Label lblSelectMethod;
        private AntdUI.Select selMobileMethod;
        private AntdUI.Label lblSender;
        private AntdUI.Input inputSenderPhone;
        private AntdUI.Label lblTrx;
        private AntdUI.Input inputTrxId;
        private AntdUI.Button btnConfirm;
        private AntdUI.Button btnCancel;
    }
}