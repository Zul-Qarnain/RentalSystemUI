
namespace RentalSystemUI.Forms
{
    partial class AddPropertyForm
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
            this.txtTitle = new AntdUI.Input();
            
            this.lblAddress = new AntdUI.Label();
            this.txtAddress = new AntdUI.Input();
            
            this.lblCity = new AntdUI.Label();
            this.txtCity = new AntdUI.Input();
            
            this.lblRent = new AntdUI.Label();
            this.txtRent = new AntdUI.Input();
            
            this.lblStatus = new AntdUI.Label();
            this.cmbStatus = new AntdUI.Select(); // Assuming Select exists, otherwise Input
            
            this.lblDescription = new AntdUI.Label();
            this.txtDescription = new AntdUI.Input();
            
            this.lblRooms = new AntdUI.Label();
            this.txtRooms = new AntdUI.Input();
            
            this.lblKitchen = new AntdUI.Label();
            this.txtKitchen = new AntdUI.Input();
            
            this.lblWashRoom = new AntdUI.Label();
            this.txtWashRoom = new AntdUI.Input();
            
            this.chkPet = new AntdUI.Checkbox();
            this.chkAC = new AntdUI.Checkbox();
            this.chkAvailability = new AntdUI.Checkbox();

            this.btnSave = new AntdUI.Button();
            this.btnCancel = new AntdUI.Button();
            
            this.SuspendLayout();

            // Labels & Inputs Font
            System.Drawing.Font lblFont = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            System.Drawing.Font inputFont = new System.Drawing.Font("Segoe UI", 10F);

            // Title
            this.lblTitle.Text = "Property Title";
            this.lblTitle.Location = new System.Drawing.Point(30, 20);
            this.lblTitle.Size = new System.Drawing.Size(150, 25);
            this.lblTitle.Font = lblFont;
            
            this.txtTitle.Location = new System.Drawing.Point(30, 45);
            this.txtTitle.Size = new System.Drawing.Size(500, 40); // Widened
            this.txtTitle.Font = inputFont;

            // Address
            this.lblAddress.Text = "Address";
            this.lblAddress.Location = new System.Drawing.Point(30, 100);
            this.lblAddress.Size = new System.Drawing.Size(150, 25);
            this.lblAddress.Font = lblFont;
            
            this.txtAddress.Location = new System.Drawing.Point(30, 125);
            this.txtAddress.Size = new System.Drawing.Size(500, 40); // Widened
            this.txtAddress.Font = inputFont;

            // City (Half Width)
            this.lblCity.Text = "City";
            this.lblCity.Location = new System.Drawing.Point(30, 180);
            this.lblCity.Size = new System.Drawing.Size(100, 25);
            this.lblCity.Font = lblFont;
            
            this.txtCity.Location = new System.Drawing.Point(30, 205);
            this.txtCity.Size = new System.Drawing.Size(240, 40);
            this.txtCity.Font = inputFont;

            // Rent (Half Width)
            this.lblRent.Text = "Rent Amount";
            this.lblRent.Location = new System.Drawing.Point(290, 180);
            this.lblRent.Size = new System.Drawing.Size(150, 25);
            this.lblRent.Font = lblFont;
            
            this.txtRent.Location = new System.Drawing.Point(290, 205);
            this.txtRent.Size = new System.Drawing.Size(240, 40);
            this.txtRent.Font = inputFont;

            // Rooms (Third Width)
            this.lblRooms.Text = "Rooms";
            this.lblRooms.Location = new System.Drawing.Point(30, 260);
            this.lblRooms.Size = new System.Drawing.Size(80, 25);
            this.lblRooms.Font = lblFont;
            
            this.txtRooms.Location = new System.Drawing.Point(30, 285);
            this.txtRooms.Size = new System.Drawing.Size(100, 40);
            this.txtRooms.Font = inputFont;
            this.txtRooms.Text = "1";

            // Kitchen (Third Width)
            this.lblKitchen.Text = "Kitchen";
            this.lblKitchen.Location = new System.Drawing.Point(180, 260);
            this.lblKitchen.Size = new System.Drawing.Size(80, 25);
            this.lblKitchen.Font = lblFont;

            this.txtKitchen.Location = new System.Drawing.Point(180, 285);
            this.txtKitchen.Size = new System.Drawing.Size(100, 40);
            this.txtKitchen.Font = inputFont;
            this.txtKitchen.Text = "1";

            // WashRoom (Third Width)
            this.lblWashRoom.Text = "Washroom";
            this.lblWashRoom.Location = new System.Drawing.Point(330, 260);
            this.lblWashRoom.Size = new System.Drawing.Size(80, 25);
            this.lblWashRoom.Font = lblFont;

            this.txtWashRoom.Location = new System.Drawing.Point(330, 285);
            this.txtWashRoom.Size = new System.Drawing.Size(100, 40);
            this.txtWashRoom.Font = inputFont;
            this.txtWashRoom.Text = "1";

            // Status (Right Column)
            this.lblStatus.Text = "Status";
            this.lblStatus.Location = new System.Drawing.Point(560, 20);
            this.lblStatus.Size = new System.Drawing.Size(100, 25);
            this.lblStatus.Font = lblFont;

