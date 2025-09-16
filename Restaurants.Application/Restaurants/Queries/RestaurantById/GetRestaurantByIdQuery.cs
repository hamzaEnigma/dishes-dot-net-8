using MediatR;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Queries.RestaurantById
{
    public class GetRestaurantByIdQuery : IRequest<RestaurantDto>
    {
        public Guid id { get; set; }
        public GetRestaurantByIdQuery(Guid id)
        {
            this.id = id;
        }
    }
}
