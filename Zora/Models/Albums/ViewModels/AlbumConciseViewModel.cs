using System.ComponentModel.DataAnnotations;
using Zora.Shared.Domain.Common;

namespace Zora.Web.Models.Albums.ViewModels
{
    public class AlbumConciseViewModel
    {
        public int Id { get; set; }


        [Display(Name = ValidationConstants.Title)]
        public string Title { get; set; }

        public string AlbumFolerName { get; set; }

        public string FileName { get; set; }
    }
}
