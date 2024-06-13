using AutoMapper;
using RestaurantReservationApp.Dto;
using RestaurantReservationApp.Models;

namespace RestaurantReservationApp.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, CustomerDto>();
            CreateMap<Reservation, ReservationDto>();
            CreateMap<ReservationDto, ReservationDto>();
            CreateMap<Restaurant, RestaurantDto>();
            CreateMap<Restaurant, RestaurantCreateDto>();
            CreateMap<RestaurantCreateDto, Restaurant>();
            CreateMap<ReservationDto, ReservationDto>();
            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewDto, ReviewDto>();
            CreateMap<Role, RoleDto>();
            CreateMap<RoleDto, RoleDto>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
