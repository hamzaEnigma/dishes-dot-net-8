using MediatR;
using Restaurants.Domin.Entities;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant
{
    public class DeleteRestaurantCommand(Guid id) : IRequest<bool>
    {
        public Guid id = id;
    }
}
