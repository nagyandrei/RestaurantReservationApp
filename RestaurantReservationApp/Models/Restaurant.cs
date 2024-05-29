namespace RestaurantReservationApp.Models
{
    public class Restaurant
    {
        public string Id { get; set; } = new Guid().ToString();
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string OppeningHours { get; set; }
        public int Seats { get; set; }
    }
}
