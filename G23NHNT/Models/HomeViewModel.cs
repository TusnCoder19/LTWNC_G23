// HomeViewModel.cs
using G23NHNT.Models;
using System.Collections.Generic;

namespace G23NHNT.Models
{
    public class HomeViewModel
    {
        public IEnumerable<HouseType>? HouseTypes { get; set; }
        public HouseType? SelectedHouseType { get; set; }    

        public IEnumerable<House> Houses { get; set; }
        public bool IsChuTro { get; set; }
    }
}
