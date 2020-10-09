using System.ComponentModel.DataAnnotations;
using Zora.Shared.Domain.Common;

namespace Zora.Web.Data.Models
{
    public class Teacher
    {
        public int Id { get; set; }

        [Required]
        [StringLength(ValidationConstants.MaxTitleLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(ValidationConstants.MaxTeacherescriptionLength)]
        public string Description { get; set; }

        [Required]
        [StringLength(ValidationConstants.CoverPhotoLength)]
        public string CoverPhoto { get; set; }

    }
}
