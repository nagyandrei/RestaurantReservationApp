namespace RestaurantReservationApp.Dto
{
    public class RestaurantCreateDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string OpeningHours { get; set; }
        public int Seats { get; set; }
    }
}