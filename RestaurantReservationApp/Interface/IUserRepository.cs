using RestaurantReservationApp.Models;

namespace RestaurantReservationApp.Interface
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<bool> ExistsByEmailAddressAsync(string email);
        Task<bool> ExistsByUsernameAsync(string name);
        Task<User> GetUserByEmailAndPasswordAsync(string username, string password);
    }

}
