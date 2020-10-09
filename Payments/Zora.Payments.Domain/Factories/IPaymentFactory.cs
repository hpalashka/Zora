using Zora.Payments.Domain.Models;
using Zora.Shared.Domain;

namespace Zora.Payments.Domain.Factories
{
    public interface IPaymentFactory : IFactory<Payment>
    {
        IPaymentFactory WithTitle(string title);

        IPaymentFactory WithAmount(decimal amount);

        IPaymentFactory WithPaymentDue(DateTimeRange paymentDue);

        IPaymentFactory WithStudent(int studentId);
    }
}
