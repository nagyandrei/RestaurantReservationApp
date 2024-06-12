using RestaurantReservationApp.Models;

namespace RestaurantReservationApp.Interface
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<Role> GetRoleByNameAsync(string roleName);
    }

}
