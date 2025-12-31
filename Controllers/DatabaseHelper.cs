using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using DotNetEnv; // Library to read .env

namespace RentalSystemUI.Controllers
{
    public class DatabaseHelper
    {
        // Singleton pattern (Optional, but good for DB helpers) or just a standard class
        // We will use a standard class method for simplicity but load Env once.

        private readonly string _connectionString;

        public DatabaseHelper()
        {
            // 1. Load the .env file variables into the system environment
            Env.Load();

            // 2. Get the secret string
            _connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
                                ?? throw new Exception("Database Connection String not found in .env file!");
        }

        // ==========================================
        //  CORE METHOD: GET CONNECTION
        // ==========================================
        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        // ==========================================
        //  HELPER: READ DATA (SELECT)
        // ==========================================
        // Pass in a query and parameters, get a DataTable back
        public DataTable ExecuteQuery(string query, SqlParameter[]? parameters = null)
        {
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    try
                    {
                        conn.Open();
                        DataTable dt = new DataTable();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                        return dt;
                    }
                    catch (Exception ex)
                    {
                        // Log error here if you have a logger
                        throw new Exception($"Database Error: {ex.Message}");
                    }
                }
            }
        }

        // ==========================================
        //  HELPER: WRITE DATA (INSERT/UPDATE/DELETE)
        // ==========================================
        public int ExecuteNonQuery(string query, SqlParameter[]? parameters = null)
        {
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    try
                    {
                        conn.Open();
                        return cmd.ExecuteNonQuery(); // Returns number of rows affected
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Database Error: {ex.Message}");
                    }
                }
            }
        }
    }
}