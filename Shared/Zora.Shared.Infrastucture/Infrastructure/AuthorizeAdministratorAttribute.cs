using Zora.Shared.Domain.Common;

namespace Zora.Shared.Infrastructure
{
    using Microsoft.AspNetCore.Authorization;
    using static Constants;

    public class AuthorizeAdministratorAttribute : AuthorizeAttribute
    {
        public AuthorizeAdministratorAttribute() => this.Roles = AdministratorRoleName;
    }
}
