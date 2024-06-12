using Microsoft.EntityFrameworkCore;
using RestaurantReservationApp.Data;
using RestaurantReservationApp.Interface;
using RestaurantReservationApp.Models;

namespace RestaurantReservationApp.Repository
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        private readonly DataContext _context;
        public RoleRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Role> GetRoleByNameAsync(string roleName)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.RoleName.Equals(roleName));
        }

    }

}
