using Zora.Payments.Domain.Models;
using Zora.Payments.Infrastructure.Persistance;
using Zora.Shared.Services;

namespace Zora.Payments.Web.Services
{
    public class PaymentService : DataService<Payment>, IPaymentService
    {
        public PaymentService(PaymentsDbContext context)
            : base(context)
       { }

    }
}
