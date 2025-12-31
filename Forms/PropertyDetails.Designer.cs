namespace RentalSystemUI.Forms
{
    partial class PropertyDetails
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
            this.pnlScroll = new System.Windows.Forms.Panel();
            this.btnClose = new AntdUI.Button();

            // --- Header ---
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubHeader = new System.Windows.Forms.Label();

            // --- Image Grid ---
            this.pnlImages = new System.Windows.Forms.Panel();
            this.picMain = new System.Windows.Forms.PictureBox();
            this.pnlSmallImages = new System.Windows.Forms.Panel();
            this.picSub1 = new System.Windows.Forms.PictureBox();
            this.picSub2 = new System.Windows.Forms.PictureBox();
            this.picSub3 = new System.Windows.Forms.PictureBox();
            this.picSub4 = new System.Windows.Forms.PictureBox();

            // --- Content Container ---
            this.pnlContent = new System.Windows.Forms.Panel();

            // Left Side (Info)
            this.pnlLeftInfo = new System.Windows.Forms.Panel();
            this.lblHost = new System.Windows.Forms.Label();
            this.lblSpecs = new System.Windows.Forms.Label();
            this.divider1 = new System.Windows.Forms.Label(); // Line
            this.lblDescriptionTitle = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.divider2 = new System.Windows.Forms.Label();
            this.lblAmenitiesTitle = new System.Windows.Forms.Label();
            this.flowAmenities = new System.Windows.Forms.FlowLayoutPanel();

            // Right Side (Booking Card)
            this.cardBooking = new AntdUI.Panel();
            this.lblPriceLarge = new System.Windows.Forms.Label();
            this.lblPerMonth = new System.Windows.Forms.Label();
            this.dateCheckIn = new AntdUI.DatePicker();
            this.dateCheckOut = new AntdUI.DatePicker();
            this.selGuests = new AntdUI.Select();
            this.btnBook = new AntdUI.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblTotalValue = new System.Windows.Forms.Label();
            this.lblBreakdown = new System.Windows.Forms.Label(); // "You won't be charged yet"

            this.pnlScroll.SuspendLayout();
            this.pnlImages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMain)).BeginInit();
            this.pnlSmallImages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSub1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSub2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSub3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSub4)).BeginInit();
            this.pnlContent.SuspendLayout();
            this.pnlLeftInfo.SuspendLayout();
            this.cardBooking.SuspendLayout();
            this.SuspendLayout();

            // 
            // Form Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.pnlScroll);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Property Details";

            // 
            // pnlScroll
            // 
            this.pnlScroll.AutoScroll = true;
            this.pnlScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlScroll.Controls.Add(this.pnlContent);
            this.pnlScroll.Controls.Add(this.pnlImages);
            this.pnlScroll.Controls.Add(this.lblSubHeader);
            this.pnlScroll.Controls.Add(this.lblTitle);
            this.pnlScroll.Controls.Add(this.btnClose);
            this.pnlScroll.Padding = new System.Windows.Forms.Padding(40);

            // Close Button
            this.btnClose.Location = new System.Drawing.Point(1140, 10);
            this.btnClose.Size = new System.Drawing.Size(40, 40);
            this.btnClose.Text = "✕";
            this.btnClose.Type = AntdUI.TTypeMini.Error;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            // Title
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(40, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(400, 60);
            this.lblTitle.Text = "Sunny Downtown Loft";

            // SubHeader (Location + Rating)
            this.lblSubHeader.AutoSize = true;
            this.lblSubHeader.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSubHeader.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblSubHeader.Location = new System.Drawing.Point(45, 80);
            this.lblSubHeader.Name = "lblSubHeader";
            this.lblSubHeader.Text = "★ 4.98 (124 reviews)  •  San Francisco, CA";

            // 
            // --- IMAGES SECTION ---
            // 
            this.pnlImages.Location = new System.Drawing.Point(40, 120);
            this.pnlImages.Size = new System.Drawing.Size(1120, 400);
            this.pnlImages.Controls.Add(this.pnlSmallImages);
            this.pnlImages.Controls.Add(this.picMain);

            // Big Image (Left Half)
            this.picMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.picMain.Width = 555;
            this.picMain.BackColor = System.Drawing.Color.LightGray;
            this.picMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom; // or CenterImage for crop effect

            // Small Images Container (Right Half)
            this.pnlSmallImages.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlSmallImages.Width = 555;
            this.pnlSmallImages.Controls.Add(this.picSub4);
            this.pnlSmallImages.Controls.Add(this.picSub3);
            this.pnlSmallImages.Controls.Add(this.picSub2);
            this.pnlSmallImages.Controls.Add(this.picSub1);

            // 4 Small Pics Grid
            int w = 272; int h = 195; int pad = 10;
            this.picSub1.SetBounds(0, 0, w, h); this.picSub1.BackColor = System.Drawing.Color.WhiteSmoke; this.picSub1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picSub2.SetBounds(w + pad, 0, w, h); this.picSub2.BackColor = System.Drawing.Color.WhiteSmoke; this.picSub2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picSub3.SetBounds(0, h + pad, w, h); this.picSub3.BackColor = System.Drawing.Color.WhiteSmoke; this.picSub3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picSub4.SetBounds(w + pad, h + pad, w, h); this.picSub4.BackColor = System.Drawing.Color.WhiteSmoke; this.picSub4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;

            // 
            // --- MAIN CONTENT AREA ---
            // 
            this.pnlContent.Location = new System.Drawing.Point(40, 540);
            this.pnlContent.Size = new System.Drawing.Size(1120, 800); // Tall enough for content
            this.pnlContent.Controls.Add(this.cardBooking);
            this.pnlContent.Controls.Add(this.pnlLeftInfo);

            // 
            // LEFT INFO (Description, Amenities)
            // 
            this.pnlLeftInfo.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeftInfo.Width = 650;
            this.pnlLeftInfo.Controls.Add(this.flowAmenities);
            this.pnlLeftInfo.Controls.Add(this.lblAmenitiesTitle);
            this.pnlLeftInfo.Controls.Add(this.divider2);
            this.pnlLeftInfo.Controls.Add(this.lblDescription);
            this.pnlLeftInfo.Controls.Add(this.lblDescriptionTitle);
            this.pnlLeftInfo.Controls.Add(this.divider1);
            this.pnlLeftInfo.Controls.Add(this.lblSpecs);
            this.pnlLeftInfo.Controls.Add(this.lblHost);

            this.lblHost.Text = "Entire loft hosted by Sarah";
            this.lblHost.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblHost.Location = new System.Drawing.Point(0, 0);
            this.lblHost.AutoSize = true;

            this.lblSpecs.Text = "4 guests • 2 bedrooms • 2 beds • 2 baths";
            this.lblSpecs.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSpecs.Location = new System.Drawing.Point(0, 40);
            this.lblSpecs.AutoSize = true;

            // Divider Line
            this.divider1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.divider1.Location = new System.Drawing.Point(0, 80);
            this.divider1.Size = new System.Drawing.Size(600, 2);

            // Description
            this.lblDescriptionTitle.Text = "About this place";
            this.lblDescriptionTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblDescriptionTitle.Location = new System.Drawing.Point(0, 100);
            this.lblDescriptionTitle.AutoSize = true;

            this.lblDescription.Text = "Loading description...";
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblDescription.Location = new System.Drawing.Point(0, 140);
            this.lblDescription.Size = new System.Drawing.Size(600, 150); // Auto wraps

            // Divider 2
            this.divider2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.divider2.Location = new System.Drawing.Point(0, 310);
            this.divider2.Size = new System.Drawing.Size(600, 2);

            // Amenities
            this.lblAmenitiesTitle.Text = "What this place offers";
            this.lblAmenitiesTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblAmenitiesTitle.Location = new System.Drawing.Point(0, 330);
            this.lblAmenitiesTitle.AutoSize = true;

            this.flowAmenities.Location = new System.Drawing.Point(0, 370);
            this.flowAmenities.Size = new System.Drawing.Size(600, 200);
            // (We will add labels here via code)

            // 
            // RIGHT SIDE (BOOKING CARD)
            // 
            this.cardBooking.Location = new System.Drawing.Point(720, 0); // Positioned to right
            this.cardBooking.Size = new System.Drawing.Size(380, 450);
            this.cardBooking.Shadow = 15;
            this.cardBooking.ShadowColor = System.Drawing.Color.FromArgb(30, 0, 0, 0);
            this.cardBooking.Radius = 12;
            this.cardBooking.BackColor = System.Drawing.Color.White;

            // Add Controls to Booking Card
            this.cardBooking.Controls.Add(this.lblTotalValue);
            this.cardBooking.Controls.Add(this.lblTotal);
            this.cardBooking.Controls.Add(this.lblBreakdown);
            this.cardBooking.Controls.Add(this.btnBook);
            this.cardBooking.Controls.Add(this.selGuests);
            this.cardBooking.Controls.Add(this.dateCheckOut);
            this.cardBooking.Controls.Add(this.dateCheckIn);
            this.cardBooking.Controls.Add(this.lblPerMonth);
            this.cardBooking.Controls.Add(this.lblPriceLarge);

            // Price Header
            this.lblPriceLarge.Text = "$2,400";
            this.lblPriceLarge.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblPriceLarge.Location = new System.Drawing.Point(20, 20);
            this.lblPriceLarge.AutoSize = true;

            this.lblPerMonth.Text = "/ month";
            this.lblPerMonth.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPerMonth.Location = new System.Drawing.Point(110, 28);
            this.lblPerMonth.AutoSize = true;

            // Inputs
            this.dateCheckIn.Location = new System.Drawing.Point(25, 70);
            this.dateCheckIn.Size = new System.Drawing.Size(160, 45);
            this.dateCheckIn.PlaceholderText = "Check-In";

            this.dateCheckOut.Location = new System.Drawing.Point(195, 70);
            this.dateCheckOut.Size = new System.Drawing.Size(160, 45);
            this.dateCheckOut.PlaceholderText = "Check-Out";

            this.selGuests.Location = new System.Drawing.Point(25, 125);
            this.selGuests.Size = new System.Drawing.Size(330, 45);
            this.selGuests.PlaceholderText = "1 Guest";
            this.selGuests.Items.AddRange(new object[] { "1 Guest", "2 Guests", "3 Guests", "4 Guests" });

            // Button
            this.btnBook.Text = "Book Now";
            this.btnBook.Type = AntdUI.TTypeMini.Primary;
            this.btnBook.BackActive = System.Drawing.Color.FromArgb(227, 28, 95); // Airbnb Pink/Red
            this.btnBook.Radius = 8;
            this.btnBook.Location = new System.Drawing.Point(25, 190);
            this.btnBook.Size = new System.Drawing.Size(330, 50);
            this.btnBook.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnBook.Click += new System.EventHandler(this.btnBookNow_Click);

            this.lblBreakdown.Text = "You won't be charged yet";
            this.lblBreakdown.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblBreakdown.Location = new System.Drawing.Point(25, 250);
            this.lblBreakdown.Size = new System.Drawing.Size(330, 20);
            this.lblBreakdown.ForeColor = System.Drawing.Color.Gray;

            // Total
            this.lblTotal.Text = "Total";
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotal.Location = new System.Drawing.Point(25, 300);
            this.lblTotal.AutoSize = true;

            this.lblTotalValue.Text = "$2,550";
            this.lblTotalValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotalValue.Location = new System.Drawing.Point(280, 300);
            this.lblTotalValue.AutoSize = true;

            this.pnlScroll.ResumeLayout(false);
            this.pnlScroll.PerformLayout();
            this.pnlImages.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picMain)).EndInit();
            this.pnlSmallImages.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picSub1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSub2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSub3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSub4)).EndInit();
            this.pnlContent.ResumeLayout(false);
            this.pnlLeftInfo.ResumeLayout(false);
            this.pnlLeftInfo.PerformLayout();
            this.cardBooking.ResumeLayout(false);
            this.cardBooking.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        // Variables
        private System.Windows.Forms.Panel pnlScroll;
        private AntdUI.Button btnClose;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubHeader;

        private System.Windows.Forms.Panel pnlImages;
        private System.Windows.Forms.PictureBox picMain;
        private System.Windows.Forms.Panel pnlSmallImages;
        private System.Windows.Forms.PictureBox picSub1;
        private System.Windows.Forms.PictureBox picSub2;
        private System.Windows.Forms.PictureBox picSub3;
        private System.Windows.Forms.PictureBox picSub4;

        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnlLeftInfo;
        private System.Windows.Forms.Label lblHost;
        private System.Windows.Forms.Label lblSpecs;
        private System.Windows.Forms.Label divider1;
        private System.Windows.Forms.Label lblDescriptionTitle;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label divider2;
        private System.Windows.Forms.Label lblAmenitiesTitle;
        private System.Windows.Forms.FlowLayoutPanel flowAmenities;

        private AntdUI.Panel cardBooking;
        private System.Windows.Forms.Label lblPriceLarge;
        private System.Windows.Forms.Label lblPerMonth;
        private AntdUI.DatePicker dateCheckIn;
        private AntdUI.DatePicker dateCheckOut;
        private AntdUI.Select selGuests;
        private AntdUI.Button btnBook;
        private System.Windows.Forms.Label lblBreakdown;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblTotalValue;
    }
}