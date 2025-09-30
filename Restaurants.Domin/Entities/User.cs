using Microsoft.AspNetCore.Identity;

namespace Restaurants.Domin.Entities
{
    public class User : IdentityUser
    {
        public DateOnly? DateOfBirth { get; set; }
        public string? Nationality { get; set; }
        public List<Restaurant> Restaurants { get; set; } = default!;
    }
}
