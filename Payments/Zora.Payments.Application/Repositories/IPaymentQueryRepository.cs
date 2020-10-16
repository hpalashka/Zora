using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Zora.Payments.Application.Quieries.Common;
using Zora.Payments.Domain.Models;
using Zora.Shared.Application.Contracts;

namespace Zora.Payments.Application.Repositories
{
    public interface IPaymentQueryRepository : IQueryRepository<Payment>
    {
        Task<IList<PaymentsViewModel>> Payments(int id, CancellationToken cancellationToken = default);

        Task<IList<PaymentsViewModel>> Payments(CancellationToken cancellationToken = default);
    }
}

