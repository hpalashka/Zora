using FluentValidation;
using Zora.Shared.Domain.Common;

namespace Zora.Students.Application.Commands.Create
{
    public class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
    {
        public CreateStudentCommandValidator()
        {
            this.RuleFor(p => p.Name)
                .MinimumLength(ValidationConstants.MinStringLength)
                .MaximumLength(ValidationConstants.MaxTitleLength)
                .NotEmpty();

            this.RuleFor(p => p.Email)
                .MinimumLength(ValidationConstants.MinEmailLength)
                .MaximumLength(ValidationConstants.MaxEmailLength)
                .NotEmpty();


            this.RuleFor(u => u.PhoneNumber)
                .MinimumLength(ValidationConstants.MinPhoneNumberLength)
                .MaximumLength(ValidationConstants.MaxPhoneNumberLength)
                .Matches(ValidationConstants.PhoneNumberRegularExpression)
                .NotEmpty();

        }
    }
}