using System;

namespace RentalSystemUI.Models
{
    public class PropertyImage
    {
        public int ImageID { get; set; }
        public int PropertyID { get; set; }
        public string ImagePath { get; set; } = string.Empty;
    }
}
