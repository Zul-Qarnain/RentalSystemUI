using System.Collections.Generic;
using System.Linq;
using RentalSystemUI.Data;
using RentalSystemUI.Models;

namespace RentalSystemUI.Services
{
    public class PropertyService
    {
        private PropertyRepository _propRepo = new PropertyRepository();

        public List<Property> GetSearchProperties()
        {
            return _propRepo.GetAllAvailable();
        }

        public Property? GetPropertyDetails(int id)
        {
            var prop = _propRepo.GetById(id);
            if (prop == null) return null;

            // Populate images
            var images = _propRepo.GetImagesByPropertyId(id);
            prop.ImagePaths = images.Select(i => i.ImagePath).ToList();

            return prop;
        }
    }
}
