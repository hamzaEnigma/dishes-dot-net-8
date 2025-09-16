using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domin.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant
{
    public class DeleteRestaurantCommandHandler(IRestaurantsRepository _restoRepo, ILogger<DeleteRestaurantCommandHandler> _logger)
        : IRequestHandler<DeleteRestaurantCommand, bool>
    {
        public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("deleting restaurant with id {id}", request.id);
            var rest = await _restoRepo.GetByIdAsync(request.id);
            if (rest is null) 
            { return false; }

            await _restoRepo.DeleteAsync(rest);
            return true;
        }
    }
}
