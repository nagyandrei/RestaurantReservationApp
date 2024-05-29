namespace RestaurantReservationApp.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string RestaurantId { get; set; }
        public string CustomerId { get; set; }
        public int Raiting { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
