using G23NHNT.Models;
using G23NHNT.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace G23NHNT.Repositories
{
    public class HouseRepository : IHouseRepository
    {
        private readonly G23_NHNTContext _context;
        private readonly ILogger<HouseRepository> _logger;

        public HouseRepository(G23_NHNTContext context, ILogger<HouseRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<House>> GetAllHousesAsync(string searchString)
        {
            return await _context.Houses
                .Include(h => h.HouseDetails)
                .Include(h => h.HouseType)
                .Include(h => h.AmenityIds) // Ensure the AmenityIds property is included
                .Include(h => h.IdUserNavigation)
                .Where(h => string.IsNullOrEmpty(searchString) ||
                    h.HouseDetails.Any(hd => hd.Address.Contains(searchString) ||
                                             hd.Describe.Contains(searchString)))
                .ToListAsync();
        }

        public async Task<House> GetHouseWithDetailsAsync(int id)
        {
            return await _context.Houses
               .Include(h => h.HouseDetails)
               .Include(h => h.HouseType)
               .Include(h => h.IdUserNavigation)
               .Include(h => h.Reviews)
                   .ThenInclude(review => review.IdUserNavigation)
               .FirstOrDefaultAsync(h => h.IdHouse == id);
        }

        public async Task AddAsync(House house)
        {
            try
            {
                await _context.Houses.AddAsync(house);
                int affectedRows = await _context.SaveChangesAsync();

                if (affectedRows == 0)
                {
                    _logger.LogWarning("No records were added to the database.");
                }
                else
                {
                    _logger.LogInformation($"Successfully added {affectedRows} records to the database.");
                }
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError($"Database update error: {dbEx.Message}");
                _logger.LogError(dbEx.StackTrace);
            }
            catch (Exception ex)
            {
                _logger.LogError($"General error adding house: {ex.Message}");
                _logger.LogError(ex.StackTrace);
            }
        }

        public async Task UpdateAsync(House house)
        {
            // Check if the entity is detached, and if so, attach it
            if (_context.Entry(house).State == EntityState.Detached)
            {
                _context.Houses.Attach(house);
            }

            // Mark the entity as modified
            _context.Entry(house).State = EntityState.Modified;

            // Save changes to the database
            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                _logger.LogWarning("No rows affected during the house update.");
            }
        }

        public async Task DeleteAsync(int id)
        {
            var house = await _context.Houses.FindAsync(id);
            if (house != null)
            {
                _context.Houses.Remove(house);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<House>> GetHousesByUserId(int userId)
        {
            return await _context.Houses
                            .Include(h => h.HouseDetails)
                           .Where(h => h.IdUser == userId)
                           .ToListAsync();
        }

        public async Task UpdateAmenitiesAsync(int houseId, List<int> amenityIds)
        {
            var house = await _context.Houses.FirstOrDefaultAsync(h => h.IdHouse == houseId);

            if (house != null)
            {
                // Update AmenityIds
                house.AmenityIdsArray = amenityIds;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"An error occurred while updating amenities for House ID {houseId}: {ex.Message}");
                    throw new InvalidOperationException("Error occurred while saving the updated amenities.");
                }
            }
            else
            {
                _logger.LogWarning($"House with ID {houseId} not found.");
                throw new KeyNotFoundException($"House with ID {houseId} not found.");
            }
        }

        public Task<IEnumerable<House>> GetFilteredHousesAsync(string searchString, string priceRange, string sortBy, string roomType, List<string> amenities)
        {
            throw new NotImplementedException();
        }
    }
}
