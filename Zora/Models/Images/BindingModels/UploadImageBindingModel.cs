using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Zora.Shared.Data;
using Zora.Web.Data.Models;

namespace Zora.Web.Models.Images.BindingModels
{
    public class UploadImageBindingModel
    {
        [Required(ErrorMessage = ValidationConstants.RequiredField)]
            [StringLength(ValidationConstants.MaxTitleLength)]
        [Display(Name = ValidationConstants.Title)]
        public string Title { get; set; }


        [Required(ErrorMessage = ValidationConstants.RequiredField)]
        [Display(Name = ValidationConstants.UploadImage)]
        public IFormFile UploadImage { get; set; }

        [Display(Name = ValidationConstants.Album)]
        public int AlbumId { get; set; }
        public Album Album { get; set; }
    }
}
