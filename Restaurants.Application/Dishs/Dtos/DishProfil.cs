using AutoMapper;
using Restaurants.Domin.Entities;

namespace Restaurants.Application.Dishs.Dtos
{
    public class DishProfil : Profile
    {
        public DishProfil() {
            CreateMap<Dish, DishDto>();
        }
    }
}
