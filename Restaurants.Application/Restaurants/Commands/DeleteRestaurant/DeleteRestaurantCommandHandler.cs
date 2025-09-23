using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domin.Constants;
using Restaurants.Domin.Entities;
using Restaurants.Domin.Exceptions;
using Restaurants.Domin.Interfaces;
using Restaurants.Domin.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant
{
    public class DeleteRestaurantCommandHandler(IRestaurantsRepository _restoRepo, ILogger<DeleteRestaurantCommandHandler> _logger,
        IRestaurantAuthorizationService _authRestoService
        )
        : IRequestHandler<DeleteRestaurantCommand>
    {
        public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("deleting restaurant with id {id}", request.id);
            var rest = await _restoRepo.GetByIdAsync(request.id);
            if (rest is null)
                throw new NotFoundException(nameof(Restaurant),request.id.ToString());
            if (!_authRestoService.Authorize(rest, ResourceOperation.Delete))
            {
                throw new ForbidenException();
            }
            await _restoRepo.DeleteAsync(rest);         
        }
    }
}
