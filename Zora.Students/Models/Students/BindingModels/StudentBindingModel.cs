using System.ComponentModel.DataAnnotations;
using Zora.Shared.Data;

namespace Zora.Students.Models.Students.BindingModels
{
    public class StudentBindingModel
    {

        [Required]
        [StringLength(ValidationConstants.MaxTitleLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(ValidationConstants.MaxTitleLength)]
        public string Email { get; set; }

    }
}
