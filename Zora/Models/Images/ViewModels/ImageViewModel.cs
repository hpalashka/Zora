using System.ComponentModel.DataAnnotations;
using Zora.Shared.Data;

namespace Zora.Web.Models.Images.ViewModels
{
    public class ImageViewModel
    {
        public int Id { get; set; }


        [Display(Name = ValidationConstants.Title)]
        public string Title { get; set; }

        public string FilePath { get; set; }
        public string ThumbPath { get; set; }
        public string AltText { get; set; }
       
    }
}
