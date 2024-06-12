using Microsoft.EntityFrameworkCore;
using RestaurantReservationApp.Data;
using RestaurantReservationApp.Interface;
using RestaurantReservationApp.Models;

namespace RestaurantReservationApp.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> ExistsByEmailAddressAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email.Equals(email));
        }

        public async Task<bool> ExistsByUsernameAsync(string name)
        {
            return await _context.Users.AnyAsync(u => u.Name.Equals(name));
        }

        public async Task<User> GetUserByEmailAndPasswordAsync(string username, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(r => r.Name.Equals(username) && r.Password.Equals(password));
        }

        // Implement any user-specific methods here
    }

}
