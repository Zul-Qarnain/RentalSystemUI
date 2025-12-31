namespace RentalSystemUI.Forms
{
    partial class RentAllSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RentAllSearch));
            pnlTopBar = new Panel();
            lblLogo = new Label();
            btnHeart = new AntdUI.Button();
            btnNotif = new AntdUI.Button();
            btnMenuMessages = new AntdUI.Button();
            btnMenuRentals = new AntdUI.Button();
            btnMenuHome = new AntdUI.Button();
            pnlSidebar = new Panel();
            inputSqFt = new AntdUI.InputNumber();
            lblSqFt = new Label();
            selKitchen = new AntdUI.Select();
            lblKitchen = new Label();
            selCorridor = new AntdUI.Select();
            lblCorridor = new Label();
            selBathroom = new AntdUI.Select();
            lblBathroom = new Label();
            selBedroom = new AntdUI.Select();
            lblBedroom = new Label();
            chkParking = new AntdUI.Checkbox();
            chkPet = new AntdUI.Checkbox();
            chkWasher = new AntdUI.Checkbox();
            chkAC = new AntdUI.Checkbox();
            lblAmenities = new Label();
            sliderRent = new AntdUI.Slider();
            lblRentValue = new Label();
            lblRentTitle = new Label();
            btnResetFilters = new Label();
            lblFilterTitle = new Label();
            pnlMainContent = new Panel();
            flowListings = new FlowLayoutPanel();
            pnlSearchHeader = new Panel();
            lblResultCount = new Label();
            btnFilterMore = new AntdUI.Button();
            btnFilterAmenities = new AntdUI.Button();
            btnFilterBeds = new AntdUI.Button();
            btnFilterHomeType = new AntdUI.Button();
            btnSearch = new AntdUI.Button();
            txtSearch = new AntdUI.Input();
            pnlDetailsHost = new Panel();
            pnlTopBar.SuspendLayout();
            pnlSidebar.SuspendLayout();
            pnlMainContent.SuspendLayout();
            pnlSearchHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTopBar
            // 
            pnlTopBar.BackColor = Color.White;
            pnlTopBar.Controls.Add(lblLogo);
            pnlTopBar.Controls.Add(btnHeart);
            pnlTopBar.Controls.Add(btnNotif);
            pnlTopBar.Controls.Add(btnMenuMessages);
            pnlTopBar.Controls.Add(btnMenuRentals);
            pnlTopBar.Controls.Add(btnMenuHome);
            pnlTopBar.Dock = DockStyle.Top;
            pnlTopBar.Location = new Point(0, 0);
            pnlTopBar.Name = "pnlTopBar";
            pnlTopBar.Size = new Size(1400, 80);
            pnlTopBar.TabIndex = 2;
            // 
            // lblLogo
            // 
            lblLogo.AutoSize = true;
            lblLogo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblLogo.ForeColor = Color.FromArgb(255, 90, 31);
            lblLogo.Location = new Point(30, 20);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(143, 48);
            lblLogo.TabIndex = 0;
            lblLogo.Text = "RentAll";
            // 
            // btnHeart
            // 
            btnHeart.Location = new Point(1250, 25);
            btnHeart.Name = "btnHeart";
            btnHeart.Size = new Size(40, 32);
            btnHeart.TabIndex = 2;
            // 
            // btnNotif
            // 
            btnNotif.Location = new Point(1200, 25);
            btnNotif.Name = "btnNotif";
            btnNotif.Size = new Size(40, 32);
            btnNotif.TabIndex = 3;
            // 
            // btnMenuMessages
            // 
            btnMenuMessages.Icon = (Image)resources.GetObject("btnMenuMessages.Icon");
            btnMenuMessages.IconSize = new Size(18, 18);
            btnMenuMessages.Location = new Point(765, 36);
            btnMenuMessages.Name = "btnMenuMessages";
            btnMenuMessages.Size = new Size(120, 44);
            btnMenuMessages.TabIndex = 4;
            // 
            // btnMenuRentals
            // 
            btnMenuRentals.Icon = (Image)resources.GetObject("btnMenuRentals.Icon");
            btnMenuRentals.IconSize = new Size(18, 18);
            btnMenuRentals.Location = new Point(621, 30);
            btnMenuRentals.Name = "btnMenuRentals";
            btnMenuRentals.Size = new Size(150, 50);
            btnMenuRentals.TabIndex = 5;
            // 
            // btnMenuHome
            // 
            btnMenuHome.Icon = (Image)resources.GetObject("btnMenuHome.Icon");
            btnMenuHome.IconSize = new Size(18, 18);
            btnMenuHome.Location = new Point(524, 30);
            btnMenuHome.Name = "btnMenuHome";
            btnMenuHome.Size = new Size(120, 44);
            btnMenuHome.TabIndex = 6;
            btnMenuHome.Click += btnMenuHome_Click;
            // 
            // pnlSidebar
            // 
            pnlSidebar.AutoScroll = true;
            pnlSidebar.BackColor = Color.White;
            pnlSidebar.Controls.Add(inputSqFt);
            pnlSidebar.Controls.Add(lblSqFt);
            pnlSidebar.Controls.Add(selKitchen);
            pnlSidebar.Controls.Add(lblKitchen);
            pnlSidebar.Controls.Add(selCorridor);
            pnlSidebar.Controls.Add(lblCorridor);
            pnlSidebar.Controls.Add(selBathroom);
            pnlSidebar.Controls.Add(lblBathroom);
            pnlSidebar.Controls.Add(selBedroom);
            pnlSidebar.Controls.Add(lblBedroom);
            pnlSidebar.Controls.Add(chkParking);
            pnlSidebar.Controls.Add(chkPet);
            pnlSidebar.Controls.Add(chkWasher);
            pnlSidebar.Controls.Add(chkAC);
            pnlSidebar.Controls.Add(lblAmenities);
            pnlSidebar.Controls.Add(sliderRent);
            pnlSidebar.Controls.Add(lblRentValue);
            pnlSidebar.Controls.Add(lblRentTitle);
            pnlSidebar.Controls.Add(btnResetFilters);
            pnlSidebar.Controls.Add(lblFilterTitle);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Location = new Point(0, 80);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Padding = new Padding(30, 20, 20, 50);
            pnlSidebar.Size = new Size(320, 870);
            pnlSidebar.TabIndex = 1;
            // 
            // inputSqFt
            // 
            inputSqFt.Location = new Point(30, 470);
            inputSqFt.Name = "inputSqFt";
            inputSqFt.PlaceholderText = "e.g. 1200";
            inputSqFt.Size = new Size(250, 35);
            inputSqFt.TabIndex = 0;
            inputSqFt.Text = "0";
            // 
            // lblSqFt
            // 
            lblSqFt.AutoSize = true;
            lblSqFt.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSqFt.Location = new Point(30, 445);
            lblSqFt.Name = "lblSqFt";
            lblSqFt.Size = new Size(180, 28);
            lblSqFt.TabIndex = 1;
            lblSqFt.Text = "Square Feet (Min)";
            // 
            // selKitchen
            // 
            selKitchen.Items.AddRange(new object[] { "Any", "1", "2" });
            selKitchen.Location = new Point(30, 400);
            selKitchen.Name = "selKitchen";
            selKitchen.PlaceholderText = "Select Number";
            selKitchen.Size = new Size(250, 35);
            selKitchen.TabIndex = 2;
            // 
            // lblKitchen
            // 
            lblKitchen.AutoSize = true;
            lblKitchen.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblKitchen.Location = new Point(30, 375);
            lblKitchen.Name = "lblKitchen";
            lblKitchen.Size = new Size(93, 28);
            lblKitchen.TabIndex = 3;
            lblKitchen.Text = "Kitchens";
            // 
            // selCorridor
            // 
            selCorridor.Items.AddRange(new object[] { "Any", "1", "2" });
            selCorridor.Location = new Point(30, 330);
            selCorridor.Name = "selCorridor";
            selCorridor.PlaceholderText = "Select Number";
            selCorridor.Size = new Size(250, 35);
            selCorridor.TabIndex = 4;
            // 
            // lblCorridor
            // 
            lblCorridor.AutoSize = true;
            lblCorridor.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCorridor.Location = new Point(30, 305);
            lblCorridor.Name = "lblCorridor";
            lblCorridor.Size = new Size(99, 28);
            lblCorridor.TabIndex = 5;
            lblCorridor.Text = "Corridors";
            // 
            // selBathroom
            // 
            selBathroom.Items.AddRange(new object[] { "Any", "1", "2", "3+" });
            selBathroom.Location = new Point(30, 260);
            selBathroom.Name = "selBathroom";
            selBathroom.PlaceholderText = "Select Number";
            selBathroom.Size = new Size(250, 35);
            selBathroom.TabIndex = 6;
            // 
            // lblBathroom
            // 
            lblBathroom.AutoSize = true;
            lblBathroom.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBathroom.Location = new Point(30, 235);
            lblBathroom.Name = "lblBathroom";
            lblBathroom.Size = new Size(122, 28);
            lblBathroom.TabIndex = 7;
            lblBathroom.Text = "Washrooms";
            // 
            // selBedroom
            // 
            selBedroom.Items.AddRange(new object[] { "Any", "1", "2", "3", "4", "5+" });
            selBedroom.Location = new Point(30, 190);
            selBedroom.Name = "selBedroom";
            selBedroom.PlaceholderText = "Select Number";
            selBedroom.Size = new Size(250, 35);
            selBedroom.TabIndex = 8;
            // 
            // lblBedroom
            // 
            lblBedroom.AutoSize = true;
            lblBedroom.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBedroom.Location = new Point(30, 165);
            lblBedroom.Name = "lblBedroom";
            lblBedroom.Size = new Size(107, 28);
            lblBedroom.TabIndex = 9;
            lblBedroom.Text = "Bedrooms";
            // 
            // chkParking
            // 
            chkParking.Location = new Point(30, 660);
            chkParking.Name = "chkParking";
            chkParking.Size = new Size(250, 30);
            chkParking.TabIndex = 10;
            chkParking.Text = "Parking Spot";
            // 
            // chkPet
            // 
            chkPet.Location = new Point(30, 625);
            chkPet.Name = "chkPet";
            chkPet.Size = new Size(250, 30);
            chkPet.TabIndex = 11;
            chkPet.Text = "Pet Friendly";
            // 
            // chkWasher
            // 
            chkWasher.Location = new Point(30, 590);
            chkWasher.Name = "chkWasher";
            chkWasher.Size = new Size(250, 30);
            chkWasher.TabIndex = 12;
            chkWasher.Text = "Washer / Dryer";
            // 
            // chkAC
            // 
            chkAC.Location = new Point(30, 555);
            chkAC.Name = "chkAC";
            chkAC.Size = new Size(250, 30);
            chkAC.TabIndex = 13;
            chkAC.Text = "Air Conditioning";
            // 
            // lblAmenities
            // 
            lblAmenities.AutoSize = true;
            lblAmenities.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblAmenities.Location = new Point(30, 525);
            lblAmenities.Name = "lblAmenities";
            lblAmenities.Size = new Size(107, 28);
            lblAmenities.TabIndex = 14;
            lblAmenities.Text = "Amenities";
            // 
            // sliderRent
            // 
            sliderRent.Fill = Color.FromArgb(255, 90, 31);
            sliderRent.Location = new Point(30, 110);
            sliderRent.Name = "sliderRent";
            sliderRent.Size = new Size(250, 30);
            sliderRent.TabIndex = 15;
            sliderRent.Value = 40;
            // 
            // lblRentValue
            // 
            lblRentValue.AutoSize = true;
            lblRentValue.ForeColor = Color.Gray;
            lblRentValue.Location = new Point(30, 140);
            lblRentValue.Name = "lblRentValue";
            lblRentValue.Size = new Size(266, 25);
            lblRentValue.TabIndex = 16;
            lblRentValue.Text = "$850                                $3,200";
            // 
            // lblRentTitle
            // 
            lblRentTitle.AutoSize = true;
            lblRentTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblRentTitle.Location = new Point(30, 80);
            lblRentTitle.Name = "lblRentTitle";
            lblRentTitle.Size = new Size(142, 28);
            lblRentTitle.TabIndex = 17;
            lblRentTitle.Text = "Monthly Rent";
            // 
            // btnResetFilters
            // 
            btnResetFilters.AutoSize = true;
            btnResetFilters.Cursor = Cursors.Hand;
            btnResetFilters.ForeColor = Color.Red;
            btnResetFilters.Location = new Point(220, 38);
            btnResetFilters.Name = "btnResetFilters";
            btnResetFilters.Size = new Size(79, 25);
            btnResetFilters.TabIndex = 18;
            btnResetFilters.Text = "Reset All";
            // 
            // lblFilterTitle
            // 
            lblFilterTitle.AutoSize = true;
            lblFilterTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblFilterTitle.Location = new Point(30, 30);
            lblFilterTitle.Name = "lblFilterTitle";
            lblFilterTitle.Size = new Size(97, 38);
            lblFilterTitle.TabIndex = 19;
            lblFilterTitle.Text = "Filters";
            // 
            // pnlMainContent
            // 
            pnlMainContent.BackColor = Color.FromArgb(248, 248, 248);
            pnlMainContent.Controls.Add(flowListings);
            pnlMainContent.Controls.Add(pnlSearchHeader);
            pnlMainContent.Dock = DockStyle.Fill;
            pnlMainContent.Location = new Point(320, 80);
            pnlMainContent.Name = "pnlMainContent";
            pnlMainContent.Size = new Size(1080, 870);
            pnlMainContent.TabIndex = 0;
            // 
            // flowListings
            // 
            flowListings.AutoScroll = true;
            flowListings.Dock = DockStyle.Fill;
            flowListings.Location = new Point(0, 160);
            flowListings.Name = "flowListings";
            flowListings.Padding = new Padding(10);
            flowListings.Size = new Size(1080, 710);
            flowListings.TabIndex = 0;
            // 
            // pnlSearchHeader
            // 
            pnlSearchHeader.BackColor = Color.FromArgb(248, 248, 248);
            pnlSearchHeader.Controls.Add(lblResultCount);
            pnlSearchHeader.Controls.Add(btnFilterMore);
            pnlSearchHeader.Controls.Add(btnFilterAmenities);
            pnlSearchHeader.Controls.Add(btnFilterBeds);
            pnlSearchHeader.Controls.Add(btnFilterHomeType);
            pnlSearchHeader.Controls.Add(btnSearch);
            pnlSearchHeader.Controls.Add(txtSearch);
            pnlSearchHeader.Dock = DockStyle.Top;
            pnlSearchHeader.Location = new Point(0, 0);
            pnlSearchHeader.Name = "pnlSearchHeader";
            pnlSearchHeader.Padding = new Padding(20);
            pnlSearchHeader.Size = new Size(1080, 160);
            pnlSearchHeader.TabIndex = 1;
            // 
            // lblResultCount
            // 
            lblResultCount.AutoSize = true;
            lblResultCount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblResultCount.Location = new Point(20, 130);
            lblResultCount.Name = "lblResultCount";
            lblResultCount.Size = new Size(347, 28);
            lblResultCount.TabIndex = 0;
            lblResultCount.Text = "142 properties in San Francisco, CA";
            // 
            // btnFilterMore
            // 
            btnFilterMore.Location = new Point(470, 85);
            btnFilterMore.Name = "btnFilterMore";
            btnFilterMore.Radius = 20;
            btnFilterMore.Size = new Size(120, 35);
            btnFilterMore.TabIndex = 1;
            btnFilterMore.Text = "More Filters";
            // 
            // btnFilterAmenities
            // 
            btnFilterAmenities.BackColor = Color.Black;
            btnFilterAmenities.ForeColor = Color.White;
            btnFilterAmenities.Location = new Point(320, 85);
            btnFilterAmenities.Name = "btnFilterAmenities";
            btnFilterAmenities.Radius = 20;
            btnFilterAmenities.Size = new Size(140, 35);
            btnFilterAmenities.TabIndex = 2;
            btnFilterAmenities.Text = "Any Amenities x";
            // 
            // btnFilterBeds
            // 
            btnFilterBeds.Location = new Point(150, 85);
            btnFilterBeds.Name = "btnFilterBeds";
            btnFilterBeds.Radius = 20;
            btnFilterBeds.Size = new Size(160, 35);
            btnFilterBeds.TabIndex = 3;
            btnFilterBeds.Text = "Bedrooms v";
            // 
            // btnFilterHomeType
            // 
            btnFilterHomeType.Location = new Point(20, 85);
            btnFilterHomeType.Name = "btnFilterHomeType";
            btnFilterHomeType.Radius = 20;
            btnFilterHomeType.Size = new Size(120, 35);
            btnFilterHomeType.TabIndex = 4;
            btnFilterHomeType.Text = "Home Type v";
            // 
            // btnSearch
            // 
            btnSearch.BackActive = Color.FromArgb(255, 90, 31);
            btnSearch.Location = new Point(630, 20);
            btnSearch.Name = "btnSearch";
            btnSearch.Radius = 8;
            btnSearch.Size = new Size(100, 50);
            btnSearch.TabIndex = 5;
            btnSearch.Text = "Search";
            btnSearch.Type = AntdUI.TTypeMini.Primary;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(20, 20);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search by City, Zip, or Address...";
            txtSearch.Radius = 8;
            txtSearch.Size = new Size(600, 50);
            txtSearch.TabIndex = 6;
            // 
            // pnlDetailsHost
            // 
            pnlDetailsHost.Dock = DockStyle.Fill;
            pnlDetailsHost.Location = new Point(320, 80);
            pnlDetailsHost.Name = "pnlDetailsHost";
            pnlDetailsHost.Size = new Size(1080, 870);
            pnlDetailsHost.TabIndex = 10;
            pnlDetailsHost.Visible = false;
            // 
            // RentAllSearch
            // 
            BackColor = Color.White;
            ClientSize = new Size(1400, 950);
            Controls.Add(pnlDetailsHost);
            Controls.Add(pnlMainContent);
            Controls.Add(pnlSidebar);
            Controls.Add(pnlTopBar);
            Name = "RentAllSearch";
            Text = "RentAll Search";
            WindowState = FormWindowState.Maximized;
            pnlTopBar.ResumeLayout(false);
            pnlTopBar.PerformLayout();
            pnlSidebar.ResumeLayout(false);
            pnlSidebar.PerformLayout();
            pnlMainContent.ResumeLayout(false);
            pnlSearchHeader.ResumeLayout(false);
            pnlSearchHeader.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        // DECLARE VARIABLES
        private System.Windows.Forms.Panel pnlTopBar;
        private System.Windows.Forms.Label lblLogo;
        private AntdUI.Button btnHeart;
        private AntdUI.Button btnNotif;
        private AntdUI.Button btnMenuMessages;
        private AntdUI.Button btnMenuRentals;
        private AntdUI.Button btnMenuHome;

        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Label lblFilterTitle;
        private System.Windows.Forms.Label btnResetFilters;
        private System.Windows.Forms.Label lblRentTitle;
        private AntdUI.Slider sliderRent;
        private System.Windows.Forms.Label lblRentValue;

        private System.Windows.Forms.Label lblAmenities;
        private AntdUI.Checkbox chkAC;
        private AntdUI.Checkbox chkWasher;
        private AntdUI.Checkbox chkPet;
        private AntdUI.Checkbox chkParking;

        private System.Windows.Forms.Label lblBedroom;
        private AntdUI.Select selBedroom;
        private System.Windows.Forms.Label lblBathroom;
        private AntdUI.Select selBathroom;
        private System.Windows.Forms.Label lblCorridor;
        private AntdUI.Select selCorridor;
        private System.Windows.Forms.Label lblKitchen;
        private AntdUI.Select selKitchen;
        private System.Windows.Forms.Label lblSqFt;
        private AntdUI.InputNumber inputSqFt;

        private System.Windows.Forms.Panel pnlMainContent;
        private System.Windows.Forms.Panel pnlSearchHeader;
        private AntdUI.Input txtSearch;
        private AntdUI.Button btnSearch;
        private AntdUI.Button btnFilterHomeType;
        private AntdUI.Button btnFilterBeds;
        private AntdUI.Button btnFilterAmenities;
        private AntdUI.Button btnFilterMore;
        private System.Windows.Forms.Label lblResultCount;
        private System.Windows.Forms.FlowLayoutPanel flowListings;

        // NEW PANEL
        private System.Windows.Forms.Panel pnlDetailsHost;
    }
}