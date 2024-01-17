using System.Security.Claims;
using Application.Contracts.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.JwtAuthentication
{
    public class CurrentLoggedInService : ICurrentLoggedInService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentLoggedInService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid GetCurrentLoggedInId()
        {
            var userId = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (Guid.TryParse(userId, out var id))
            {
                return id;
            }

            return Guid.Empty;
        }

        public string GetCurrentLoggedInUsername()
        {
            var username = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.Name);
            return username ?? string.Empty;
        }

        public string GetCurrentLoggedInEmail()
        {
            var email = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.Email);
            return email ?? string.Empty;
        }

        public string GetCurrentLoggedInRole()
        {
            var role = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
            return role ?? string.Empty;
        }
        public int GetCurrentLoggedInPriority()
        {
            var priority =_httpContextAccessor?.HttpContext?.User.FindFirstValue("http://schemas.example.com/claims/priority");
            if (int.TryParse(priority, out int id))
            {
                return id;
            }

            return 0;
        }
    }
}
