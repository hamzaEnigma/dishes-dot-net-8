
namespace Restaurants.Application.Users
{
    public record CurrentUser(string id, string email, IEnumerable<string> roles)
    {
        public bool IsInRole(string role) => roles.Contains(role);
    }
}
