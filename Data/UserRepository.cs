using System;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using RentalSystemUI.Models;

namespace RentalSystemUI.Data
{
    public class UserRepository : Database
    {
        public bool Exists(string email, string phone)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM USERS WHERE Email = @email OR Phone = @phone";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@phone", phone);
                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
        }

        public bool ExistsByEmail(string email)
        {
             using (var conn = GetConnection())
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM USERS WHERE Email = @email";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
        }

        public User? GetByEmail(string email)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                // Ensure we get Active users if that logic is needed (existing code: IsActive = 1)
                string query = "SELECT UserID, FullName, Email, PasswordHash, Phone, UserType FROM USERS WHERE Email = @email AND IsActive = 1";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                UserID = reader["UserID"] != DBNull.Value ? (int)reader["UserID"] : 0, // UserID might not be selected in original code, but good to have
                                FullName = reader["FullName"].ToString() ?? "",
                                Email = reader["Email"].ToString() ?? "",
                                PasswordHash = reader["PasswordHash"].ToString() ?? "",
                                Phone = reader["Phone"].ToString() ?? "",
                                UserType = reader["UserType"].ToString() ?? ""
                            };
                        }
                    }
                }
            }
            return null;
        }

        public bool Insert(User user)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = @"INSERT INTO USERS (FullName, Email, PasswordHash, Phone, UserType, IsActive) 
                                 VALUES (@name, @email, @hash, @phone, @role, 1)";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", user.FullName);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@hash", user.PasswordHash);
                    cmd.Parameters.AddWithValue("@phone", user.Phone);
                    cmd.Parameters.AddWithValue("@role", user.UserType);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public void UpdatePassword(string email, string newPasswordHash)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "UPDATE USERS SET PasswordHash = @hash WHERE Email = @email";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@hash", newPasswordHash);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool ExistsByEmailExceptUser(string email, int userId)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM USERS WHERE Email = @email AND UserID <> @uid";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@uid", userId);
                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
        }

        public bool UpdateProfile(int userId, string fullName, string email, string phone)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "UPDATE USERS SET FullName=@name, Email=@email, Phone=@phone WHERE UserID=@uid";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", fullName ?? "");
                    cmd.Parameters.AddWithValue("@email", email ?? "");
                    cmd.Parameters.AddWithValue("@phone", phone ?? "");
                    cmd.Parameters.AddWithValue("@uid", userId);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public string? GetPasswordHash(int userId)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "SELECT PasswordHash FROM USERS WHERE UserID = @uid";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@uid", userId);
                    var obj = cmd.ExecuteScalar();
                    return obj == null || obj == DBNull.Value ? null : obj.ToString();
                }
            }
        }

        public bool UpdatePasswordByUserId(int userId, string newPasswordHash)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "UPDATE USERS SET PasswordHash = @hash WHERE UserID = @uid";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@hash", newPasswordHash);
                    cmd.Parameters.AddWithValue("@uid", userId);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
