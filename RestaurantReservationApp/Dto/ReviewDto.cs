namespace RestaurantReservationApp.Dto
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string RestaurantId { get; set; }
        public string CustomerId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }
        public RestaurantDto Restaurant { get; set; }
        public CustomerDto Customer { get; set; }
    }
}
