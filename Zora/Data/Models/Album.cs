using System.ComponentModel.DataAnnotations;
using Zora.Shared.Data;

namespace Zora.Web.Data.Models
{
    public class Album
    {
        public int Id { get; set; }

        [Required]
        [StringLength(ValidationConstants.MaxTitleLength)]
        public string Title { get; set; }


        [StringLength(ValidationConstants.MaxAlbumDescriptionLength)]
        public string Description { get; set; }


        [StringLength(ValidationConstants.MaxFolderLength)]
        public string AlbumFolderName { get; set; }

        //the name is always the same(AlbumCover), but we need this for the file extension
        [StringLength(ValidationConstants.CoverPhotoLength)]
        public string CoverPhoto { get; set; }

    }
}
