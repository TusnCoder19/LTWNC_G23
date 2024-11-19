// HouseRepository.cs
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
        public async Task<IEnumerable<House>> GetFilteredHousesAsync(string searchString, string priceRange, string sortBy, string roomType, List<string> amenities)
        {
            var query = _context.Houses
                .Include(h => h.HouseDetails)
                .Include(h => h.HouseType)
                .Include(h => h.IdAmenities)
                .Include(h => h.IdUserNavigation)
                .AsQueryable();

            // Search by keyword
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(h => h.NameHouse.Contains(searchString) || h.PostTitle.Contains(searchString) || h.HouseDetails.Any(hd =>
                    hd.Address.Contains(searchString) || hd.Describe.Contains(searchString)));
            }

            // Filter by price range
            if (!string.IsNullOrEmpty(priceRange))
            {
                var ranges = priceRange.Split('-');
                if (ranges.Length == 2 && int.TryParse(ranges[0], out int minPrice) && int.TryParse(ranges[1], out int maxPrice))
                {
                    query = query.Where(h => h.HouseDetails.Any(hd => hd.Price >= minPrice && hd.Price <= maxPrice));
                }
                else if (ranges.Length == 1 && int.TryParse(ranges[0], out int minOnlyPrice)) // For "Trên 10 triệu"
                {
                    query = query.Where(h => h.HouseDetails.Any(hd => hd.Price >= minOnlyPrice));
                }
            }

            // Filter by room type
            if (!string.IsNullOrEmpty(roomType))
            {
                Console.WriteLine($"Filtering by room type: {roomType}");

                var houseTypes = query.Select(h => h.HouseType.Name).ToList();
                Console.WriteLine("Available room types:");
                foreach (var type in houseTypes)
                {
                    Console.WriteLine(type);
                }

                query = query.Where(h => h.HouseType.Name.ToLower() == roomType.ToLower());
            }

            // Filter by amenities
            if (amenities != null && amenities.Any())
            {
                query = query.Where(h => h.IdAmenities.Any(a => amenities.Contains(a.Name)));
            }

            // Sorting
            if (!string.IsNullOrEmpty(sortBy))
            {
                query = sortBy switch
                {
                    "priceLowHigh" => query.OrderBy(h => h.HouseDetails.Min(hd => hd.Price)),
                    "priceHighLow" => query.OrderByDescending(h => h.HouseDetails.Max(hd => hd.Price)),
                    _ => query.OrderByDescending(h => h.IdHouse) // Default to newest
                };
            }

            return await query.ToListAsync();
        }
    }
}
