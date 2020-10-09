using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Zora.Payments.Domain.Models;
using Zora.Payments.Domain.Repositories;
using Zora.Shared.Application;
using Zora.Shared.Domain.Models;

namespace Zora.Payments.Application.Commands.Delete
{
    public class PayPaymentCommand : EntityCommand<int>, IRequest<bool>
    {
        public Message MessageData { get; set; } = default!;

        public class PayPaymentCommandHandler : IRequestHandler<PayPaymentCommand, bool>
        {
            private readonly IPaymentDomainRepository _paymentRepository;

            public PayPaymentCommandHandler(IPaymentDomainRepository paymentRepository)
            {
                _paymentRepository = paymentRepository;
            }

            public async Task<bool> Handle(PayPaymentCommand request, CancellationToken cancellationToken)
            {
                Payment payment = await _paymentRepository.FindPayment(request.Id, cancellationToken);
                payment.PayPayment();
                await _paymentRepository.Save(payment, cancellationToken, request.MessageData);

                return true;
            }
        }
    }
}
