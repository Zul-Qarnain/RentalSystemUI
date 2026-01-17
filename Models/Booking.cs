using System;

namespace RentalSystemUI.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        public int PropertyID { get; set; }
        public int TenantID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? DurationMonths { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected
        public DateTime CreatedAt { get; set; }
    }
}
