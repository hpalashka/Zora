using System.Threading.Tasks;
using Zora.Shared.Domain;

namespace Zora.Shared.Infrastructure.Events
{
       public interface IEventDispatcher //todo inernal??
    {
        Task Dispatch(IDomainEvent domainEvent);
    }
}
