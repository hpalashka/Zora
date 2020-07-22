using System.ComponentModel.DataAnnotations;
using Zora.Shared.Data;

namespace Zora.Students.Data.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        [StringLength(ValidationConstants.MaxTitleLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(ValidationConstants.MaxTitleLength)]
        public string Email { get; set; }

    }
}
