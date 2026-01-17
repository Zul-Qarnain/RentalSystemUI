using System;

namespace RentalSystemUI.Services
{
    /// <summary>
    /// Global database connection state manager.
    /// Shared across all services to prevent repeated connection attempts.
    /// </summary>
    public static class DatabaseState
    {
        /// <summary>
        /// If true, database connection has failed and all services should use demo data.
        /// </summary>
        public static bool ConnectionFailed { get; set; } = false;

        /// <summary>
        /// Marks the connection as failed. Called by any service that encounters a DB error.
        /// </summary>
        public static void MarkFailed()
        {
            ConnectionFailed = true;
            System.Diagnostics.Debug.WriteLine("Database connection marked as failed - using demo data");
        }

        /// <summary>
        /// Resets the connection state (for retry logic if needed).
        /// </summary>
        public static void Reset()
        {
            ConnectionFailed = false;
        }
    }
}
