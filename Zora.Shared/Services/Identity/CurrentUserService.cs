using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Zora.Shared.Infrastructure;

namespace Zora.Shared.Services.Identity
{

    public class CurrentUserService : ICurrentUserService
    {
        private readonly ClaimsPrincipal user;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            user = httpContextAccessor.HttpContext?.User;

            if (user == null)
            {
                throw new InvalidOperationException("This request does not have an authenticated user.");
            }

            UserId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            Email = user.FindFirstValue(ClaimTypes.Name);
        }

        public string UserId { get; }
        public string Email { get; }

        public bool IsAdministrator => user.IsAdministrator();
    }
}
