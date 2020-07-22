using System;
using System.ComponentModel.DataAnnotations;
using Zora.Shared.Data;

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


    }
}
