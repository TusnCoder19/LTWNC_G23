// IHouseRepository.cs
using G23NHNT.Models;
using G23NHNT.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace G23NHNT.Repositories
{
    public interface IHouseRepository
    {
        Task<IEnumerable<House>> GetAllHousesAsync(string searchString);
        Task<House> GetHouseWithDetailsAsync(int id);
        Task AddAsync(House house);
        Task UpdateAsync(House house);
        Task DeleteAsync(int id);
        //Task<IEnumerable<House>> GetHousesByCategoryAsync(string category);
        Task<List<House>> GetHousesByUserId(int userId, int? userRole);
        Task<IEnumerable<House>> GetFilteredHousesAsync(string searchString, string priceRange, string sortBy, string roomType, List<string> amenities);
        Task UpdateAmenitiesAsync(int houseId, List<int> amenityIds);
    }
}
