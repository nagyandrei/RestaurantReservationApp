using RestaurantReservationApp.Data;
using RestaurantReservationApp.Interface;
using RestaurantReservationApp.Models;

namespace RestaurantReservationApp.Repository
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        public ReviewRepository(DataContext context) : base(context)
        {
        }

        // Implement any review-specific methods here
    }

}
