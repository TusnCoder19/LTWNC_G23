// IHouseRepository.cs
using G23NHNT.Models;
using G23NHNT.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace G23NHNT.Repositories
{
    public interface ISearchRepository
    {
        Task<IEnumerable<House>> GetFilteredHousesAsync(string searchString, string priceRange, string sortBy, string roomType, List<string> amenities);
    }
}
