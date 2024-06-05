using System.ComponentModel.DataAnnotations;

namespace RestaurantReservationApp.Models
{
    public class Customer
    {
        public string Id { get; set; } = new Guid().ToString();

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
