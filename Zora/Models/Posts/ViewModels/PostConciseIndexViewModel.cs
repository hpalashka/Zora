using System;
using System.ComponentModel.DataAnnotations;
using Zora.Shared.Data;

namespace Zora.Web.Models.Posts.ViewModels
{
    public class PostConciseIndexViewModel
    {

        public int Id { get; set; }


        [Display(Name = ValidationConstants.Title)]
        public string Title { get; set; }


        [Display(Name = ValidationConstants.Description)]
        public string Description { get; set; }


        [Display(Name = ValidationConstants.CreatedDate)]
        public DateTime CreatedDate { get; set; }

        public string ImagePath { get; set; }
    }
}
