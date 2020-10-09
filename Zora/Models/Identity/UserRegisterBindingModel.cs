using System.ComponentModel.DataAnnotations;
using Zora.Shared.Domain.Common;

namespace Zora.Web.Models.Identity.BindingModels
{
    public class UserRegisterBindingModel
    {

        [Required(ErrorMessage = ValidationConstants.RequiredField)]
        [StringLength(ValidationConstants.MaxTitleLength)]
        [Display(Name = ValidationConstants.FullName)]
        public string Name { get; set; }

        [Required(ErrorMessage =ValidationConstants.RequiredField)]
        [EmailAddress]
        [MinLength(ValidationConstants.MinEmailLength)]
        [MaxLength(ValidationConstants.MaxEmailLength)]
        [Display(Name = ValidationConstants.Email)]
        public string Email { get; set; }

        [Required(ErrorMessage = ValidationConstants.RequiredField)]
        [Phone]
        [Display(Name = ValidationConstants.Phone)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = ValidationConstants.RequiredField)]
        [DataType(DataType.Password)]
        [StringLength(ValidationConstants.MaxPasswordLength, ErrorMessage = ValidationConstants.PasswordLenght, MinimumLength = ValidationConstants.MinPasswordLength)]
        [Display(Name = ValidationConstants.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = ValidationConstants.RequiredField)]
        [DataType(DataType.Password)]
        [StringLength(ValidationConstants.MaxPasswordLength, ErrorMessage = ValidationConstants.PasswordLenght, MinimumLength = ValidationConstants.MinPasswordLength)]
        [Display(Name = ValidationConstants.ConfirmPassword)]
        [Compare("Password", ErrorMessage = ValidationConstants.PasswordMatch)]
        public string ConfirmPassword { get; set; }
    }
}
