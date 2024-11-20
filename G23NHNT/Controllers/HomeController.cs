// HomeController.cs
using G23NHNT.Models;
using G23NHNT.Repositories;
using G23NHNT.Repository.House;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using System.Threading.Tasks;

namespace G23NHNT.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHouseRepository _houseRepository;
        private readonly IAmenityRepository _amenityRepository;
        private readonly IHouseTypeRepository _houseTypeRepository;

        public HomeController(IHouseRepository houseRepository, IAmenityRepository amenityRepository, IHouseTypeRepository houseTypeRepository)
        {
            _houseRepository = houseRepository;
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

            var houses = await _houseRepository.GetFilteredHousesAsync(searchString, priceRange, sortBy, roomType, amenities);

            var viewModel = new HomeViewModel
            {
                Houses = houses,
                IsChuTro = User.IsInRole("ChuTro"),
                Amenities = amenitiesList,
                HouseTypes = houseTypesList
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Main()
        {
            return View();
        }

        public async Task<IActionResult> Detail(int id)
        {

            var house = await _houseRepository.GetHouseWithDetailsAsync(id);
            if (house != null)
            {
                return View("HouseDetails", house);
            }
            return NotFound();
        }

    }
}
