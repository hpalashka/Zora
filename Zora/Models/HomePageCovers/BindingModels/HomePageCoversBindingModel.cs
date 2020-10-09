using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Zora.Shared.Domain.Common;

namespace Zora.Web.Models.HomePageCovers.BindingModels
{
    public class HomePageCoversBindingModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = ValidationConstants.RequiredField)]
            [StringLength(ValidationConstants.MaxTitleLength)]
        [Display(Name = ValidationConstants.Title)]
        public string Title { get; set; }

        [Required(ErrorMessage = ValidationConstants.RequiredField)]
        [Display(Name = ValidationConstants.UploadImage)]
        public IFormFile UploadImage { get; set; }
    }
}
