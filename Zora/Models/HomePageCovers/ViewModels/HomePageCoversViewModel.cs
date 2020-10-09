using System.ComponentModel.DataAnnotations;
using Zora.Shared.Domain.Common;

namespace Zora.Web.Models.HomePageCovers.ViewModels
{
    public class HomePageCoversViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = ValidationConstants.RequiredField)]
            [StringLength(ValidationConstants.MaxTitleLength)]
        [Display(Name = ValidationConstants.Title)]
        public string Title { get; set; }

        public string FilePath { get; set; }
    }
}
