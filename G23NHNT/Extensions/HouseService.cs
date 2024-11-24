using G23NHNT.Models;
using G23NHNT.Repositories;
using G23NHNT.ViewModels;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace G23NHNT.Services
{
    public class HouseService
    {
        private readonly IHouseRepository _houseRepository;
        private readonly IHouseDetailRepository _houseDetailRepository;
        private readonly IAmenityRepository _amenityRepository;

        public HouseService(IHouseRepository houseRepository, IHouseDetailRepository houseDetailRepository, IAmenityRepository amenityRepository)
        {
            _houseRepository = houseRepository;
            _houseDetailRepository = houseDetailRepository;
            _amenityRepository = amenityRepository;
        }

        public async Task<bool> CreateHouseAsync(HousePostViewModel model)
        {
            try
            {
                // Create the House entity
                await _houseRepository.AddAsync(model.House);

                // Link the HouseDetail entity
                model.HouseDetail.IdHouse = model.House.IdHouse;
                await _houseDetailRepository.AddAsync(model.HouseDetail);

                // Add selected amenity IDs to the house
                model.House.AmenityIdsArray = model.SelectedAmenities;
       

               // Update the House entity with the selected amenity IDs
               await _houseRepository.UpdateAsync(model.House);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating house: {ex.Message}");
                return false;
            }
        }
    }
}
