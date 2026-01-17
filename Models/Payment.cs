using System;

namespace RentalSystemUI.Models
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public int BookingID { get; set; }
        public int TenantID { get; set; }
        public int PropertyID { get; set; }
        public decimal Amount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string Status { get; set; } = "Verified"; // Verified, Failed
        public string TransactionID { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;

        // Navigation / View Properties
        public string TenantName { get; set; } = string.Empty;
        public string PropertyTitle { get; set; } = string.Empty;
    }
}
