

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domin.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQueryHandler(IRestaurantsRepository _restoRepo, ILogger<GetAllRestaurantsQueryHandler> _logger,IMapper _mapper)
        : IRequestHandler<GetAllRestaurantsQuery, IEnumerable<RestaurantDto>>
    {
        public async Task<IEnumerable<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting all restaurants with matching phrase : {phrase}", request.searchPhrase);
            var restaurants = await _restoRepo.GetAllMatching(request.searchPhrase);
            var restaurantsDto = _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
            return restaurantsDto;

        }
    }
}
