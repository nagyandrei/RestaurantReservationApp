namespace RestaurantReservationApp.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public string RestaurantId { get; set; }
        public DateTime ReservationDate { get; set; }
        public int NumberOfGuests { get; set; }
        public string ReservationStatus { get; set; }
    }
}
