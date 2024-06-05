namespace RestaurantReservationApp.Dto
{
    public class CustomerDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public UserDto User { get; set; }
    }
}
