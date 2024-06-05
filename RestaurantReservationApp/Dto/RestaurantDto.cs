namespace RestaurantReservationApp.Dto
{
    public class RestaurantDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string OpeningHours { get; set; }
        public int Seats { get; set; }
        public string OwnerId { get; set; }
        public UserDto Owner { get; set; }
        public List<ReviewDto> Reviews { get; set; }
    }
}
