using Zora.Shared.Domain;

namespace Zora.Shared.Application.Contracts
{
    public interface IQueryRepository<in TEntity>
        where TEntity : IAggregateRoot
    {
    }
}
