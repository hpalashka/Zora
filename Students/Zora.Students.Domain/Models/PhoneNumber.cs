using System.Text.RegularExpressions;
using Zora.Shared.Domain.Common;
using Zora.Shared.Domain.Models;
using Zora.Students.Domain.Exceptions;

namespace Zora.Students.Domain.Models
{
    public class PhoneNumber : ValueObject
    {
        internal PhoneNumber(string number)
        {
            this.Validate(number);

            if (!Regex.IsMatch(number, ValidationConstants.PhoneNumberRegularExpression))
            {
                throw new InvalidPhoneNumberException("Phone number must start with a '+' and contain only digits afterwards.");
            }

            this.Number = number;
        }

        public string Number { get; }

        public static implicit operator string(PhoneNumber number) => number.Number;

        public static implicit operator PhoneNumber(string number) => new PhoneNumber(number);

        private void Validate(string phoneNumber) 
            => Guard.ForStringLength<InvalidPhoneNumberException>(phoneNumber, ValidationConstants.MinPhoneNumberLength, ValidationConstants.MaxPhoneNumberLength, nameof(PhoneNumber));
    }
}
