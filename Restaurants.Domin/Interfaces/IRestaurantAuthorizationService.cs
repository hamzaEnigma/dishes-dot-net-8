using Restaurants.Domin.Constants;
using Restaurants.Domin.Entities;

namespace Restaurants.Domin.Interfaces
{
    public interface IRestaurantAuthorizationService
    {
        bool Authorize(Restaurant restaurant, ResourceOperation resourceOperation);
    }
}
