using System.ComponentModel.DataAnnotations;

namespace RestaurantReservationApp.Models
{
    public class Reservation
    {
        public string Id { get; set; } = new Guid().ToString();

        [Required]
        public string CustomerId { get; set; }

        [Required]
        public string RestaurantId { get; set; }

        [Required]
        public DateTime ReservationDate { get; set; }

        [Range(1, int.MaxValue)]
        public int NumberOfGuests { get; set; }

        [Required]
        [StringLength(50)]
        public string ReservationStatus { get; set; }

        public Customer Customer { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
