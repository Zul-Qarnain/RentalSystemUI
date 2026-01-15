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
            // Ideally DotNetEnv.Env.Load() should be called at App startup (Program.cs), but here is safe for now.
            Env.Load();
        }

        public SqlConnection GetConnection()
        {
            string connString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") 
                                ?? throw new InvalidOperationException("DB_CONNECTION_STRING is missing in .env layer");
            return new SqlConnection(connString);
        }
    }
}
