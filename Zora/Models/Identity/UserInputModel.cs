using System.ComponentModel.DataAnnotations;
using Zora.Shared.Domain.Common;

namespace Zora.Web.Models.Identity
{
    public class UserInputModel 
    {
        [Required(ErrorMessage = ValidationConstants.RequiredField)]
        [EmailAddress]
        [Display(Name = ValidationConstants.Email)]
        public string Email { get; set; }

        [Required(ErrorMessage = ValidationConstants.RequiredField)]
        [DataType(DataType.Password)]
        [Display(Name = ValidationConstants.Password)]
        public string Password { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }
    }
}
