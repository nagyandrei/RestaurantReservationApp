using System.ComponentModel.DataAnnotations;

namespace RestaurantReservationApp.Models
{
    public class User
    {
        public string Id { get; set; } = new Guid().ToString();

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string RoleId { get; set; }
        public Role Role { get; set; }
    }
}
