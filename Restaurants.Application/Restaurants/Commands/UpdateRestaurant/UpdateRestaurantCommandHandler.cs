using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Domin.Entities;
using Restaurants.Domin.Exceptions;
using Restaurants.Domin.Interfaces;
using Restaurants.Domin.Repositories;
using Restaurants.Domin.Constants;
namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommandHandler(IMapper _mapper, IRestaurantsRepository _restaurantsRepo,
        IRestaurantAuthorizationService _authRestoService,
        ILogger<CreateRestaurantCommandHandler> _logger)
        : IRequestHandler<UpdateRestaurantCommand>
    {
        public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("updating the restuarant with {id}",request.Id);
            var restaurant = await _restaurantsRepo.GetByIdAsync(request.Id);

            if (restaurant == null)
                throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

            _mapper.Map(request, restaurant);
            if (!_authRestoService.Authorize(restaurant, ResourceOperation.Update))
            {
                throw new ForbidenException();
            }
            await _restaurantsRepo.SaveChangesAsync();
            
        }
    }
}
