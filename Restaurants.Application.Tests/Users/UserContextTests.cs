using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Restaurants.Application.Users;
using Restaurants.Domin;
using Restaurants.Domin.Entities;
using System.Security.Claims;
using Xunit;

namespace Restaurants.Application.Tests.Users
{
    public class UserContextTests
    {
        [Fact()]
        public void GetCurrentUserTest_WithAuthenticatedUser_ShouldReturnCurrentUser()
        {
            // arrange

            var Mock = new Mock<IHttpContextAccessor>();
            var dateOfBirth = new DateOnly(1990, 1, 1);
            var claims = new List<Claim>() {
                 new(ClaimTypes.NameIdentifier, "1"),
                new(ClaimTypes.Email, "test@test.com"),
                new(ClaimTypes.Role, UserRoles.Admin),
                new(ClaimTypes.Role, UserRoles.User),
                new("Nationality", "German"),
                new("DateOfBirth", dateOfBirth.ToString("yyyy-MM-dd"))
              };

            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "test"));
            Mock.Setup(t => t.HttpContext).Returns(new DefaultHttpContext() { User = user });

            var userContext = new UserContext(Mock.Object);

            //act 

            var currentUser = userContext.GetCurrentUser();

            // assert

            currentUser.Should().NotBeNull();
            currentUser.id.Should().Be("1");
            currentUser.email.Should().Be("test@test.com");
            currentUser.roles.Should().ContainInOrder(UserRoles.Admin, UserRoles.User);
            currentUser.Nationality.Should().Be("German");
            currentUser.DateOfBirth.Should().Be(dateOfBirth);

        }

        [Fact]
        public void GetCurrentUser_WithUserContextNotPresent_ThrowsInvalidOperationException()
        {
            // arrange

            var Mock = new Mock<IHttpContextAccessor>();
            Mock.Setup(t => t.HttpContext).Returns((HttpContext)null!);
            var userContext = new UserContext(Mock.Object);


            //act 
            Action action = () => userContext.GetCurrentUser();

            // assert
            action.Should().Throw<InvalidOperationException>()
                .WithMessage("User context is not present");
        }




    }
}
