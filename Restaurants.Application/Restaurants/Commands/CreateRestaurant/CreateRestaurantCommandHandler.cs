
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domin.Entities;
using Restaurants.Domin.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandHandler(IMapper mapper, IRestaurantsRepository _restaurantsRepo, ILogger<CreateRestaurantCommandHandler> _logger) 
        : IRequestHandler<CreateRestaurantCommand,Guid>
    {
        public async Task<Guid> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("creating a restaurant");
            _logger.LogInformation("creating a {@restaurant}",request);
            var restaurant = mapper.Map<Restaurant>(request);
            var result = await _restaurantsRepo.CreateAsync(restaurant);
            return result;
        }
    }
}
