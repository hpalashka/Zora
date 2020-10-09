using System.Threading;
using System.Threading.Tasks;
using Zora.Payments.Domain.Models;
using Zora.Shared.Domain;

namespace Zora.Payments.Domain.Repositories
{

    public interface IPaymentDomainRepository : IDomainRepository<Payment>
    {
        Task<Payment> FindPayment(int id, CancellationToken cancellationToken = default);

        Task<bool> DeletePayment(int id, CancellationToken cancellationToken = default);

    }
}

