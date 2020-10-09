using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Zora.Shared.Domain.Common;

namespace Zora.Identity.Data.Models
{
    public class User : IdentityUser
    {

        [StringLength(ValidationConstants.MaxTitleLength)]
        public string Name { get; set; }

    }
}
