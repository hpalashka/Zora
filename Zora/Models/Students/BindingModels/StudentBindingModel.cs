using System.ComponentModel.DataAnnotations;
using Zora.Shared.Domain.Common;

namespace Zora.Web.Models.Students.BindingModels
{
    public class StudentBindingModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(ValidationConstants.MaxTitleLength)]
        [Display(Name = ValidationConstants.FullName)]
        public string Name { get; set; }


        [Required]
        [StringLength(ValidationConstants.MaxTitleLength)]
        [Display(Name = ValidationConstants.Email)]
        public string Email { get; set; }

        [Required]
        [Display(Name = ValidationConstants.Phone)]
        public string PhoneNumber { get; set; }
    }
}