            this.cmbStatus.Location = new System.Drawing.Point(560, 45);
            this.cmbStatus.Size = new System.Drawing.Size(220, 40);
            this.cmbStatus.Font = inputFont;
            this.cmbStatus.List = true; 

            // Description
            this.lblDescription.Text = "Description";
            this.lblDescription.Location = new System.Drawing.Point(560, 100);
            this.lblDescription.Size = new System.Drawing.Size(100, 25);
            this.lblDescription.Font = lblFont;
            
            this.txtDescription.Location = new System.Drawing.Point(560, 125);
            this.txtDescription.Size = new System.Drawing.Size(220, 120);
            this.txtDescription.Font = inputFont;
            this.txtDescription.Multiline = true;

            // Checkboxes (Vertical Stack below Description)
            this.chkPet.Text = "Pet Allowed";
            this.chkPet.Location = new System.Drawing.Point(560, 260);
            this.chkPet.Size = new System.Drawing.Size(150, 30);
            
            this.chkAC.Text = "Air Conditioned";
            this.chkAC.Location = new System.Drawing.Point(560, 295); // Moved down
            this.chkAC.Size = new System.Drawing.Size(150, 30);

            this.chkAvailability.Text = "Is Available?";
            this.chkAvailability.Location = new System.Drawing.Point(560, 330); // Moved down
            this.chkAvailability.Size = new System.Drawing.Size(150, 30);
            this.chkAvailability.Checked = true;

            // Image Upload Section - Bottom Left
            this.lblImages = new AntdUI.Label { Text = "Property Images (Max 4)", Location = new System.Drawing.Point(30, 350), Size = new System.Drawing.Size(200, 25), Font = lblFont };
            this.btnUploadImages = new AntdUI.Button { Text = "+ Add Images", Type = AntdUI.TTypeMini.Primary, Location = new System.Drawing.Point(30, 380), Size = new System.Drawing.Size(130, 35) };
            
            // Image List Panel - Shows selected images with remove buttons
            this.pnlImageList = new System.Windows.Forms.FlowLayoutPanel 
            { 
                Location = new System.Drawing.Point(30, 420), 
                Size = new System.Drawing.Size(350, 100), 
                AutoScroll = true,
                FlowDirection = System.Windows.Forms.FlowDirection.TopDown,
                WrapContents = false,
                BackColor = System.Drawing.Color.FromArgb(248, 250, 252),
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            };

            // Buttons - Bottom Right
            this.btnSave.Text = "Save Property";
            this.btnSave.Type = AntdUI.TTypeMini.Primary;
            this.btnSave.Location = new System.Drawing.Point(560, 480);
            this.btnSave.Size = new System.Drawing.Size(220, 45);
            this.btnSave.BackColor = System.Drawing.Color.Blue;
            this.btnSave.ForeColor = System.Drawing.Color.White;

            this.btnCancel.Text = "Cancel";
            this.btnCancel.Type = AntdUI.TTypeMini.Default;
            this.btnCancel.Location = new System.Drawing.Point(430, 480);
            this.btnCancel.Size = new System.Drawing.Size(100, 45);

            // Form - Increased height
            this.ClientSize = new System.Drawing.Size(820, 550);
            this.Controls.Add(this.lblTitle); this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.lblAddress); this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.lblCity); this.Controls.Add(this.txtCity);
            this.Controls.Add(this.lblRent); this.Controls.Add(this.txtRent);
            this.Controls.Add(this.lblRooms); this.Controls.Add(this.txtRooms);
            this.Controls.Add(this.lblKitchen); this.Controls.Add(this.txtKitchen);
            this.Controls.Add(this.lblWashRoom); this.Controls.Add(this.txtWashRoom);
            this.Controls.Add(this.lblStatus); this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.lblDescription); this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.chkPet);
            this.Controls.Add(this.chkAC);
            this.Controls.Add(this.chkAvailability);
            this.Controls.Add(this.lblImages);
            this.Controls.Add(this.btnUploadImages);
            this.Controls.Add(this.pnlImageList);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);

            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add New Property";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private AntdUI.Label lblTitle, lblAddress, lblCity, lblRent, lblStatus, lblDescription, lblRooms, lblKitchen, lblWashRoom;
        private AntdUI.Input txtTitle, txtAddress, txtCity, txtRent, txtDescription, txtRooms, txtKitchen, txtWashRoom;
        
        // Use AntdUI.Select for ComboBox if available, else Input or standard ComboBox. 
        // Based on previous code, AntdUI has many controls. Assuming Select exists. 
        // Check `MyProperties.cs`... it used standard combo or buttons. 
        // I will trust AntdUI has a Select or Dropdown. If not, I'll switch to standard.
        // Actually, let's use standard ComboBox or AntdUI.Select if known. 
        // Safest is AntdUI.Select if I've seen it. I haven't seen it explicitly used yet.
        // I'll stick to AntdUI.Select but if it fails build I'll fix.
        public AntdUI.Select cmbStatus; 

        public AntdUI.Checkbox chkPet, chkAC, chkAvailability;
        public AntdUI.Button btnSave, btnCancel;
        
        // Image Upload Controls
        public AntdUI.Label lblImages;
        public AntdUI.Button btnUploadImages;
        public System.Windows.Forms.FlowLayoutPanel pnlImageList;
    }
}
