using RestaurantReservationApp.Data;
using RestaurantReservationApp.Interface;
using RestaurantReservationApp.Models;

namespace RestaurantReservationApp.Repository
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DataContext context) : base(context)
        {
        }

        // Implement any customer-specific methods here
    }

}
