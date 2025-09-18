
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domin.Entities;
using Restaurants.Domin.Exceptions;

namespace Restaurants.Application.Users.Commands
{
    public class UpdateUserCommandHandler(ILogger<UpdateUserCommandHandler> _logger, IUserContext _userContext, IUserStore<User> _userStore)
        : IRequestHandler<UpdateUserCommand>
    {
        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _userContext.GetCurrentUser();
            _logger.LogInformation(" updating user :{id}", user?.id);
            var dbUser = await _userStore.FindByIdAsync(user!.id, cancellationToken);
            if (dbUser is null)
            {
                throw new NotFoundException(nameof(dbUser), user!.id);
            }
            dbUser.DateOfBirth = request.DateOfBirth;
            dbUser.Nationality = request.Nationality;  

            await _userStore.UpdateAsync(dbUser, cancellationToken);
        }
    }
}
