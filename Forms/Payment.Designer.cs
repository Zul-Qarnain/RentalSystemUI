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
            AntdUI.Tabs.StyleLine styleLine2 = new AntdUI.Tabs.StyleLine();
            pnlBackground = new Panel();
            cardDetails = new AntdUI.Panel();
            btnPay = new AntdUI.Button();
            txtCVC = new AntdUI.Input();
            txtExpiry = new AntdUI.Input();
            txtNumber = new AntdUI.Input();
            txtName = new AntdUI.Input();
            tabMethods = new AntdUI.Tabs();
            cardSummary = new AntdUI.Panel();
            lblSubHeader = new AntdUI.Label();
            lblHeader = new AntdUI.Label();
            pnlNav = new Panel();
            lblNavLinks = new AntdUI.Label();
            lblLogo = new AntdUI.Label();
            pnlBackground.SuspendLayout();
            cardDetails.SuspendLayout();
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
            cardDetails.Controls.Add(btnPay);
            cardDetails.Controls.Add(txtCVC);
            cardDetails.Controls.Add(txtExpiry);
            cardDetails.Controls.Add(txtNumber);
            cardDetails.Controls.Add(txtName);
            cardDetails.Controls.Add(tabMethods);
            cardDetails.Location = new Point(470, 230);
            cardDetails.Name = "cardDetails";
            cardDetails.Radius = 12;
            cardDetails.Shadow = 10;
            cardDetails.Size = new Size(550, 500);
            cardDetails.TabIndex = 0;
            // 
            // btnPay
            // 
            btnPay.DefaultBack = Color.FromArgb(22, 119, 255);
            btnPay.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnPay.ForeColor = Color.White;
            btnPay.Location = new Point(30, 410);
            btnPay.Name = "btnPay";
            btnPay.Size = new Size(490, 60);
            btnPay.TabIndex = 0;
            btnPay.Text = "Pay $1,265.00";
            // 
            // txtCVC
            // 
            txtCVC.Location = new Point(0, 0);
            txtCVC.Name = "txtCVC";
            txtCVC.Size = new Size(0, 0);
            txtCVC.TabIndex = 1;
            // 
            // txtExpiry
            // 
            txtExpiry.Location = new Point(0, 0);
            txtExpiry.Name = "txtExpiry";
            txtExpiry.Size = new Size(0, 0);
            txtExpiry.TabIndex = 2;
            // 
            // txtNumber
            // 
            txtNumber.Location = new Point(30, 180);
            txtNumber.Name = "txtNumber";
            txtNumber.Size = new Size(490, 50);
            txtNumber.TabIndex = 3;
            // 
            // txtName
            // 
            txtName.Location = new Point(30, 100);
            txtName.Name = "txtName";
            txtName.Size = new Size(490, 50);
            txtName.TabIndex = 4;
            // 
            // tabMethods
            // 
            tabMethods.Location = new Point(20, 10);
            tabMethods.Name = "tabMethods";
            tabMethods.Size = new Size(510, 50);
            tabMethods.Style = styleLine2;
            tabMethods.TabIndex = 5;
            // 
            // cardSummary
            // 
            cardSummary.Back = Color.White;
            cardSummary.Location = new Point(60, 230);
            cardSummary.Name = "cardSummary";
            cardSummary.Radius = 12;
            cardSummary.Shadow = 10;
            cardSummary.Size = new Size(380, 500);
            cardSummary.TabIndex = 1;
            // 
            // lblSubHeader
            // 
            lblSubHeader.ForeColor = Color.FromArgb(100, 116, 139);
            lblSubHeader.Location = new Point(64, 170);
            lblSubHeader.Name = "lblSubHeader";
            lblSubHeader.Size = new Size(600, 30);
            lblSubHeader.TabIndex = 2;
            lblSubHeader.Text = "Secure checkout for your upcoming rental period.";
            // 
            // lblHeader
            // 
            lblHeader.Font = new Font("Segoe UI", 26F, FontStyle.Bold);
            lblHeader.Location = new Point(60, 110);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(600, 60);
            lblHeader.TabIndex = 3;
            lblHeader.Text = "Complete Your Payment";
            // 
            // pnlNav
            // 
            pnlNav.BackColor = Color.White;
            pnlNav.Controls.Add(lblNavLinks);
            pnlNav.Controls.Add(lblLogo);
            pnlNav.Dock = DockStyle.Top;
            pnlNav.Location = new Point(0, 0);
            pnlNav.Name = "pnlNav";
            pnlNav.Size = new Size(1100, 70);
            pnlNav.TabIndex = 4;
            // 
            // lblNavLinks
            // 
            lblNavLinks.Font = new Font("Segoe UI", 10F);
            lblNavLinks.ForeColor = Color.Gray;
            lblNavLinks.Location = new Point(600, 15);
            lblNavLinks.Name = "lblNavLinks";
            lblNavLinks.Size = new Size(450, 40);
            lblNavLinks.TabIndex = 0;
            lblNavLinks.Text = "Dashboard    Properties    Discover    Payments";
            lblNavLinks.Click += lblNavLinks_Click;
            // 
            // lblLogo
            // 
            lblLogo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblLogo.Location = new Point(30, 15);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(200, 40);
            lblLogo.TabIndex = 1;
            lblLogo.Text = "🏠 Rental Manager";
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
            pnlNav.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlBackground;
        private System.Windows.Forms.Panel pnlNav;
        private AntdUI.Label lblLogo;
        private AntdUI.Label lblNavLinks;
        private AntdUI.Label lblHeader;
        private AntdUI.Label lblSubHeader;
        private AntdUI.Panel cardSummary;
        private AntdUI.Panel cardDetails;
        private AntdUI.Tabs tabMethods;
        private AntdUI.Input txtName;
        private AntdUI.Input txtNumber;
        private AntdUI.Input txtExpiry;
        private AntdUI.Input txtCVC;
        private AntdUI.Button btnPay;
    }
}