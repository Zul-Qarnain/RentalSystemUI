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
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Multiselect = true;
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    if (ofd.FileNames.Length > 4)
                    {
                        AntdUI.Message.warn(this, "You can select a maximum of 4 images.");
                        return;
                    }
                    _selectedImages = new List<string>(ofd.FileNames);
                    lblImageFileNames.Text = $"{_selectedImages.Count} file(s) selected: " + string.Join(", ", _selectedImages.Select(System.IO.Path.GetFileName));
                }
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
                         rent, status, rooms, kitchen, washroom, isPet, isAc, isAvailable
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
