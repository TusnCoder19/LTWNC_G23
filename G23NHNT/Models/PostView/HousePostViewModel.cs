using G23NHNT.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace G23NHNT.ViewModels
{
    public class HousePostViewModel
    {
        public House House { get; set; } = new House();
        public HouseDetail HouseDetail { get; set; } = new HouseDetail();
        public List<Amenity> Amenities { get; set; } = new List<Amenity>();
        public List<int> SelectedAmenities { get; set; } = new List<int>();
        public int SelectedHouseType { get; set; }
        public IEnumerable<SelectListItem> HouseTypes { get; set; } = new List<SelectListItem>();
    }
}
