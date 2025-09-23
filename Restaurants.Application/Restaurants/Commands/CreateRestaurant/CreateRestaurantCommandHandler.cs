
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domin.Entities;
using Restaurants.Domin.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandHandler(IMapper mapper, IRestaurantsRepository _restaurantsRepo, 
        ILogger<CreateRestaurantCommandHandler> _logger,
        IUserContext _userContext
        ) 
        : IRequestHandler<CreateRestaurantCommand,Guid>
    {
        public async Task<Guid> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var user = _userContext.GetCurrentUser();
            _logger.LogInformation("User {user} [{userid}] is creating a {@restaurant}", user!.email,user.id,request);
            var restaurant = mapper.Map<Restaurant>(request);
            restaurant.OwnerId = user.id;
            var result = await _restaurantsRepo.CreateAsync(restaurant);
            return result;
        }
    }
}
