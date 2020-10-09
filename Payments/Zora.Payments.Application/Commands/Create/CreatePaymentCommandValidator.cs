using FluentValidation;
using Zora.Shared.Domain.Common;

namespace Zora.Payments.Application.Commands.Create
{
    public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
    {
        public CreatePaymentCommandValidator()
        {
            this.RuleFor(p => p.Title)
                .MinimumLength(ValidationConstants.MinStringLength)
                .MaximumLength(ValidationConstants.MaxPaymentTitleLength)
                .NotEmpty();

            this.RuleFor(p => p.Amount)
                .InclusiveBetween(0, decimal.MaxValue);

            this.RuleFor(p => p.PaymentDue.Start)
                .LessThanOrEqualTo(p=>p.PaymentDue.End)
                .NotEmpty();

            this.RuleFor(p => p.StudentId)
                .NotEmpty();
        }
    }
}