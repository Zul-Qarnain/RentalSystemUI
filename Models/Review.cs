using System;

namespace RentalSystemUI.Models
{
    public class Review
    {
        public int ReviewID { get; set; }
        public int PropertyID { get; set; }
        public int TenantID { get; set; }
        public int Rating { get; set; } // 1-5
        public string Comment { get; set; } = string.Empty;
        public string Reply { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public bool IsResolved { get; set; }

        // Navigation / View Properties
        public string TenantName { get; set; } = string.Empty;
    }
}
