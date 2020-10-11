using System.Threading.Tasks;
using Zora.Shared.Domain;

namespace Zora.Shared.Application
{
      public interface IEventHandler<in TEvent>
        where TEvent : IDomainEvent
    {
        Task Handle(TEvent domainEvent);
    }
}
