using System;
using System.ComponentModel.DataAnnotations;
using Zora.Shared.Domain.Common;

namespace Zora.Web.Data.Models
{
    public class Post
    {

        public int Id { get; set; }

        [Required]
        [StringLength(ValidationConstants.MaxTitleLength)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(ValidationConstants.MaxPostDescriptionLength)]
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        [StringLength(ValidationConstants.MaxFileNameLength)]
        public string ImageFile { get; set; }

    }
}
