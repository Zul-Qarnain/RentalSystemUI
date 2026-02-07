using System;

namespace RentalSystemUI.Models
{
    public class RefundRequestModel
    {
        public int RefundRequestID { get; set; }
        public int BookingID { get; set; }
        public string? Status { get; set; }
        public string? Reason { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Amount { get; set; }
        public string? TenantName { get; set; }
        public string? PropertyTitle { get; set; }
    }
}
