using System.Security.Claims;

namespace Zora.Shared.Infrastructure
{
    using static Constants;

    public static class ClaimsPrincipalExtensions
    {
        public static bool IsAdministrator(this ClaimsPrincipal user)
            => user.IsInRole(AdministratorRoleName);
    }
}
