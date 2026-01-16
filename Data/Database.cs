using System;
using System.Data.SqlClient; // Note: Prompt asked for Microsoft.Data.SqlClient but sometimes System.Data.SqlClient is used in older, but we should use Microsoft.Data.SqlClient if available. 
// Checking existing code: PropertyDetails.cs uses Microsoft.Data.SqlClient.
// DatabaseHelper.cs uses Microsoft.Data.SqlClient.
using Microsoft.Data.SqlClient;
using DotNetEnv;

namespace RentalSystemUI.Data
{
    public class Database
    {
        public Database()
        {
            // Load .env if not already loaded (safe to call multiple times or handle static init)
            // Use overwrite to ensure .env value wins over any machine-level variable pointing to LocalDB.
            Env.Load();
        }

        public SqlConnection GetConnection()
        {
            // Prefer the value from .env (via DotNetEnv) first, then fall back to process env
            var connString = Env.GetString("DB_CONNECTION_STRING");
            if (string.IsNullOrWhiteSpace(connString))
            {
                connString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            }

            if (string.IsNullOrWhiteSpace(connString))
            {
                throw new InvalidOperationException("DB_CONNECTION_STRING is missing in .env layer");
            }
            return new SqlConnection(connString);
        }
    }
}
