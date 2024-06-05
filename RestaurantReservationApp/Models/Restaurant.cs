using System.ComponentModel.DataAnnotations;

namespace RestaurantReservationApp.Models
{
    public class Restaurant
    {
        public string Id { get; set; } = new Guid().ToString();

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        public string OpeningHours { get; set; }

        [Range(1, int.MaxValue)]
        public int Seats { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public User Owner { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
