using G23NHNT.Models;
using G23NHNT.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using G23NHNT.ViewModels;
using G23NHNT.Repository.House;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace G23NHNT.Controllers
{
    public class QuanLiController : Controller
    {
        private readonly ILogger<QuanLiController> _logger;
        private readonly IHouseRepository _houseRepository;
        private readonly IHouseDetailRepository _houseDetailRepository;
        private readonly IHouseTypeRepository _houseTypeRepository;
        private readonly IAmenityRepository _amenityRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public QuanLiController(ILogger<QuanLiController> logger,IHouseRepository houseRepository, IHouseDetailRepository houseDetailRepository,
            IHouseTypeRepository houseTypeRepository, IAmenityRepository amenityRepository, IWebHostEnvironment webHostEnvironment)
        {
            _houseRepository = houseRepository;
            _houseDetailRepository = houseDetailRepository;
            _houseTypeRepository = houseTypeRepository;
            _amenityRepository = amenityRepository;
             _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> ListHouseRoom()
        {
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
                string userName = HttpContext.Session.GetString("UserName") ?? "";
                int? userRole = HttpContext.Session.GetInt32("Role");

                var houses = await _houseRepository.GetHousesByUserId(userId, userRole);

                // Sử dụng HomeViewModel
                var viewModel = new HomeViewModel
                {
                    Houses = houses,
                    IsChuTro = true
                };

                ViewBag.UserId = userId;
                ViewBag.UserName = userName;

                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditHouse(int id)
        {
            var house = await _houseRepository.GetHouseWithDetailsAsync(id);
            if (house == null)
            {
                return NotFound();
            }

            var viewModel = new HousePostViewModel
            {
                House = house,
                HouseDetail = house.HouseDetails,
                SelectedAmenities = house.AmenityIdsArray,
                Amenities = (await _amenityRepository.GetAllAmenitiesAsync()).ToList(),
                SelectedHouseType = house.HouseTypeId, // Lấy loại nhà hiện tại
                HouseTypes = (await _houseTypeRepository.GetAllHouseTypes())
                             .Select(ht => new SelectListItem
                             {
                                 Value = ht.IdHouseType.ToString(),
                                 Text = ht.Name,
                                 Selected = ht.IdHouseType == house.HouseTypeId
                             }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditHouse(HousePostViewModel viewModel)
        {
            // If validation fails, return view with error
            if (!ModelState.IsValid)
            {
                // Collect all validation errors from ModelState
                var errors = ModelState
                    .Where(ms => ms.Value.Errors.Any())
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                // Add a general error to the ModelState if needed
                ModelState.AddModelError(string.Empty, "There are some validation errors. Please check the form and try again.");

                // Re-populate viewModel data to show the dropdowns and fields again
                viewModel.Amenities = (await _amenityRepository.GetAllAmenitiesAsync()).ToList();
                viewModel.HouseTypes = (await _houseTypeRepository.GetAllHouseTypes())
                                       .Select(ht => new SelectListItem
                                       {
                                           Value = ht.IdHouseType.ToString(),
                                           Text = ht.Name
                                       }).ToList();

                // Return the view with the model, validation errors, and populated dropdowns
                return Json(new { success = false, errors });
            }

            // Fetch the existing house and associated details
            var existingHouse = await _houseRepository.GetHouseWithDetailsAsync(viewModel.House.IdHouse);
            if (existingHouse == null)
            {
                return NotFound("House not found.");
            }

            var existingHouseDetail = existingHouse.HouseDetails;
            if (existingHouseDetail == null)
            {
                return NotFound("House details not found.");
            }

            try
            {
                // Update House fields
                await _houseRepository.UpdateAmenitiesAsync(viewModel.House.IdHouse, viewModel.SelectedAmenities);
                existingHouse.NameHouse = !string.IsNullOrEmpty(viewModel.House.NameHouse)
                    ? viewModel.House.NameHouse
                    : existingHouse.NameHouse;
                existingHouse.HouseTypeId = viewModel.SelectedHouseType;
                // Update HouseDetail fields
                existingHouseDetail.Address = !string.IsNullOrEmpty(viewModel.HouseDetail.Address)
                    ? viewModel.HouseDetail.Address
                    : existingHouseDetail.Address;
                existingHouseDetail.Price = viewModel.HouseDetail.Price > 0
                    ? viewModel.HouseDetail.Price
                    : existingHouseDetail.Price;
                existingHouseDetail.DienTich = viewModel.HouseDetail.DienTich > 0
                    ? viewModel.HouseDetail.DienTich
                    : existingHouseDetail.DienTich;
                existingHouseDetail.TienDien = !string.IsNullOrEmpty(viewModel.HouseDetail.TienDien)
                    ? viewModel.HouseDetail.TienDien
                    : existingHouseDetail.TienDien;
                existingHouseDetail.TienNuoc = !string.IsNullOrEmpty(viewModel.HouseDetail.TienNuoc)
                    ? viewModel.HouseDetail.TienNuoc
                    : existingHouseDetail.TienNuoc;
                existingHouseDetail.TienDv = !string.IsNullOrEmpty(viewModel.HouseDetail.TienDv)
                    ? viewModel.HouseDetail.TienDv
                    : existingHouseDetail.TienDv;
                existingHouseDetail.Describe = !string.IsNullOrEmpty(viewModel.HouseDetail.Describe)
                    ? viewModel.HouseDetail.Describe
                    : existingHouseDetail.Describe;
                existingHouseDetail.Status = !string.IsNullOrEmpty(viewModel.HouseDetail.Status)
                    ? viewModel.HouseDetail.Status
                    : existingHouseDetail.Status;
                existingHouseDetail.PhoneNumber = !string.IsNullOrEmpty(viewModel.HouseDetail.PhoneNumber)
                    ? viewModel.HouseDetail.PhoneNumber
                    : existingHouseDetail.PhoneNumber;

                _logger.LogInformation($"NEW IMAGE FILE: {viewModel.HouseDetail.ImageFile}");

                // Handle image upload if a new file is provided
                if (viewModel.HouseDetail.ImageFile != null && viewModel.HouseDetail.ImageFile.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img", "houses");

                    // Ensure the uploads folder exists
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    // Generate a unique file name
                    string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(viewModel.HouseDetail.ImageFile.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // Save the image file to the server
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await viewModel.HouseDetail.ImageFile.CopyToAsync(fileStream);
                    }

                    // Update the image path for the house detail
                    existingHouseDetail.Image = Path.Combine("/img", "houses", uniqueFileName).Replace("\\", "/");
                    _logger.LogInformation($"New Image Path: {existingHouseDetail.Image}");
                }

                // Save updated data to the database
                await _houseRepository.UpdateAsync(existingHouse);
                await _houseDetailRepository.UpdateAsync(existingHouseDetail);

                // Log success
                _logger.LogInformation($"House with ID {viewModel.House.IdHouse} updated successfully.");

                // Redirect to ListHouseRoom action
                return RedirectToAction("ListHouseRoom");
            }
            catch (Exception ex)
            {
                // Log error and add model error
                _logger.LogError($"An error occurred while updating the house: {ex.Message}");
                ModelState.AddModelError(string.Empty, $"An error occurred while updating the house: {ex.Message}");

                // Re-populate dropdown data for redisplay in case of an error
                viewModel.Amenities = (await _amenityRepository.GetAllAmenitiesAsync()).ToList();
                viewModel.HouseTypes = (await _houseTypeRepository.GetAllHouseTypes())
                                       .Select(ht => new SelectListItem
                                       {
                                           Value = ht.IdHouseType.ToString(),
                                           Text = ht.Name
                                       }).ToList();

                return View(viewModel);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteHouse(int id)
        {
            try
            {
                var house = await _houseRepository.GetHouseWithDetailsAsync(id);
                if (house == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy nhà trọ!" });
                }

                await _houseRepository.DeleteAsync(id);

                return Json(new { success = true, message = "Xóa nhà trọ và các liên kết liên quan thành công!" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting house: {ex.Message}");
                return Json(new { success = false, message = "Đã xảy ra lỗi khi xóa nhà trọ!" });
            }
        }
    }
}
