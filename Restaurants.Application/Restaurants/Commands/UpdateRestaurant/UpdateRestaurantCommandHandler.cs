using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Domin.Entities;
using Restaurants.Domin.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommandHandler(IMapper _mapper, IRestaurantsRepository _restaurantsRepo, ILogger<CreateRestaurantCommandHandler> _logger)
        : IRequestHandler<UpdateRestaurantCommand, bool>
    {
        public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("updating the restuarant with {id}",request.Id);
            var restaurant = await _restaurantsRepo.GetByIdAsync(request.Id);

            if (restaurant == null)
                return false;

            _mapper.Map(request, restaurant);
            await _restaurantsRepo.SaveChangesAsync();
            
            return true;

        }
    }
}
