using System.ComponentModel.DataAnnotations;

namespace RestaurantReservationApp.Models
{
    public class Role
    {
        public string Id { get; set; } = new Guid().ToString();

        [Required]
        [StringLength(50)]
        public string RoleName { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
