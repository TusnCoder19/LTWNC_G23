using G23NHNT.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace G23NHNT.Models
{
    public class SearchViewModel
    {
        public IEnumerable<House> Houses { get; set; }
        public bool IsChuTro { get; set; }
        public List<Amenity> Amenities { get; set; } = new List<Amenity>();
        public IEnumerable<SelectListItem> HouseTypes { get; set; } = new List<SelectListItem>();

    }
}
