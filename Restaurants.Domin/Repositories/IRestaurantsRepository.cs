using Restaurants.Domin.Entities;

namespace Restaurants.Domin.Repositories
{
    public interface IRestaurantsRepository
    {
        Task<IEnumerable<Restaurant>> GetAll();
        Task<Restaurant?> GetById(Guid id);
        Task<Guid> Create(Restaurant restaurant);
    }
}
