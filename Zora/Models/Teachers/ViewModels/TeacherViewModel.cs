using System.ComponentModel.DataAnnotations;
using Zora.Shared.Domain.Common;

namespace Zora.Web.Models.Teachers.ViewModels
{
    public class TeacherViewModel
    {
        public int Id { get; set; }

        [Display(Name = ValidationConstants.FullName)]
        public string Name { get; set; }

        [Display(Name = ValidationConstants.Description)]
        public string Description { get; set; }

        public string FilePath { get; set; }

    }
}
