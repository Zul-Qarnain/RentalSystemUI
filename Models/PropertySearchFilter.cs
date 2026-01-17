using System;

namespace RentalSystemUI.Models
{
    public class PropertySearchFilter
    {
        public string? SearchText { get; set; }
        public decimal? MaxMonthlyRent { get; set; }

        public int? Bedrooms { get; set; }
        public int? Washrooms { get; set; }
        public int? Corridors { get; set; }
        public int? Kitchens { get; set; }

        public int? MinSquareFeet { get; set; }

        public bool? RequiresAC { get; set; }
        public bool? PetFriendly { get; set; }

        // present in UI but not currently stored in DB schema used by PropertyService; keep for future.
        public bool? WasherDryer { get; set; }
        public bool? ParkingSpot { get; set; }

        public bool OnlyAvailable { get; set; } = true;

        public static PropertySearchFilter Default => new PropertySearchFilter();
    }
}
