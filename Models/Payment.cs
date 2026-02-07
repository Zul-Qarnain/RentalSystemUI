using System;

namespace RentalSystemUI.Models
{
    // MODEL CLASS: Defines the structure of a Payment object. Does not contain logic.
    public class Payment
    {
        // Properties map directly to database columns.
        public int PaymentID { get; set; }
        public int BookingID { get; set; }
        public int TenantID { get; set; }
        public int PropertyID { get; set; }
        public decimal Amount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string Status { get; set; } = "Verified"; // Verified, Failed
        public string TransactionID { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;

        // Navigation / View Properties (for display only, not stored in Payment table)
        public string TenantName { get; set; } = string.Empty;
        public string PropertyTitle { get; set; } = string.Empty;
    }
}
