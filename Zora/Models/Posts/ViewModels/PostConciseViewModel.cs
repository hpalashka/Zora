using System;
using System.ComponentModel.DataAnnotations;
using Zora.Shared.Domain.Common;

namespace Zora.Web.Models.Posts.BindingModels
{
    public class PostConciseViewModel
    {

        public int Id { get; set; }


        [Display(Name = ValidationConstants.Title)]
        public string Title { get; set; }


        [Display(Name = ValidationConstants.CreatedDate)]
        public DateTime CreatedDate { get; set; }

       
    }
}
