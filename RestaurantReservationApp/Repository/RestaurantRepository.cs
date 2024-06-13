using Microsoft.EntityFrameworkCore;
using RestaurantReservationApp.Data;
using RestaurantReservationApp.Interface;
using RestaurantReservationApp.Models;

namespace RestaurantReservationApp.Repository
{
    public class RestaurantRepository : GenericRepository<Restaurant>, IRestaurantRepository
    {
        private readonly DataContext _context;
        public RestaurantRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Restaurant>> GetAllByOwnerId(string ownerId)
        {
            return await _context.Restaurants.Where(r => r.OwnerId.Equals(ownerId)).ToListAsync();
        }
    }

}
