using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Domin.Repositories;

namespace Restaurants.Application.Restaurants.Queries.RestaurantById
{
    public class GetRestaurantByIdQueryHandler(IRestaurantsRepository _restoRepo, ILogger<GetAllRestaurantsQueryHandler> _logger, IMapper _mapper)
        : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto>
    {
        public async Task<RestaurantDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("getting  restaurant by id {id}", request.id);
            var restaurant = await _restoRepo.GetByIdAsync(request.id);
            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);
            return restaurantDto;
        }
    }
}
