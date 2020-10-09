using Zora.Shared.Domain;

namespace Zora.Payments.Domain.Exceptions
{
    public class InvalidPaymentException : BaseDomainException
    {
        public InvalidPaymentException()
        {
        }

        public InvalidPaymentException(string error) => this.Error = error;
    }
}
