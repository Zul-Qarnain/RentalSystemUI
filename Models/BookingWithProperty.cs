using System;

namespace RentalSystemUI.Models
{
    public class BookingWithProperty : Booking
    {
        public string PropertyTitle { get; set; } = string.Empty;
        public string PropertyAddress { get; set; } = string.Empty;
        public decimal MonthlyRent { get; set; }

        public string TenantName { get; set; } = string.Empty;
    }
}
