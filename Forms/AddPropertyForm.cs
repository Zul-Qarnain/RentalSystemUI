using System;
using System.Drawing;
using System.Windows.Forms;
using AntdUI;
using RentalSystemUI.Services;

namespace RentalSystemUI.Forms
{
    public partial class AddPropertyForm : Form
    {
        private PropertyService _service = new PropertyService();
        private int _landlordId = 1;
        private int? _propertyId = null;
        private List<string> _selectedImages = new List<string>();

        public AddPropertyForm(int landlordId = 1, int? propertyId = null)
        {
            _landlordId = landlordId;
            _propertyId = propertyId;
            InitializeComponent();
            InitializeData();
            
            if (_propertyId.HasValue)
            {
                this.Text = "Edit Property";
                btnSave.Text = "Update Property";
                LoadPropertyData();
            }
            
            // Event Handlers
            btnSave.Click += OnSaveClick;
            btnCancel.Click += (s, e) => this.Close();
            btnUploadImages.Click += OnUploadImagesClick;
        }

        private void OnUploadImagesClick(object? sender, EventArgs e)
        {
            if (_selectedImages.Count >= 4)
            {
                AntdUI.Message.warn(this, "Maximum 4 images allowed!");
                return;
            }

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Multiselect = true;
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    int remaining = 4 - _selectedImages.Count;
                    var filesToAdd = ofd.FileNames.Take(remaining);
                    
                    foreach (var file in filesToAdd)
                    {
                        if (!_selectedImages.Contains(file))
                            _selectedImages.Add(file);
                    }
                    
                    if (ofd.FileNames.Length > remaining)
                    {
                        AntdUI.Message.warn(this, $"Only added {remaining} images. Max 4 allowed.");
                    }
                    
                    RefreshImageListUI();
                }
            }
        }

        private void RefreshImageListUI()
        {
            pnlImageList.Controls.Clear();
            
            for (int i = 0; i < _selectedImages.Count; i++)
            {
                string fileName = System.IO.Path.GetFileName(_selectedImages[i]);
                string imagePath = _selectedImages[i]; // Capture for closure
                
                // Create a tag panel for each image
                System.Windows.Forms.Panel tag = new System.Windows.Forms.Panel
                {
                    Height = 28,
                    Width = 180,
                    BackColor = Color.FromArgb(226, 232, 240),
                    Margin = new Padding(3),
                    Padding = new Padding(5, 3, 5, 3)
                };
                
                // Image number + name
                System.Windows.Forms.Label lbl = new System.Windows.Forms.Label
                {
                    Text = $"{i + 1}. {fileName}",
                    AutoSize = false,
                    Width = 145,
                    Height = 22,
                    Location = new Point(5, 3),
                    Font = new Font("Segoe UI", 8),
                    ForeColor = Color.FromArgb(51, 65, 85)
                };
                
                // Remove button (X)
                System.Windows.Forms.Button btnRemove = new System.Windows.Forms.Button
                {
                    Text = "✕",
                    Size = new Size(22, 22),
                    Location = new Point(152, 3),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.FromArgb(239, 68, 68),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 7, FontStyle.Bold),
                    Cursor = Cursors.Hand
                };
                btnRemove.FlatAppearance.BorderSize = 0;
                btnRemove.Click += (s, ev) => {
                    _selectedImages.Remove(imagePath);
                    RefreshImageListUI();
                };
                
                tag.Controls.Add(lbl);
                tag.Controls.Add(btnRemove);
                pnlImageList.Controls.Add(tag);
            }
        }

        private void InitializeData()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("Available");
            cmbStatus.Items.Add("Rented");
            cmbStatus.Items.Add("Maintenance");
            cmbStatus.SelectedValue = "Available";
        }

        private void LoadPropertyData()
        {
            if (!_propertyId.HasValue) return;
            var prop = _service.GetPropertyById(_propertyId.Value);
            if (prop == null) return;

            txtTitle.Text = prop.Title;
            txtAddress.Text = prop.Address;
            txtCity.Text = prop.City;
            txtRent.Text = prop.RentAmount.ToString("0.##");
            txtDescription.Text = prop.Description;
            cmbStatus.SelectedValue = prop.Status;
            
            txtRooms.Text = prop.Rooms.ToString();
            txtKitchen.Text = prop.Kitchen.ToString();
            txtWashRoom.Text = prop.WashRoom.ToString();
            
            chkPet.Checked = prop.IsPetAllowed;
            chkAC.Checked = prop.IsAC;
            chkAvailability.Checked = prop.AvailabilityStatus;

            // Load existing images
            var existingImages = _service.GetPropertyImages(_propertyId.Value);
            if (existingImages != null && existingImages.Count > 0)
            {
                _selectedImages = existingImages;
                RefreshImageListUI();
            }
        }

        private void OnSaveClick(object? sender, EventArgs e)
        {
             // Validation
             if (string.IsNullOrWhiteSpace(txtTitle.Text))
             {
                 AntdUI.Message.error(this, "Title is required.");
                 return;
             }
             if (!decimal.TryParse(txtRent.Text, out decimal rent))
             {
                 AntdUI.Message.error(this, "Invalid Rent Amount.");
                 return;
             }

             // Parse new fields
             int.TryParse(txtRooms.Text, out int rooms);
             int.TryParse(txtKitchen.Text, out int kitchen);
             int.TryParse(txtWashRoom.Text, out int washroom);
             
             bool isPet = chkPet.Checked;
             bool isAc = chkAC.Checked;
             bool isAvailable = chkAvailability.Checked;
             string status = cmbStatus.SelectedValue?.ToString() ?? "Available";

             bool success = false;
             try 
             {
                 if (_propertyId.HasValue)
                 {
                     success = _service.UpdateProperty(
                         _propertyId.Value, txtTitle.Text, txtDescription.Text, txtAddress.Text, txtCity.Text, 
                         rent, status, rooms, kitchen, washroom, isPet, isAc, isAvailable,
                         _selectedImages
                     );
                 }
                 else
                 {
                     int newId = _service.AddProperty(
                         _landlordId, txtTitle.Text, txtDescription.Text, txtAddress.Text, txtCity.Text, 
                         rent, status, rooms, kitchen, washroom, isPet, isAc, isAvailable, 
                         _selectedImages
                     );
                     success = newId > 0;
                 }

                 if (success)
                 {
                     AntdUI.Message.success(this, _propertyId.HasValue ? "Property Updated!" : "Property Added!");
                     this.DialogResult = DialogResult.OK;
                     this.Close();
                 }
                 else
                 {
                     AntdUI.Message.error(this, "Operation failed. Database connection might be issues.");
                 }
             }
             catch (Exception ex)
             {
                 MessageBox.Show("Error Details: " + ex.ToString(), "Database/System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
             }
        }
    }
}
