using System.Threading;
using System.Threading.Tasks;
using Zora.Shared.Domain.Models;

namespace Zora.Shared.Domain
{
      public interface IDomainRepository<in TEntity>
        where TEntity : IAggregateRoot
    {
        Task Save(TEntity entity, CancellationToken cancellationToken = default, params Message[] messages);
    }
}
