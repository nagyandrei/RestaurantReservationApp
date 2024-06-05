using System.ComponentModel.DataAnnotations;

namespace RestaurantReservationApp.Models
{
    public class Review
    {
        public string Id { get; set; } = new Guid().ToString();

        [Required]
        public string RestaurantId { get; set; }

        [Required]
        public string CustomerId { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [StringLength(1000)]
        public string Comment { get; set; }

        public DateTime ReviewDate { get; set; } = DateTime.Now;
        public Restaurant Restaurant { get; set; }
        public Customer Customer { get; set; }
    }
}
