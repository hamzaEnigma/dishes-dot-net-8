
using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domin.Entities;
using Restaurants.Domin.Repositories;

namespace Restaurants.Application.Restaurants
{
    public class RestaurantService(IMapper mapper, IRestaurantsRepository _restaurantsRepo, ILogger<RestaurantService> logger) : IRestaurantService
    {
        public async Task<Guid> Create(CreateRestaurantDto dto)
        {
            var restaurant = mapper.Map<Restaurant>(dto);
            var result = await _restaurantsRepo.Create(restaurant);
            return result;
        }

        public async Task<IEnumerable<RestaurantDto>> GetAll()
        {
            logger.LogInformation("Getting all restaurants");
            var restaurants = await _restaurantsRepo.GetAll();
            var restaurantsDto = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
            return restaurantsDto;
        }

        public async Task<RestaurantDto?> GetById(Guid id)
        {
            var restaurant = await _restaurantsRepo.GetById(id);
            var restaurantDto = mapper.Map<RestaurantDto>(restaurant);
            return restaurantDto;
        }
    }
}
