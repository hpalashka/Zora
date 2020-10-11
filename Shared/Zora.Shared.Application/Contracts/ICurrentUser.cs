using System.Collections.Generic;

namespace Zora.Shared.Application.Contracts
{
    public interface ICurrentUser
    {
        string UserId { get; }

        IEnumerable<string> Roles { get; }
    }
}
