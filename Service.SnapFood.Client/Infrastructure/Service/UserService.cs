using Microsoft.AspNetCore.Components.Authorization;
using Service.SnapFood.Share.Model.Commons;

namespace Service.SnapFood.Client.Infrastructure.Service
{
    public interface IUserService
    {
        Task<CurrentUser> GetCurrentUserAsync();
    }

    public class UserService : IUserService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public UserService(
            AuthenticationStateProvider authenticationStateProvider)
        {
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<CurrentUser> GetCurrentUserAsync()
        {
            try
            {
                var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
                var user = authState.User;

                if (user.Identity?.IsAuthenticated != true)
                    return new CurrentUser();

                string userId = user.FindFirst("user_id")?.Value ?? string.Empty;
                string userName = user.Identity.Name ?? string.Empty;

                if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(userId))
                    return new CurrentUser();



                CurrentUser currentUser = new CurrentUser
                {
                    UserId = Guid.Parse(userId),
                    UserName = userName,
                };

                if (currentUser is null)
                    return new CurrentUser();

                return currentUser;
            }
            catch
            {
                return new CurrentUser();
            }
        }




    }
}
