using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Service.SnapFood.Share.Model.Commons;
using System.IdentityModel.Tokens.Jwt;

namespace Service.SnapFood.Manage.Infrastructure.Services
{
    public interface IUserService
    {
        Task<CurrentUser> GetCurrentUserAsync();
    }

    public class UserService : IUserService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly NavigationManager _navigationManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(
            AuthenticationStateProvider authenticationStateProvider,
            NavigationManager navigationManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _navigationManager = navigationManager;
            _httpContextAccessor = httpContextAccessor;
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
