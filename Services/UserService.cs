using System;
using System.Data;
using Microsoft.Data.SqlClient;
using RentalSystemUI.Data;
using BCrypt.Net;

namespace RentalSystemUI.Services
{
    /// <summary>
    /// Service class for User-related database operations.
    /// </summary>
    public class UserService
    {
        private readonly string _connectionString;
        private readonly UserRepository _repo = new UserRepository();

        public UserService()
        {
            _connectionString = "Server=localhost;Database=RentalSystemDB;Trusted_Connection=True;TrustServerCertificate=True;Connection Timeout=1;";
        }

        /// <summary>
        /// Retrieves a user by their ID.
        /// Returns demo data if database is unavailable.
        /// </summary>
        public UserModel GetUserById(int userId)
        {
            // INSTANT RETURN if we know DB is down
            if (DatabaseState.ConnectionFailed)
            {
                return GetDemoUser(userId);
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT UserID, FullName, Email, Phone, UserType, IsActive, CreatedAt FROM USERS WHERE UserID = @UserID";
                    
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.CommandTimeout = 1;

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new UserModel
                            {
                                UserID = reader.GetInt32(0),
                                FullName = reader.GetString(1),
                                Email = reader.GetString(2),
                                Phone = reader.IsDBNull(3) ? "" : reader.GetString(3),
                                UserType = reader.GetString(4),
                                IsActive = reader.GetBoolean(5),
                                CreatedAt = reader.GetDateTime(6)
                            };
                        }
                    }
                }
            }
            catch
            {
                DatabaseState.MarkFailed();
            }

            return GetDemoUser(userId);
        }

        private UserModel GetDemoUser(int userId)
        {
            return new UserModel
            {
                UserID = userId,
                FullName = "Demo User",
                Email = "demo@example.com",
                Phone = "01700000000",
                UserType = "Landlord",
                IsActive = true,
                CreatedAt = DateTime.Now
            };
        }

        /// <summary>
        /// Updates the user's profile.
        /// </summary>
        public bool UpdateProfile(int userId, string fullName, string email, string phone)
        {
            if (DatabaseState.ConnectionFailed) return false;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    string query = @"UPDATE USERS SET FullName = @FullName, Email = @Email, Phone = @Phone WHERE UserID = @UserID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Parameters.AddWithValue("@FullName", fullName);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Phone", phone ?? "");
                    cmd.CommandTimeout = 1;

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch
            {
                DatabaseState.MarkFailed();
                return false;
            }
        }

        /// <summary>
        /// Changes the user's password.
        /// </summary>
        public bool ChangePassword(int userId, string oldPasswordHash, string newPasswordHash)
        {
            if (DatabaseState.ConnectionFailed) return false;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    string verifyQuery = "SELECT COUNT(1) FROM USERS WHERE UserID = @UserID AND PasswordHash = @OldPassword";
                    SqlCommand verifyCmd = new SqlCommand(verifyQuery, conn);
                    verifyCmd.Parameters.AddWithValue("@UserID", userId);
                    verifyCmd.Parameters.AddWithValue("@OldPassword", oldPasswordHash);
                    verifyCmd.CommandTimeout = 1;

                    conn.Open();
                    if ((int)verifyCmd.ExecuteScalar() == 0) return false;

                    string updateQuery = "UPDATE USERS SET PasswordHash = @NewPassword WHERE UserID = @UserID";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                    updateCmd.Parameters.AddWithValue("@UserID", userId);
                    updateCmd.Parameters.AddWithValue("@NewPassword", newPasswordHash);
                    updateCmd.CommandTimeout = 1;

                    return updateCmd.ExecuteNonQuery() > 0;
                }
            }
            catch
            {
                DatabaseState.MarkFailed();
                return false;
            }
        }

        public bool UpdateProfileInfo(int userId, string fullName, string email, string phone, out string error)
        {
            error = string.Empty;
            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(email))
            {
                error = "Name and email are required.";
                return false;
            }

            if (_repo.ExistsByEmailExceptUser(email.Trim(), userId))
            {
                error = "This email is already used by another user.";
                return false;
            }

            try
            {
                return _repo.UpdateProfile(userId, fullName.Trim(), email.Trim(), phone?.Trim() ?? "");
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool ChangePasswordPlain(int userId, string currentPasswordPlain, string newPasswordPlain, out string error)
        {
            error = string.Empty;
            if (string.IsNullOrWhiteSpace(currentPasswordPlain) || string.IsNullOrWhiteSpace(newPasswordPlain))
            {
                error = "Please fill both current and new password.";
                return false;
            }

            if (newPasswordPlain.Trim().Length < 6)
            {
                error = "New password must be at least 6 characters.";
                return false;
            }

            try
            {
                var hash = _repo.GetPasswordHash(userId);
                if (string.IsNullOrWhiteSpace(hash) || !BCrypt.Net.BCrypt.Verify(currentPasswordPlain, hash))
                {
                    error = "Current password is incorrect.";
                    return false;
                }

                var newHash = BCrypt.Net.BCrypt.HashPassword(newPasswordPlain);
                return _repo.UpdatePasswordByUserId(userId, newHash);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
    }

    /// <summary>
    /// User model.
    /// </summary>
    public class UserModel
    {
        public int UserID { get; set; }
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public string UserType { get; set; } = "";
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
