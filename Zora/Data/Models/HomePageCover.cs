using System.ComponentModel.DataAnnotations;
using Zora.Shared.Data;

namespace Zora.Web.Data.Models
{
    public class HomePageCover
    {
        public int Id { get; set; }

        [StringLength(ValidationConstants.MaxTitleLength)]
        public string Title { get; set; }

        public string FileName { get; set; }

    }
}
