using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Zora.Payments.Domain.Factories;
using Zora.Payments.Domain.Models;
using Zora.Payments.Domain.Repositories;
using Zora.Shared.Domain.Models;

namespace Zora.Payments.Application.Commands.Create
{
    public class CreatePaymentCommand : IRequest<CreatePaymentOutputModel>
    {
        public string Title { get; set; } = default!;

        public decimal Amount { get; set; } = default!;

        public DateTimeRange PaymentDue { get; set; } = default!;

        public int StudentId { get; set; } = default!;

        public Message MessageData { get; set; } = default!;

        public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, CreatePaymentOutputModel>
        {
            private readonly IPaymentFactory _paymentFactory;
            private readonly IPaymentDomainRepository _paymentRepository;

            public CreatePaymentCommandHandler(

                IPaymentFactory paymentFactory,
                IPaymentDomainRepository paymentRepository)
            {

                _paymentFactory = paymentFactory;
                _paymentRepository = paymentRepository;
            }

            public async Task<CreatePaymentOutputModel> Handle(
                CreatePaymentCommand request,
                CancellationToken cancellationToken)
            {
                var payment = _paymentFactory
                    .WithTitle(request.Title)
                    .WithAmount(request.Amount)
                    .WithPaymentDue(request.PaymentDue)
                    .WithStudent(request.StudentId)
                    .Build();

                await _paymentRepository.Save(payment, cancellationToken, request.MessageData);

                return new CreatePaymentOutputModel(payment.Id);
            }
        }
    }
}
