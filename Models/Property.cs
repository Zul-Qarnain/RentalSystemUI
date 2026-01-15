using System;

namespace RentalSystemUI.Models
{
    public class Property
    {
        public int PropertyID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public decimal RentAmount { get; set; }
        public string Status { get; set; } = "Available";
        public string Description { get; set; } = string.Empty;
        public int LandlordID { get; set; }

        // Optional Computed or Detail properties (not strictly in table but useful)
        public string CoverImage { get; set; } = string.Empty; 
        public System.Collections.Generic.List<string> ImagePaths { get; set; } = new System.Collections.Generic.List<string>();
    }
}
