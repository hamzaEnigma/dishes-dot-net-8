using Microsoft.AspNetCore.Http;
using Restaurants.Domin;
using System.Security.Claims;
using static System.Net.WebRequestMethods;

namespace Restaurants.Application.Users
{
    public interface IUserContext
    {
        CurrentUser? GetCurrentUser();
        bool IsConnected { get; }
    }
    public class UserContext(IHttpContextAccessor _httpContextAccessor) : IUserContext
    {
        public bool IsConnected {
            get
            {
                var identity = _httpContextAccessor.HttpContext?.User?.Identity;
                return identity != null && identity.IsAuthenticated;
            }
        }

        public CurrentUser? GetCurrentUser()
        {
            var user = _httpContextAccessor?.HttpContext?.User;
            if (user == null)
            {
                throw new InvalidOperationException("User context is not present");
            }

            if (!IsConnected)
            {
                throw new UserNotConnected();
            }
            var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            var email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
            var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role)!.Select(c => c.Value);

            return new CurrentUser(userId, email, roles);

        }
    }
}
