

using AutoMapper;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Domin.Entities;

namespace Restaurants.Application.Restaurants.Dtos
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {

            CreateMap<Restaurant, RestaurantDto>()
               .ForMember(d => d.City, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.City))
               .ForMember(d => d.Street, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.Street))
               .ForMember(d => d.PostalCode, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.PostalCode))
               .ForMember(d => d.Dishes, opt => opt.MapFrom(src => src.Dishes)).ReverseMap();

            CreateMap<CreateRestaurantCommand, Restaurant>()
                   .ForMember(d => d.Address, opt => opt.MapFrom(src => new Address { City = src.City, Street = src.Street, PostalCode = src.PostalCode }));

            CreateMap<UpdateRestaurantCommand, Restaurant>();

        }
    }
}
