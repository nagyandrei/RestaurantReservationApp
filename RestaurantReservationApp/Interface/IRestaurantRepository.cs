using RestaurantReservationApp.Models;

namespace RestaurantReservationApp.Interface
{
    public interface IRestaurantRepository : IGenericRepository<Restaurant>
    {
        // Add any restaurant-specific methods here
        Task<List<Restaurant>> GetAllByOwnerId(string ownerId);
    }


}
