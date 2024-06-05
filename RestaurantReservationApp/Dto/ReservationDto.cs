namespace RestaurantReservationApp.Dto
{
    public class ReservationDto
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string RestaurantId { get; set; }
        public DateTime ReservationDate { get; set; }
        public int NumberOfGuests { get; set; }
        public string ReservationStatus { get; set; }
        public CustomerDto Customer { get; set; }
        public RestaurantDto Restaurant { get; set; }
    }
}
