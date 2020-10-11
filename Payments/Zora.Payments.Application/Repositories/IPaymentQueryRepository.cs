using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Zora.Payments.Domain.Models;
using Zora.Shared.Application.Contracts;

namespace Zora.Payments.Application.Repositories
{
    public interface IPaymentQueryRepository : IQueryRepository<Payment>
    {
        Task<IEnumerable<Payment>> Payments(int id, CancellationToken cancellationToken = default);

        Task<IEnumerable<Payment>> Payments(CancellationToken cancellationToken = default);
    }
}

