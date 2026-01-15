using System;
using RentalSystemUI.Data;
using RentalSystemUI.Models;
using BCrypt.Net;

namespace RentalSystemUI.Services
{
    public class AuthService
    {
        private UserRepository _userRepo = new UserRepository();

        // Login: Validate credentials and return User object if successful
        public User? Login(string email, string plainPassword)
        {
            var user = _userRepo.GetByEmail(email);
            if (user == null) return null;

            bool valid = BCrypt.Net.BCrypt.Verify(plainPassword, user.PasswordHash);
            if (valid) return user;
            
            return null;
        }

        // Register: Hash password and insert user
        public bool Register(User user, string plainPassword)
        {
            // Hash the password
            string hash = BCrypt.Net.BCrypt.HashPassword(plainPassword);
            user.PasswordHash = hash;

            // Basic Validation
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.FullName)) return false;

            return _userRepo.Insert(user);
        }

        public bool UserExists(string email, string phone)
        {
            return _userRepo.Exists(email, phone);
        }

        public bool EmailExists(string email)
        {
             return _userRepo.ExistsByEmail(email);
        }

        public void UpdatePassword(string email, string newPassword)
        {
            string hash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            _userRepo.UpdatePassword(email, hash);
        }
    }
}
