using System.Collections.Generic;

namespace Zora.Shared.Domain.Models
{
    public interface IEntity
    {
        IReadOnlyCollection<IDomainEvent> Events { get; }

        void ClearEvents();
    }
}
