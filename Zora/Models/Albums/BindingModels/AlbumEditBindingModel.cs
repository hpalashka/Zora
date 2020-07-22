using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Zora.Shared.Data;

namespace Zora.Web.Models.Albums.BindingModels
{
    public class AlbumEditBindingModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = ValidationConstants.RequiredField)]
        [StringLength(ValidationConstants.MaxTitleLength)]
        [Display(Name = ValidationConstants.Title)]
        public string Title { get; set; }

        [StringLength(ValidationConstants.MaxAlbumDescriptionLength)]
        [Display(Name = ValidationConstants.Description)]
        public string Description { get; set; }


        [Display(Name = ValidationConstants.CoverPhoto)]
        public IFormFile UploadImage { get; set; }

        public string FileName { get; set; }

    }
}
