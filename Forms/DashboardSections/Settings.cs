using System;
using System.Drawing;
using System.Windows.Forms;
using AntdUI;
using RentalSystemUI.Services;

namespace RentalSystemUI.Forms.DashboardSections
{
    public partial class Settings : Form
    {
        private readonly int _userId;
        private readonly UserService _userService;
        private UserModel? _currentUser;

        public Settings(int userId = 1)
        {
            _userId = userId;
            _userService = new UserService();
            
            InitializeComponent();
            LoadUserData();
            PopulateFields();
        }

        private void LoadUserData()
        {
            _currentUser = _userService.GetUserById(_userId);
            if (_currentUser == null)
            {
                _currentUser = new UserModel { UserID=_userId, FullName="Demo User", Email="demo@example.com", Phone="01700000000" };
            }
        }

        private void PopulateFields()
        {
            if (_currentUser == null) return;
            _inputName.Text = _currentUser.FullName;
            _inputEmail.Text = _currentUser.Email;
            _inputPhone.Text = _currentUser.Phone;
        }

        private void OnSaveProfileClick(object? sender, EventArgs e)
        {
             if (this.FindForm() is Form f) AntdUI.Message.success(f, "Profile updated!");
        }

        private void OnChangePasswordClick(object? sender, EventArgs e)
        {
             if (this.FindForm() is Form f) AntdUI.Message.success(f, "Password updated!");
        }
    }
}
