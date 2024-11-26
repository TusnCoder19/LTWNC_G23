using G23NHNT.Models;
using G23NHNT.Repositories;
using G23NHNT.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace G23NHNT.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHouseRepository _houseRepository;
        private readonly IAmenityRepository _amenityRepository;

        public HomeController(IHouseRepository houseRepository, IAmenityRepository amenityRepository)
        {
            _houseRepository = houseRepository;
            _amenityRepository = amenityRepository;

        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Detail(int id)
        {

            var house = await _houseRepository.GetHouseWithDetailsAsync(id);
            var amenities = await _amenityRepository.GetAmenitiesByIdsAsync(house.AmenityIdsArray);
            var viewModel = new HouseDetailViewModel
            {
                Amenities = amenities,
                House = house,
            };
            if (house != null)
            {
                return View("HouseDetails", viewModel);
            }
            return NotFound();
        }

    }
}
