using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Zora.Payments.Application.Quieries.Common;
using Zora.Payments.Application.Repositories;
using Zora.Payments.Domain.Models;

namespace Zora.Payments.Application.Queries.Payments
{
    public class PaymentsQuery : IRequest<IEnumerable<Payment>>
    {

        public class PaymentsQueryHandler : IRequestHandler<PaymentsQuery, IEnumerable<Payment>>
        {
            private readonly IPaymentQueryRepository _paymentRepository;

            public PaymentsQueryHandler(IPaymentQueryRepository paymentRepository)
            {
                _paymentRepository = paymentRepository;
            }

            public async Task<IEnumerable<Payment>> Handle(PaymentsQuery request, CancellationToken cancellationToken)
            {
                return await _paymentRepository.Payments(cancellationToken);

            }
        }
    }
}
