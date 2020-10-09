﻿using System.ComponentModel.DataAnnotations;
using Zora.Shared.Domain.Common;

namespace Zora.Web.Models.Images.ViewModels
{
    public class ImageConciseViewModel
    {
        public int Id { get; set; }


        [Display(Name = ValidationConstants.Title)]
        public string Title { get; set; }


        [Display(Name = ValidationConstants.Description)]
        public string Description { get; set; }


        public string FilePath { get; set; }
        public string ThumbPath { get; set; }
        public string AltText { get; set; }
        public int? GalleryId { get; set; }
    }
}
