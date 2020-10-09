using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Zora.Shared.Domain;
using Zora.Shared.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Zora.Shared.Infrastructure.Persistence
{
    public abstract class DataRepository<TDbContext, TEntity> : IDomainRepository<TEntity>//internal?
        where TDbContext : IDbContext
        where TEntity : class, IAggregateRoot
    {
        protected DataRepository(TDbContext db) => this.Data = db;

        protected TDbContext Data { get; }

        protected IQueryable<TEntity> All() => this.Data.Set<TEntity>();

        public async Task Save(TEntity entity, CancellationToken cancellationToken = default, params Message[] messages)
        {
            foreach (var message in messages)
            {
                this.Data.Update(message);
            }

            this.Data.Update(entity);

            await this.Data.SaveChangesAsync(cancellationToken);
        }
    }
}
