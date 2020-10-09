namespace Zora.Payments.Application.Commands.Create
{
    public class CreatePaymentOutputModel
    {
        public CreatePaymentOutputModel(int paymentId)
            => this.PaymentId = paymentId;

        public int PaymentId { get; }
    }
}
