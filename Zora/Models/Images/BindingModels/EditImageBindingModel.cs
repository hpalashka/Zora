using System.ComponentModel.DataAnnotations;
using Zora.Shared.Data;
using Zora.Web.Data.Models;

namespace Zora.Web.Models.Images.BindingModels
{
    public class EditImageBindingModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = ValidationConstants.RequiredField)]
            [StringLength(ValidationConstants.MaxTitleLength)]
        [Display(Name = ValidationConstants.Title)]
        public string Title { get; set; }


        public string FilePath { get; set; }


        public string AltText { get; set; }


        [Display(Name = ValidationConstants.Album)]
        public int AlbumId { get; set; }

        public Album Album { get; set; }
    }
}
