using MediatR;
using Restaurants.Domin.Entities;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant
{
    public class DeleteRestaurantCommand(Guid id) : IRequest
    {
        public Guid id = id;
    }
}
