using Zora.Payments.Domain.Factories;
using Zora.Payments.Domain.Models;

namespace Zora.Payments.Zora.Payments.Domain.Factories.Factories
{
    internal class PaymentFactory : IPaymentFactory
    {
        private string paymentTitle = default!;
        private decimal paymentAmount = default!;
        private DateTimeRange paymentDue = default!;
        private int paymentStudentId = default!;


        public IPaymentFactory WithTitle(string title)
        {
            paymentTitle = title;
            return this;
        }

        public IPaymentFactory WithAmount(decimal amount)
        {
            paymentAmount = amount;
            return this;
        }

        public IPaymentFactory WithPaymentDue(DateTimeRange paymentDueDate)
        {
            paymentDue = paymentDueDate;
            return this;
        }

        public IPaymentFactory WithStudent(int studentId)
        {
            paymentStudentId = studentId;
            return this;
        }

        public Payment Build() => new Payment(paymentTitle, paymentAmount, paymentStudentId, paymentDue);

        public Payment Build(string title, decimal amount, int studentId, DateTimeRange paymentDue)
            => this
                 .WithTitle(title)
                 .WithAmount(amount)
                 .WithStudent(studentId)
                 .WithPaymentDue(paymentDue)
                 .Build();

    }
}
