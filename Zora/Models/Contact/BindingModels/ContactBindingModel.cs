using Microsoft.AspNetCore.Http;
using Zora.Commons.Helpers;
using System.ComponentModel.DataAnnotations;
using Zora.Shared.Data;

namespace Zora.Web.Models.Contact.BindingModels
{
    public class ContactBindingModel
    {
        [Required(ErrorMessage = ValidationConstants.RequiredField)]
        [Display(Name = ValidationConstants.FullName)]
        public string FullName { get; set; }

        [Required(ErrorMessage = ValidationConstants.RequiredField)]
        [CustomEmailAttribute]
        [Display(Name = ValidationConstants.Email)]
        public string Email { get; set; }


        [Phone]
        [Display(Name = ValidationConstants.Phone)]
        public string Phone { get; set; }


        [Required(ErrorMessage = ValidationConstants.RequiredField)]
        [StringLength(ValidationConstants.MaxConactMessageLength)]
        [Display(Name = ValidationConstants.Messsage)]
        public string Message { get; set; }


        [Display(Name = ValidationConstants.UploadImage)]
        public IFormFile UploadImage { get; set; }

    }
}
