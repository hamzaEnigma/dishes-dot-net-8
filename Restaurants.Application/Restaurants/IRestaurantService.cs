
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants
{
    public interface IRestaurantService
    {
        Task<IEnumerable<RestaurantDto>> GetAll();
        Task<RestaurantDto?> GetById(Guid id);
        Task<Guid> Create(CreateRestaurantDto dto);
    }
}
