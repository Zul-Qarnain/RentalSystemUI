using System;

namespace RentalSystemUI.Models
{
    public class Application
    {
        public int ApplicationID { get; set; }
        public int PropertyID { get; set; }
        public int TenantID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string Status { get; set; } = "Pending"; // Pending, Accepted, Rejected
        public string Message { get; set; } = string.Empty;

        // Navigation / View Properties
        public string TenantName { get; set; } = string.Empty;
        public string PropertyTitle { get; set; } = string.Empty;
    }
}
