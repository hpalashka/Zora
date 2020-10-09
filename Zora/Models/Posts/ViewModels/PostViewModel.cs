using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using Zora.Shared.Domain.Common;

namespace Zora.Web.Models.Posts.BindingModels
{
    public class PostViewModel
    {

        public int Id { get; set; }


        [Required(ErrorMessage = ValidationConstants.RequiredField)]
        [Display(Name = ValidationConstants.Title)]
        public string Title { get; set; }


        [Required(ErrorMessage = ValidationConstants.RequiredField)]
        [DataType(DataType.MultilineText)]
        [Display(Name = ValidationConstants.Description)]
        public string Description { get; set; }


        [Display(Name = ValidationConstants.Image)]
        public IFormFile ImageFile { get; set; }

        public string ImagePath { get; set; }

        public string ImageFileFromDatabase { get; set; }


        [Display(Name = ValidationConstants.CreatedDate)]
        public DateTime CreatedDate { get; set; }
    }
}
