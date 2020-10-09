using System;
using System.ComponentModel.DataAnnotations;
using Zora.Shared.Domain.Common;

namespace Zora.Web.Data.Models
{
    public class StoreImage
    {
        public int Id { get; set; }

        [StringLength(ValidationConstants.MaxTitleLength)]
        public string Title { get; set; }

        [StringLength(ValidationConstants.MaxFileNameLength)]
        public string FileName { get; set; }

        public DateTime UploadDate { get; set; }

        public int AlbumId { get; set; }
        public virtual Album Album { get; set; }

    }
}
