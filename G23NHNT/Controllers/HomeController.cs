using G23NHNT.Models;
using G23NHNT.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace G23NHNT.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHouseRepository _houseRepository;
        private readonly IHouseTypeRepository _houseTypeRepository;
        private readonly G23_NHNTContext _context;

        public HomeController(IHouseRepository houseRepository, IHouseTypeRepository houseTypeRepository, G23_NHNTContext context)
        {
            _houseRepository = houseRepository;
            _houseTypeRepository = houseTypeRepository;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var houses = await _houseRepository.GetAllHousesAsync();
            var houseTypes = await _houseTypeRepository.GetAllHouseTypes();
            var viewModel = new HomeViewModel
            {
                Houses = houses,
                HouseTypes = houseTypes,
                IsChuTro = User.IsInRole("ChuTro"),
            };

            return View(viewModel);
        }

        public async Task<IActionResult> HousesByType(int id)
        {
            var houses = await _houseRepository.GetHousesByTypeAsync(id);

            if (houses == null || !houses.Any())
            {
                return NotFound(); 
            }
            var selectedHouseType = await _context.HouseType.FirstOrDefaultAsync(ht => ht.IdHouseType == id);
            if (selectedHouseType == null)
            {
                return NotFound();
            }
            var houseTypes = await _houseTypeRepository.GetAllHouseTypes();

            var viewModel = new HomeViewModel
            {
                Houses = houses,
                HouseTypes = houseTypes,
                SelectedHouseType = selectedHouseType,
                IsChuTro = User.IsInRole("ChuTro"),
            };
            
            return View("HousesListPartial", viewModel);
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
