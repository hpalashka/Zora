using Zora.Shared.Domain;

namespace Zora.Payments.Domain.Exceptions
{
    public class InvalidDateRangeException : BaseDomainException
    {
        public InvalidDateRangeException()
        {
        }

        public InvalidDateRangeException(string error) => this.Error = error;
    }
}
