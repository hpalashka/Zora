using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Zora.Payments.Domain.Repositories;
using Zora.Shared.Application;

namespace Zora.Payments.Application.Commands.Delete
{
    public class DeletePaymentCommand : EntityCommand<int>, IRequest<bool>
    {
       
        public class DeletePaymentCommandHandler : IRequestHandler<DeletePaymentCommand, bool>
        {
            private readonly IPaymentDomainRepository _paymentRepository;

            public DeletePaymentCommandHandler(IPaymentDomainRepository paymentRepository)
            {
                _paymentRepository = paymentRepository;
            }

            public async Task<bool> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
            {
                return await _paymentRepository.DeletePayment(request.Id, cancellationToken);
            }
        }
    }
}
