using Zora.Payments.Domain.Exceptions;
using Zora.Shared.Domain;
using Zora.Shared.Domain.Common;
using Zora.Shared.Domain.Models;


namespace Zora.Payments.Domain.Models
{
    public class Payment : Entity<int>, IAggregateRoot
    {
        internal Payment(string title, decimal amount, int studentId, DateTimeRange paymentDue)
        {
            Validate(title, amount);

            Title = title;
            Amount = amount;
            PaymentDue = paymentDue;
            Paid = false;
            StudentId = studentId;
        }

        private Payment(string title, decimal amount, int studentId)
        {
            Title = title;
            Amount = amount;
            PaymentDue = default!;
            StudentId = studentId;
            Paid = false;
        }


        public string Title { get; private set; }

        public decimal Amount { get; private set; }

        public DateTimeRange PaymentDue { get; private set; }

        public bool Paid { get; private set; }

        public int StudentId { get; private set; }


        public Payment UpdateTitle(string title)
        {
            ValidateTitle(title);
            Title = title;
            return this;
        }

        public Payment UpdateAmount(decimal amount)
        {
            ValidateAmount(amount);
            Amount = amount;
            return this;
        }

        public void UpdatePaymentDueDate(DateTimeRange newStartEnd)
        {
            if (newStartEnd == PaymentDue)
            {
                return;
            }

            PaymentDue = newStartEnd;
        }

        public Payment PayPayment()
        {
            Paid = true;
            return this;
        }

        private void Validate(string title, decimal amount)
        {
            ValidateTitle(title);
            ValidateAmount(amount);
        }


        private void ValidateTitle(string titlle)
        => Guard.ForStringLength<InvalidPaymentException>(titlle, ValidationConstants.MinStringLength, ValidationConstants.MaxPaymentTitleLength, nameof(Title));

        private void ValidateAmount(decimal amount)
          => Guard.AgainstOutOfRange<InvalidPaymentException>(amount, 0, decimal.MaxValue, nameof(Amount));

    }

}
