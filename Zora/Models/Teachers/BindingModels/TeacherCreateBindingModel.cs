using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Zora.Shared.Domain.Common;

namespace Zora.Web.Models.Teachers.BindingModels
{
    public class TeacherCreateBindingModel
    {
        [Required]
        [StringLength(ValidationConstants.MaxTitleLength)]
        [Display(Name = ValidationConstants.FullName)]
        public string Name { get; set; }

        [Required]
        [StringLength(ValidationConstants.MaxTeacherescriptionLength)]
        [Display(Name = ValidationConstants.Description)]
        public string Description { get; set; }


        [Display(Name = ValidationConstants.Image)]
        public IFormFile ImageFile { get; set; }
    }
}
