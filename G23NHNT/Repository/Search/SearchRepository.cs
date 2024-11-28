using G23NHNT.Models;
using G23NHNT.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G23NHNT.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        private readonly G23_NHNTContext _context;

        public SearchRepository(G23_NHNTContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<House>> GetFilteredHousesAsync(string searchString, string priceRange, string sortBy, string roomType, string[] amenities)
        {
            var query = _context.Houses
                .Include(h => h.HouseDetails)  // Include HouseDetails for filtering
                .Include(h => h.HouseType)    // Include HouseType for filtering by room type
                .Include(h => h.IdUserNavigation)  // Include user info (if necessary)
                .AsQueryable();

            // Search by keyword
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(h => h.NameHouse.Contains(searchString) || h.PostTitle.Contains(searchString) || h.HouseDetails.Address.Contains(searchString) || h.HouseDetails.Describe.Contains(searchString));
            }

            // Filter by price range
            if (!string.IsNullOrEmpty(priceRange))
            {
                var ranges = priceRange.Split('-');
                if (ranges.Length == 2 && int.TryParse(ranges[0], out int minPrice) && int.TryParse(ranges[1], out int maxPrice))
                {
                    query = query.Where(h => h.HouseDetails.Price >= minPrice && h.HouseDetails.Price <= maxPrice);

                }
                else if (ranges.Length == 1 && int.TryParse(ranges[0], out int minOnlyPrice)) // For "Trên 10 triệu"
                {
                    query = query.Where(h => h.HouseDetails.Price >= minOnlyPrice);

                }
            }

            // Filter by room type
            if (!string.IsNullOrEmpty(roomType))
            {
                query = query.Where(h => h.HouseType.Name.ToLower() == roomType.ToLower());
            }

            // Sorting

            query = sortBy switch
            {
                "priceLowHigh" => query.OrderBy(h => h.HouseDetails.Price),
                "priceHighLow" => query.OrderByDescending(h => h.HouseDetails.Price),
                "newest" => query.OrderByDescending(h => h.HouseDetails.TimePost),
                _ => query.OrderByDescending(h => h.HouseDetails.TimePost),
            };

            var housesList = await query.ToListAsync();
            if (amenities != null && amenities.Any())
            {
                housesList = housesList
                    .Where(h => amenities.All(a => h.AmenityIdsArray.Contains(int.Parse(a))))  // Check if house contains all amenities
                    .ToList();
            }
            return housesList;
        }
    }
}
