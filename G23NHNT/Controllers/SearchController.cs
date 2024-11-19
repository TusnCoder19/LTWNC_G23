using G23NHNT.Models;
using G23NHNT.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace G23NHNT.Controllers
{
    public class SearchController : Controller
    {

        private readonly ISearchRepository _searchRepository;
        private readonly IAmenityRepository _amenityRepository;
        private readonly IHouseTypeRepository _houseTypeRepository;

        public SearchController(ISearchRepository searchRepository, IAmenityRepository amenityRepository, IHouseTypeRepository houseTypeRepository)
        {
            _searchRepository = searchRepository;
            _amenityRepository = amenityRepository;
            _houseTypeRepository = houseTypeRepository;
        }

        public async Task<IActionResult> Index(string searchString, string priceRange, string sortBy, string roomType, List<string> amenities)
        {
            // Get the amenities and room types from the repository
            var amenitiesList = (await _amenityRepository.GetAllAmenitiesAsync()).ToList();
            var houseTypesList = (await _houseTypeRepository.GetAllHouseTypes()).Select(ht => new SelectListItem
            {
                Value = ht.IdHouseType.ToString(),
                Text = ht.Name
            }).ToList();

            // ViewBag values for the filters
            ViewBag.Keyword = searchString;
            ViewBag.PriceRange = priceRange;
            ViewBag.SortBy = sortBy;
            ViewBag.RoomType = roomType;
            ViewBag.SelectedAmenities = amenities;

            var houses = await _searchRepository.GetFilteredHousesAsync(searchString, priceRange, sortBy, roomType, amenities);

            var viewModel = new SearchViewModel
            {
                Houses = houses,
                IsChuTro = User.IsInRole("ChuTro"),
                Amenities = amenitiesList,
                HouseTypes = houseTypesList
            };

            return View(viewModel);
        }

    }
}

