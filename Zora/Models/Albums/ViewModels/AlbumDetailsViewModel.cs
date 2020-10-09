using System.ComponentModel.DataAnnotations;
using Zora.Shared.Domain.Common;

namespace Zora.Web.Models.Albums.ViewModels
{
    public class AlbumDetailsViewModel
    {
        public int Id { get; set; }


        [Display(Name = ValidationConstants.Title)]
        public string Title { get; set; }


        [Display(Name = ValidationConstants.Description)]
        public string Description { get; set; }


        [Display(Name = ValidationConstants.ImagesCount)]
        public int ImagesCount { get; set; }


        public string Author { get; set; }

        public string FileName { get; set; }
    }
}
