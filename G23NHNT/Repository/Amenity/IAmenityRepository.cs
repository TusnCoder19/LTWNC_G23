using G23NHNT.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace G23NHNT.Repositories
{
    public interface IAmenityRepository
    {
        Task<IEnumerable<Amenity>> GetAllAmenitiesAsync();
        Task<Amenity> GetAmenityByIdAsync(int id);
        Task AddAsync(Amenity amenity);
        Task UpdateAsync(Amenity amenity);
        Task DeleteAsync(int id);
        Task<List<Amenity>> GetAmenitiesByIdsAsync(List<int> amenityIds);
    }
}
