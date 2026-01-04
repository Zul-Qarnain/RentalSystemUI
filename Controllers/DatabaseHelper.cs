using System;
using System.Data;
using Microsoft.Data.SqlClient;
using BCrypt.Net; // Install BCrypt.Net-Next via NuGet
using DotNetEnv;  // Install DotNetEnv via NuGet

namespace RentalSystemUI.Controllers
{
    public class DatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper()
        {
            // Load .env file
            Env.Load();
            _connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
                                ?? throw new Exception("DB_CONNECTION_STRING string not found in .env file.");
        }

        public SqlConnection GetConnection() => new SqlConnection(_connectionString);

        // 1. CHECK IF USER EXISTS
        public bool UserExists(string email, string phone)
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

        // 2. REGISTER USER
        public bool RegisterUser(string name, string email, string phone, string plainPassword, string role)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string hash = BCrypt.Net.BCrypt.HashPassword(plainPassword);

                string query = @"INSERT INTO USERS (FullName, Email, PasswordHash, Phone, UserType, IsActive) 
                                 VALUES (@name, @email, @hash, @phone, @role, 1)";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@hash", hash);
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.Parameters.AddWithValue("@role", role);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // 3. LOGIN USER
        public bool ValidateUser(string email, string plainPassword, out string role, out string name)
        {
            role = ""; name = "";
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "SELECT PasswordHash, UserType, FullName FROM USERS WHERE Email = @email AND IsActive = 1";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string dbHash = reader["PasswordHash"].ToString() ?? "";
                            role = reader["UserType"].ToString() ?? "";
                            name = reader["FullName"].ToString() ?? "";

                            if (string.IsNullOrEmpty(dbHash)) return false;
                            return BCrypt.Net.BCrypt.Verify(plainPassword, dbHash);
                        }
                    }
                }
            }
            return false;
        }

        // 4. UPDATE PASSWORD
        public void UpdatePassword(string email, string newPass)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string hash = BCrypt.Net.BCrypt.HashPassword(newPass);
                string query = "UPDATE USERS SET PasswordHash = @hash WHERE Email = @email";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@hash", hash);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable ExecuteQuery(string query, SqlParameter[]? parameters = null)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        var dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }
}