
using System.ComponentModel.DataAnnotations;
using Zora.Shared.Data;

namespace Zora.Web.Models.Students.ViewModels
{
    public class StudentsViewModel
    {
        public int Id { get; set; }


        [Display(Name = ValidationConstants.FullName)]
        public string Name { get; set; }


        [Display(Name = ValidationConstants.Email)]
        public string Email { get; set; }
    }
}
