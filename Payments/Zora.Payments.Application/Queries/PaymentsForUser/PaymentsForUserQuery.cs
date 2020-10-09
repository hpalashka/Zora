using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Zora.Payments.Application.Quieries.Common;
using Zora.Payments.Application.Repositories;
using Zora.Payments.Domain.Models;

namespace Zora.Payments.Application.Queries.PaymentsForUser
{
    public class PaymentsForUserQuery : IRequest<IEnumerable<Payment>>
    {
        public int Id { get; set; }

        public class PaymentsForUserQueryHandler : IRequestHandler<PaymentsForUserQuery, IEnumerable<Payment>>
        {
            private readonly IPaymentQueryRepository _paymentRepository;

            public PaymentsForUserQueryHandler(IPaymentQueryRepository paymentRepository)
            {
                _paymentRepository = paymentRepository;
            }

            public async Task<IEnumerable<Payment>> Handle(PaymentsForUserQuery request, CancellationToken cancellationToken)
            {
                return await _paymentRepository.Payments(request.Id, cancellationToken);

            }
        }
    }
}
