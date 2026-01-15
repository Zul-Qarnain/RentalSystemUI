namespace RentalSystemUI.Forms
{
    partial class HomeownerDashboard
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
            pnlSidebar = new Panel();
            pnlSidebarBottom = new Panel();
            lblUserEmail = new Label();
            lblUserName = new Label();
            picUser = new PictureBox();
            btnSettings = new AntdUI.Button();
            btnTenants = new AntdUI.Button();
            btnPayments = new AntdUI.Button();
            btnBookings = new AntdUI.Button();
            btnProperties = new AntdUI.Button();
            btnDashboard = new AntdUI.Button();
            pnlBrand = new Panel();
            lblBrandSubtitle = new Label();
            lblBrandTitle = new Label();
            pnlTopBar = new Panel();
            btnClose = new AntdUI.Button();
            btnMinimize = new AntdUI.Button();
            btnNotif = new AntdUI.Button();
            btnAddProperty = new AntdUI.Button();
            lblBreadcrumb = new Label();
            pnlMain = new Panel();
            pnlBottomContent = new Panel();
            pnlBookingRequests = new AntdUI.Panel();
            flowBookingRequests = new FlowLayoutPanel();
            lblReqHeader = new Label();
            lblReqCount = new AntdUI.Button();
            pnlRecentProperties = new AntdUI.Panel();
            flowRecentProps = new FlowLayoutPanel();
            pnlRecentHeader = new Panel();
            btnViewAll = new Label();
            lblRecentTitle = new Label();
            flowStats = new FlowLayoutPanel();
            cardProperties = new AntdUI.Panel();
            lblPropDelta = new AntdUI.Button();
            lblPropValue = new Label();
            lblPropTitle = new Label();
            iconProp = new AntdUI.Button();
            cardBookings = new AntdUI.Panel();
            lblBookingsDelta = new AntdUI.Button();
            lblBookingsValue = new Label();
            lblBookingsTitle = new Label();
            iconBookings = new AntdUI.Button();
            cardEarnings = new AntdUI.Panel();
            lblEarningsDelta = new AntdUI.Button();
            lblEarningsValue = new Label();
            lblEarningsTitle = new Label();
            iconEarnings = new AntdUI.Button();
            lblPageSubtitle = new Label();
            lblPageTitle = new Label();
            pnlSidebar.SuspendLayout();
            pnlSidebarBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picUser).BeginInit();
            pnlBrand.SuspendLayout();
            pnlTopBar.SuspendLayout();
            pnlMain.SuspendLayout();
            pnlBottomContent.SuspendLayout();
            pnlBookingRequests.SuspendLayout();
            pnlRecentProperties.SuspendLayout();
            pnlRecentHeader.SuspendLayout();
            flowStats.SuspendLayout();
            cardProperties.SuspendLayout();
            cardBookings.SuspendLayout();
            cardEarnings.SuspendLayout();
            SuspendLayout();
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = Color.White;
            pnlSidebar.Controls.Add(pnlSidebarBottom);
            pnlSidebar.Controls.Add(btnSettings);
            pnlSidebar.Controls.Add(btnTenants);
            pnlSidebar.Controls.Add(btnPayments);
            pnlSidebar.Controls.Add(btnBookings);
            pnlSidebar.Controls.Add(btnProperties);
            pnlSidebar.Controls.Add(btnDashboard);
            pnlSidebar.Controls.Add(pnlBrand);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Location = new Point(0, 0);
            pnlSidebar.Margin = new Padding(4, 5, 4, 5);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(371, 1226);
            pnlSidebar.TabIndex = 0;
            // 
            // pnlSidebarBottom
            // 
            pnlSidebarBottom.Controls.Add(lblUserEmail);
            pnlSidebarBottom.Controls.Add(lblUserName);
            pnlSidebarBottom.Controls.Add(picUser);
            pnlSidebarBottom.Dock = DockStyle.Bottom;
            pnlSidebarBottom.Location = new Point(0, 1093);
            pnlSidebarBottom.Margin = new Padding(4, 5, 4, 5);
            pnlSidebarBottom.Name = "pnlSidebarBottom";
            pnlSidebarBottom.Size = new Size(371, 133);
            pnlSidebarBottom.TabIndex = 7;
            // 
            // lblUserEmail
            // 
            lblUserEmail.AutoSize = true;
            lblUserEmail.Font = new Font("Segoe UI", 8F);
            lblUserEmail.ForeColor = Color.Gray;
            lblUserEmail.Location = new Point(86, 67);
            lblUserEmail.Margin = new Padding(4, 0, 4, 0);
            lblUserEmail.Name = "lblUserEmail";
            lblUserEmail.Size = new Size(156, 21);
            lblUserEmail.TabIndex = 2;
            lblUserEmail.Text = "marcus@rentals.com";
            // 
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblUserName.Location = new Point(86, 33);
            lblUserName.Margin = new Padding(4, 0, 4, 0);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(135, 25);
            lblUserName.TabIndex = 0;
            lblUserName.Text = "Marcus Admin";
            // 
            // picUser
            // 
            picUser.Location = new Point(17, 33);
            picUser.Margin = new Padding(4, 5, 4, 5);
            picUser.Name = "picUser";
            picUser.Size = new Size(57, 67);
            picUser.TabIndex = 0;
            picUser.TabStop = false;
            // 
            // btnSettings
            // 
            btnSettings.BackActive = Color.Empty;
            btnSettings.BackColor = Color.Transparent;
            btnSettings.Dock = DockStyle.Top;
            btnSettings.Font = new Font("Segoe UI", 10F);
            btnSettings.ForeColor = Color.FromArgb(64, 64, 64);
            btnSettings.Location = new Point(0, 515);
            btnSettings.Margin = new Padding(4, 5, 4, 5);
            btnSettings.Name = "btnSettings";
            btnSettings.Padding = new Padding(29, 0, 0, 0);
            btnSettings.Radius = 0;
            btnSettings.Size = new Size(371, 83);
            btnSettings.TabIndex = 6;
            btnSettings.Text = "Settings";
            // 
            // btnTenants
            // 
            btnTenants.BackActive = Color.Empty;
            btnTenants.BackColor = Color.Transparent;
            btnTenants.Dock = DockStyle.Top;
            btnTenants.Font = new Font("Segoe UI", 10F);
            btnTenants.ForeColor = Color.FromArgb(64, 64, 64);
            btnTenants.Location = new Point(0, 432);
            btnTenants.Margin = new Padding(4, 5, 4, 5);
            btnTenants.Name = "btnTenants";
            btnTenants.Padding = new Padding(29, 0, 0, 0);
            btnTenants.Radius = 0;
            btnTenants.Size = new Size(371, 83);
            btnTenants.TabIndex = 5;
            btnTenants.Text = "Tenants";
            // 
            // btnPayments
            // 
            btnPayments.BackActive = Color.Empty;
            btnPayments.BackColor = Color.Transparent;
            btnPayments.Dock = DockStyle.Top;
            btnPayments.Font = new Font("Segoe UI", 10F);
            btnPayments.ForeColor = Color.FromArgb(64, 64, 64);
            btnPayments.Location = new Point(0, 349);
            btnPayments.Margin = new Padding(4, 5, 4, 5);
            btnPayments.Name = "btnPayments";
            btnPayments.Padding = new Padding(29, 0, 0, 0);
            btnPayments.Radius = 0;
            btnPayments.Size = new Size(371, 83);
            btnPayments.TabIndex = 4;
            btnPayments.Text = "Payments";
            // 
            // btnBookings
            // 
            btnBookings.BackActive = Color.Empty;
            btnBookings.BackColor = Color.Transparent;
            btnBookings.Dock = DockStyle.Top;
            btnBookings.Font = new Font("Segoe UI", 10F);
            btnBookings.ForeColor = Color.FromArgb(64, 64, 64);
            btnBookings.Location = new Point(0, 266);
            btnBookings.Margin = new Padding(4, 5, 4, 5);
            btnBookings.Name = "btnBookings";
            btnBookings.Padding = new Padding(29, 0, 0, 0);
            btnBookings.Radius = 0;
            btnBookings.Size = new Size(371, 83);
            btnBookings.TabIndex = 3;
            btnBookings.Text = "Bookings";
            // 
            // btnProperties
            // 
            btnProperties.BackActive = Color.Empty;
            btnProperties.BackColor = Color.Transparent;
            btnProperties.Dock = DockStyle.Top;
            btnProperties.Font = new Font("Segoe UI", 10F);
            btnProperties.ForeColor = Color.FromArgb(64, 64, 64);
            btnProperties.Location = new Point(0, 183);
            btnProperties.Margin = new Padding(4, 5, 4, 5);
            btnProperties.Name = "btnProperties";
            btnProperties.Padding = new Padding(29, 0, 0, 0);
            btnProperties.Radius = 0;
            btnProperties.Size = new Size(371, 83);
            btnProperties.TabIndex = 2;
            btnProperties.Text = "Properties";
            // 
            // btnDashboard
            // 
            btnDashboard.BackActive = Color.FromArgb(233, 242, 255);
            btnDashboard.BackColor = Color.FromArgb(233, 242, 255);
            btnDashboard.Dock = DockStyle.Top;
            btnDashboard.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnDashboard.ForeColor = Color.FromArgb(24, 144, 255);
            btnDashboard.Location = new Point(0, 133);
            btnDashboard.Margin = new Padding(4, 5, 4, 5);
            btnDashboard.Name = "btnDashboard";
            btnDashboard.Padding = new Padding(29, 0, 0, 0);
            btnDashboard.Radius = 0;
            btnDashboard.Size = new Size(371, 50);
            btnDashboard.TabIndex = 1;
            btnDashboard.Text = "Dashboard";
            // 
            // pnlBrand
            // 
            pnlBrand.Controls.Add(lblBrandSubtitle);
            pnlBrand.Controls.Add(lblBrandTitle);
            pnlBrand.Dock = DockStyle.Top;
            pnlBrand.Location = new Point(0, 0);
            pnlBrand.Margin = new Padding(4, 5, 4, 5);
            pnlBrand.Name = "pnlBrand";
            pnlBrand.Size = new Size(371, 133);
            pnlBrand.TabIndex = 0;
            // 
            // lblBrandSubtitle
            // 
            lblBrandSubtitle.AutoSize = true;
            lblBrandSubtitle.Font = new Font("Segoe UI", 8F);
            lblBrandSubtitle.ForeColor = Color.Gray;
            lblBrandSubtitle.Location = new Point(86, 75);
            lblBrandSubtitle.Margin = new Padding(4, 0, 4, 0);
            lblBrandSubtitle.Name = "lblBrandSubtitle";
            lblBrandSubtitle.Size = new Size(136, 21);
            lblBrandSubtitle.TabIndex = 1;
            lblBrandSubtitle.Text = "Admin Dashboard";
            // 
            // lblBrandTitle
            // 
            lblBrandTitle.AutoSize = true;
            lblBrandTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblBrandTitle.Location = new Point(86, 33);
            lblBrandTitle.Margin = new Padding(4, 0, 4, 0);
            lblBrandTitle.Name = "lblBrandTitle";
            lblBrandTitle.Size = new Size(192, 32);
            lblBrandTitle.TabIndex = 0;
            lblBrandTitle.Text = "Landlord Portal";
            // 
            // pnlTopBar
            // 
            pnlTopBar.BackColor = Color.White;
            pnlTopBar.Controls.Add(btnClose);
            pnlTopBar.Controls.Add(btnMinimize);
            pnlTopBar.Controls.Add(btnNotif);
            pnlTopBar.Controls.Add(btnAddProperty);
            pnlTopBar.Controls.Add(lblBreadcrumb);
            pnlTopBar.Dock = DockStyle.Top;
            pnlTopBar.Location = new Point(371, 0);
            pnlTopBar.Margin = new Padding(4, 5, 4, 5);
            pnlTopBar.Name = "pnlTopBar";
            pnlTopBar.Padding = new Padding(29, 33, 29, 33);
            pnlTopBar.Size = new Size(1343, 117);
            pnlTopBar.TabIndex = 1;
            // 
            // btnClose
            // 
            btnClose.BackHover = Color.Red;
            btnClose.Cursor = Cursors.Hand;
            btnClose.Dock = DockStyle.Right;
            btnClose.Font = new Font("Segoe UI", 11F);
            btnClose.ForeHover = Color.White;
            btnClose.Location = new Point(929, 33);
            btnClose.Margin = new Padding(4, 5, 4, 5);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(57, 51);
            btnClose.TabIndex = 10;
            btnClose.Text = "✕";
            // 
            // btnMinimize
            // 
            btnMinimize.BackHover = Color.FromArgb(240, 240, 240);
            btnMinimize.Cursor = Cursors.Hand;
            btnMinimize.Dock = DockStyle.Right;
            btnMinimize.Font = new Font("Segoe UI", 11F);
            btnMinimize.Location = new Point(986, 33);
            btnMinimize.Margin = new Padding(4, 5, 4, 5);
            btnMinimize.Name = "btnMinimize";
            btnMinimize.Size = new Size(57, 51);
            btnMinimize.TabIndex = 9;
            btnMinimize.Text = "─";
            // 
            // btnNotif
            // 
            btnNotif.BackActive = Color.Empty;
            btnNotif.BackColor = Color.Transparent;
            btnNotif.Dock = DockStyle.Right;
            btnNotif.Location = new Point(1043, 33);
            btnNotif.Margin = new Padding(4, 5, 4, 5);
            btnNotif.Name = "btnNotif";
            btnNotif.Size = new Size(71, 51);
            btnNotif.TabIndex = 1;
            // 
            // btnAddProperty
            // 
            btnAddProperty.BackColor = Color.FromArgb(24, 144, 255);
            btnAddProperty.Dock = DockStyle.Right;
            btnAddProperty.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAddProperty.ForeColor = Color.White;
            btnAddProperty.Location = new Point(1114, 33);
            btnAddProperty.Margin = new Padding(4, 5, 4, 5);
            btnAddProperty.Name = "btnAddProperty";
            btnAddProperty.Size = new Size(200, 51);
            btnAddProperty.TabIndex = 2;
            btnAddProperty.Text = "+ Add Property";
            btnAddProperty.Type = AntdUI.TTypeMini.Primary;
            // 
            // lblBreadcrumb
            // 
            lblBreadcrumb.AutoSize = true;
            lblBreadcrumb.Font = new Font("Segoe UI", 9F);
            lblBreadcrumb.ForeColor = Color.Gray;
            lblBreadcrumb.Location = new Point(29, 47);
            lblBreadcrumb.Margin = new Padding(4, 0, 4, 0);
            lblBreadcrumb.Name = "lblBreadcrumb";
            lblBreadcrumb.Size = new Size(166, 25);
            lblBreadcrumb.TabIndex = 0;
            lblBreadcrumb.Text = "Home / Dashboard";
            // 
            // pnlMain
            // 
            pnlMain.AutoScroll = true;
            pnlMain.BackColor = Color.FromArgb(248, 248, 248);
            pnlMain.Controls.Add(pnlBottomContent);
            pnlMain.Controls.Add(flowStats);
            pnlMain.Controls.Add(lblPageSubtitle);
            pnlMain.Controls.Add(lblPageTitle);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(371, 117);
            pnlMain.Margin = new Padding(4, 5, 4, 5);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(43, 33, 43, 33);
            pnlMain.Size = new Size(1343, 1109);
            pnlMain.TabIndex = 2;
            // 
            // pnlBottomContent
            // 
            pnlBottomContent.Controls.Add(pnlBookingRequests);
            pnlBottomContent.Controls.Add(pnlRecentProperties);
            pnlBottomContent.Dock = DockStyle.Top;
            pnlBottomContent.Location = new Point(43, 576);
            pnlBottomContent.Margin = new Padding(4, 5, 4, 5);
            pnlBottomContent.Name = "pnlBottomContent";
            pnlBottomContent.Padding = new Padding(0, 33, 0, 0);
            pnlBottomContent.Size = new Size(1231, 833);
            pnlBottomContent.TabIndex = 3;
            // 
            // pnlBookingRequests
            // 
            pnlBookingRequests.BackColor = Color.Transparent;
            pnlBookingRequests.Controls.Add(flowBookingRequests);
            pnlBookingRequests.Controls.Add(lblReqHeader);
            pnlBookingRequests.Controls.Add(lblReqCount);
            pnlBookingRequests.Dock = DockStyle.Right;
            pnlBookingRequests.Location = new Point(760, 33);
            pnlBookingRequests.Margin = new Padding(4, 5, 4, 5);
            pnlBookingRequests.Name = "pnlBookingRequests";
            pnlBookingRequests.Size = new Size(471, 800);
            pnlBookingRequests.TabIndex = 1;
            // 
            // flowBookingRequests
            // 
            flowBookingRequests.AutoScroll = true;
            flowBookingRequests.Dock = DockStyle.Fill;
            flowBookingRequests.Location = new Point(0, 0);
            flowBookingRequests.Margin = new Padding(4, 5, 4, 5);
            flowBookingRequests.Name = "flowBookingRequests";
            flowBookingRequests.Size = new Size(471, 800);
            flowBookingRequests.TabIndex = 2;
            // 
            // lblReqHeader
            // 
            lblReqHeader.AutoSize = true;
            lblReqHeader.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblReqHeader.Location = new Point(0, 0);
            lblReqHeader.Margin = new Padding(4, 0, 4, 0);
            lblReqHeader.Name = "lblReqHeader";
            lblReqHeader.Size = new Size(218, 32);
            lblReqHeader.TabIndex = 0;
            lblReqHeader.Text = "Booking Requests";
            // 
            // lblReqCount
            // 
            lblReqCount.BackColor = Color.FromArgb(255, 87, 87);
            lblReqCount.Location = new Point(400, 0);
            lblReqCount.Margin = new Padding(4, 5, 4, 5);
            lblReqCount.Name = "lblReqCount";
            lblReqCount.Size = new Size(71, 37);
            lblReqCount.TabIndex = 1;
            lblReqCount.Text = "2 New";
            // 
            // pnlRecentProperties
            // 
            pnlRecentProperties.BackColor = Color.White;
            pnlRecentProperties.Controls.Add(flowRecentProps);
            pnlRecentProperties.Controls.Add(pnlRecentHeader);
            pnlRecentProperties.Dock = DockStyle.Left;
            pnlRecentProperties.Location = new Point(0, 33);
            pnlRecentProperties.Margin = new Padding(0, 0, 29, 0);
            pnlRecentProperties.Name = "pnlRecentProperties";
            pnlRecentProperties.Padding = new Padding(14, 17, 14, 17);
            pnlRecentProperties.Radius = 12;
            pnlRecentProperties.Shadow = 5;
            pnlRecentProperties.Size = new Size(757, 800);
            pnlRecentProperties.TabIndex = 0;
            // 
            // flowRecentProps
            // 
            flowRecentProps.AutoScroll = true;
            flowRecentProps.Dock = DockStyle.Fill;
            flowRecentProps.Location = new Point(21, 91);
            flowRecentProps.Margin = new Padding(4, 5, 4, 5);
            flowRecentProps.Name = "flowRecentProps";
            flowRecentProps.Size = new Size(715, 685);
            flowRecentProps.TabIndex = 1;
            // 
            // pnlRecentHeader
            // 
            pnlRecentHeader.Controls.Add(btnViewAll);
            pnlRecentHeader.Controls.Add(lblRecentTitle);
            pnlRecentHeader.Dock = DockStyle.Top;
            pnlRecentHeader.Location = new Point(21, 24);
            pnlRecentHeader.Margin = new Padding(4, 5, 4, 5);
            pnlRecentHeader.Name = "pnlRecentHeader";
            pnlRecentHeader.Size = new Size(715, 67);
            pnlRecentHeader.TabIndex = 0;
            // 
            // btnViewAll
            // 
            btnViewAll.AutoSize = true;
            btnViewAll.Cursor = Cursors.Hand;
            btnViewAll.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnViewAll.ForeColor = Color.FromArgb(24, 144, 255);
            btnViewAll.Location = new Point(629, 8);
            btnViewAll.Margin = new Padding(4, 0, 4, 0);
            btnViewAll.Name = "btnViewAll";
            btnViewAll.Size = new Size(81, 25);
            btnViewAll.TabIndex = 1;
            btnViewAll.Text = "View All";
            // 
            // lblRecentTitle
            // 
            lblRecentTitle.AutoSize = true;
            lblRecentTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblRecentTitle.Location = new Point(0, 0);
            lblRecentTitle.Margin = new Padding(4, 0, 4, 0);
            lblRecentTitle.Name = "lblRecentTitle";
            lblRecentTitle.Size = new Size(217, 32);
            lblRecentTitle.TabIndex = 0;
            lblRecentTitle.Text = "Recent Properties";
            // 
            // flowStats
            // 
            flowStats.Controls.Add(cardProperties);
            flowStats.Controls.Add(cardBookings);
            flowStats.Controls.Add(cardEarnings);
            flowStats.Dock = DockStyle.Top;
            flowStats.Location = new Point(43, 126);
            flowStats.Margin = new Padding(4, 5, 4, 5);
            flowStats.Name = "flowStats";
            flowStats.Size = new Size(1231, 450);
            flowStats.TabIndex = 2;
            // 
            // cardProperties
            // 
            cardProperties.BackColor = Color.White;
            cardProperties.Controls.Add(lblPropDelta);
            cardProperties.Controls.Add(lblPropValue);
            cardProperties.Controls.Add(lblPropTitle);
            cardProperties.Controls.Add(iconProp);
            cardProperties.Location = new Point(29, 33);
            cardProperties.Margin = new Padding(29, 33, 29, 33);
            cardProperties.Name = "cardProperties";
            cardProperties.Radius = 12;
            cardProperties.Shadow = 8;
            cardProperties.Size = new Size(371, 333);
            cardProperties.TabIndex = 0;
            // 
            // lblPropDelta
            // 
            lblPropDelta.BackColor = Color.FromArgb(230, 255, 230);
            lblPropDelta.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblPropDelta.ForeColor = Color.Green;
            lblPropDelta.Location = new Point(114, 233);
            lblPropDelta.Margin = new Padding(4, 5, 4, 5);
            lblPropDelta.Name = "lblPropDelta";
            lblPropDelta.Radius = 12;
            lblPropDelta.Size = new Size(86, 40);
            lblPropDelta.TabIndex = 3;
            lblPropDelta.Text = "+2 new";
            // 
            // lblPropValue
            // 
            lblPropValue.AutoSize = true;
            lblPropValue.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblPropValue.Location = new Point(29, 217);
            lblPropValue.Margin = new Padding(4, 0, 4, 0);
            lblPropValue.Name = "lblPropValue";
            lblPropValue.Size = new Size(84, 65);
            lblPropValue.TabIndex = 2;
            lblPropValue.Text = "12";
            // 
            // lblPropTitle
            // 
            lblPropTitle.AutoSize = true;
            lblPropTitle.Font = new Font("Segoe UI", 10F);
            lblPropTitle.ForeColor = Color.Gray;
            lblPropTitle.Location = new Point(29, 33);
            lblPropTitle.Margin = new Padding(4, 0, 4, 0);
            lblPropTitle.Name = "lblPropTitle";
            lblPropTitle.Size = new Size(148, 28);
            lblPropTitle.TabIndex = 1;
            lblPropTitle.Text = "Total Properties";
            // 
            // iconProp
            // 
            iconProp.BackColor = Color.FromArgb(230, 247, 255);
            iconProp.ForeColor = Color.FromArgb(24, 144, 255);
            iconProp.Location = new Point(286, 33);
            iconProp.Margin = new Padding(4, 5, 4, 5);
            iconProp.Name = "iconProp";
            iconProp.Radius = 8;
            iconProp.Size = new Size(57, 67);
            iconProp.TabIndex = 0;
            // 
            // cardBookings
            // 
            cardBookings.BackColor = Color.White;
            cardBookings.Controls.Add(lblBookingsDelta);
            cardBookings.Controls.Add(lblBookingsValue);
            cardBookings.Controls.Add(lblBookingsTitle);
            cardBookings.Controls.Add(iconBookings);
            cardBookings.Location = new Point(458, 33);
            cardBookings.Margin = new Padding(29, 33, 29, 33);
            cardBookings.Name = "cardBookings";
            cardBookings.Radius = 12;
            cardBookings.Shadow = 8;
            cardBookings.Size = new Size(374, 333);
            cardBookings.TabIndex = 1;
            // 
            // lblBookingsDelta
            // 
            lblBookingsDelta.BackColor = Color.FromArgb(230, 255, 230);
            lblBookingsDelta.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblBookingsDelta.ForeColor = Color.Green;
            lblBookingsDelta.Location = new Point(86, 233);
            lblBookingsDelta.Margin = new Padding(4, 5, 4, 5);
            lblBookingsDelta.Name = "lblBookingsDelta";
            lblBookingsDelta.Radius = 12;
            lblBookingsDelta.Size = new Size(114, 40);
            lblBookingsDelta.TabIndex = 3;
            lblBookingsDelta.Text = "+1 this week";
            // 
            // lblBookingsValue
            // 
            lblBookingsValue.AutoSize = true;
            lblBookingsValue.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblBookingsValue.Location = new Point(29, 217);
            lblBookingsValue.Margin = new Padding(4, 0, 4, 0);
            lblBookingsValue.Name = "lblBookingsValue";
            lblBookingsValue.Size = new Size(56, 65);
            lblBookingsValue.TabIndex = 2;
            lblBookingsValue.Text = "8";
            // 
            // lblBookingsTitle
            // 
            lblBookingsTitle.AutoSize = true;
            lblBookingsTitle.Font = new Font("Segoe UI", 10F);
            lblBookingsTitle.ForeColor = Color.Gray;
            lblBookingsTitle.Location = new Point(29, 33);
            lblBookingsTitle.Margin = new Padding(4, 0, 4, 0);
            lblBookingsTitle.Name = "lblBookingsTitle";
            lblBookingsTitle.Size = new Size(152, 28);
            lblBookingsTitle.TabIndex = 1;
            lblBookingsTitle.Text = "Active Bookings";
            // 
            // iconBookings
            // 
            iconBookings.BackColor = Color.FromArgb(248, 235, 255);
            iconBookings.ForeColor = Color.Purple;
            iconBookings.Location = new Point(286, 33);
            iconBookings.Margin = new Padding(4, 5, 4, 5);
            iconBookings.Name = "iconBookings";
            iconBookings.Radius = 8;
            iconBookings.Size = new Size(57, 67);
            iconBookings.TabIndex = 0;
            // 
            // cardEarnings
            // 
            cardEarnings.BackColor = Color.White;
            cardEarnings.Controls.Add(lblEarningsDelta);
            cardEarnings.Controls.Add(lblEarningsValue);
            cardEarnings.Controls.Add(lblEarningsTitle);
            cardEarnings.Controls.Add(iconEarnings);
            cardEarnings.Location = new Point(29, 432);
            cardEarnings.Margin = new Padding(29, 33, 29, 33);
            cardEarnings.Name = "cardEarnings";
            cardEarnings.Radius = 12;
            cardEarnings.Shadow = 8;
            cardEarnings.Size = new Size(371, 333);
            cardEarnings.TabIndex = 2;
            // 
            // lblEarningsDelta
            // 
            lblEarningsDelta.BackColor = Color.FromArgb(230, 255, 230);
            lblEarningsDelta.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblEarningsDelta.ForeColor = Color.Green;
            lblEarningsDelta.Location = new Point(186, 233);
            lblEarningsDelta.Margin = new Padding(4, 5, 4, 5);
            lblEarningsDelta.Name = "lblEarningsDelta";
            lblEarningsDelta.Radius = 12;
            lblEarningsDelta.Size = new Size(143, 40);
            lblEarningsDelta.TabIndex = 3;
            lblEarningsDelta.Text = "+12% vs last mo";
            // 
            // lblEarningsValue
            // 
            lblEarningsValue.AutoSize = true;
            lblEarningsValue.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblEarningsValue.Location = new Point(29, 217);
            lblEarningsValue.Margin = new Padding(4, 0, 4, 0);
            lblEarningsValue.Name = "lblEarningsValue";
            lblEarningsValue.Size = new Size(209, 65);
            lblEarningsValue.TabIndex = 2;
            lblEarningsValue.Text = "$24,500";
            // 
            // lblEarningsTitle
            // 
            lblEarningsTitle.AutoSize = true;
            lblEarningsTitle.Font = new Font("Segoe UI", 10F);
            lblEarningsTitle.ForeColor = Color.Gray;
            lblEarningsTitle.Location = new Point(29, 33);
            lblEarningsTitle.Margin = new Padding(4, 0, 4, 0);
            lblEarningsTitle.Name = "lblEarningsTitle";
            lblEarningsTitle.Size = new Size(165, 28);
            lblEarningsTitle.TabIndex = 1;
            lblEarningsTitle.Text = "Monthly Earnings";
            // 
            // iconEarnings
            // 
            iconEarnings.BackColor = Color.FromArgb(255, 250, 230);
            iconEarnings.ForeColor = Color.Orange;
            iconEarnings.Location = new Point(286, 33);
            iconEarnings.Margin = new Padding(4, 5, 4, 5);
            iconEarnings.Name = "iconEarnings";
            iconEarnings.Radius = 8;
            iconEarnings.Size = new Size(57, 67);
            iconEarnings.TabIndex = 0;
            iconEarnings.Text = "$";
            // 
            // lblPageSubtitle
            // 
            lblPageSubtitle.AutoSize = true;
            lblPageSubtitle.Dock = DockStyle.Top;
            lblPageSubtitle.Font = new Font("Segoe UI", 10F);
            lblPageSubtitle.ForeColor = Color.Gray;
            lblPageSubtitle.Location = new Point(43, 98);
            lblPageSubtitle.Margin = new Padding(4, 0, 4, 0);
            lblPageSubtitle.Name = "lblPageSubtitle";
            lblPageSubtitle.Size = new Size(372, 28);
            lblPageSubtitle.TabIndex = 1;
            lblPageSubtitle.Text = "Overview of your properties and requests";
            // 
            // lblPageTitle
            // 
            lblPageTitle.AutoSize = true;
            lblPageTitle.Dock = DockStyle.Top;
            lblPageTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblPageTitle.Location = new Point(43, 33);
            lblPageTitle.Margin = new Padding(4, 0, 4, 0);
            lblPageTitle.Name = "lblPageTitle";
            lblPageTitle.Size = new Size(273, 65);
            lblPageTitle.TabIndex = 0;
            lblPageTitle.Text = "Dashboard";
            // 
            // HomeownerDashboard
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1714, 1226);
            Controls.Add(pnlMain);
            Controls.Add(pnlTopBar);
            Controls.Add(pnlSidebar);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "HomeownerDashboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "HomeownerDashboard";
            pnlSidebar.ResumeLayout(false);
            pnlSidebarBottom.ResumeLayout(false);
            pnlSidebarBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picUser).EndInit();
            pnlBrand.ResumeLayout(false);
            pnlBrand.PerformLayout();
            pnlTopBar.ResumeLayout(false);
            pnlTopBar.PerformLayout();
            pnlMain.ResumeLayout(false);
            pnlMain.PerformLayout();
            pnlBottomContent.ResumeLayout(false);
            pnlBookingRequests.ResumeLayout(false);
            pnlBookingRequests.PerformLayout();
            pnlRecentProperties.ResumeLayout(false);
            pnlRecentHeader.ResumeLayout(false);
            pnlRecentHeader.PerformLayout();
            flowStats.ResumeLayout(false);
            cardProperties.ResumeLayout(false);
            cardProperties.PerformLayout();
            cardBookings.ResumeLayout(false);
            cardBookings.PerformLayout();
            cardEarnings.ResumeLayout(false);
            cardEarnings.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Panel pnlBrand;
        private System.Windows.Forms.Label lblBrandTitle;
        private System.Windows.Forms.Label lblBrandSubtitle;
        private AntdUI.Button btnDashboard;
        private AntdUI.Button btnSettings;
        private AntdUI.Button btnTenants;
        private AntdUI.Button btnPayments;
        private AntdUI.Button btnBookings;
        private AntdUI.Button btnProperties;
        private System.Windows.Forms.Panel pnlTopBar;
        private System.Windows.Forms.Label lblBreadcrumb;
        private AntdUI.Button btnNotif;
        private AntdUI.Button btnAddProperty;
        private AntdUI.Button btnClose;
        private AntdUI.Button btnMinimize;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.Label lblPageSubtitle;
        private System.Windows.Forms.FlowLayoutPanel flowStats;
        private AntdUI.Panel cardProperties;
        private AntdUI.Button iconProp;
        private System.Windows.Forms.Label lblPropTitle;
        private System.Windows.Forms.Label lblPropValue;
        private AntdUI.Button lblPropDelta;
        private AntdUI.Panel cardBookings;
        private AntdUI.Button lblBookingsDelta;
        private System.Windows.Forms.Label lblBookingsValue;
        private System.Windows.Forms.Label lblBookingsTitle;
        private AntdUI.Button iconBookings;
        private AntdUI.Panel cardEarnings;
        private AntdUI.Button lblEarningsDelta;
        private System.Windows.Forms.Label lblEarningsValue;
        private System.Windows.Forms.Label lblEarningsTitle;
        private AntdUI.Button iconEarnings;
        private System.Windows.Forms.Panel pnlBottomContent;
        private AntdUI.Panel pnlRecentProperties;
        private System.Windows.Forms.Panel pnlRecentHeader;
        private System.Windows.Forms.Label lblRecentTitle;
        private System.Windows.Forms.Label btnViewAll;
        private System.Windows.Forms.FlowLayoutPanel flowRecentProps;
        private AntdUI.Panel pnlBookingRequests;
        private System.Windows.Forms.Label lblReqHeader;
        private AntdUI.Button lblReqCount;
        private System.Windows.Forms.FlowLayoutPanel flowBookingRequests;
        private System.Windows.Forms.Panel pnlSidebarBottom;
        private System.Windows.Forms.Label lblUserEmail;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.PictureBox picUser;
    }
}
