using System.ComponentModel.DataAnnotations;

namespace Zora.Identity.Models.Identity
{
    using static Data.DataConstants.Identity;
    public class UserInputModel
    {
        [EmailAddress]
        [Required]
        [MinLength(MinEmailLength)]
        [MaxLength(MaxEmailLength)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }


        [MaxLength(MaxNameLength)]
        public string Name { get; set; }
    }
}
