using System.Threading.Tasks;
using Zora.Shared.Data.Models;

namespace Zora.Shared.Services
{
    public interface IDataService<in TEntity>
          where TEntity : class
    {
        Task MarkMessageAsPublished(int id);

        Task Save(TEntity entity, params Message[] messages);
    }
}
