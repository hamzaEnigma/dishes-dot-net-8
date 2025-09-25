using Restaurants.Domin.Entities;

namespace Restaurants.Domin.Repositories
{
    public interface IRestaurantsRepository
    {
        Task<IEnumerable<Restaurant>> GetAllAsync();
        Task<Restaurant?> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(Restaurant restaurant);
        Task DeleteAsync(Restaurant restaurant);
        Task SaveChangesAsync();
        Task<IEnumerable<Restaurant>> GetAllMatching(string? searchPhrase);
    }
}
