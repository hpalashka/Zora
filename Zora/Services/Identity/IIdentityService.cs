using System.Threading.Tasks;
using Refit;
using Zora.Web.Models.Identity;

namespace Zora.Web.Services.Identity
{

    public interface IIdentityService
    {
        [Post("/Identity/Login")]
        Task<UserOutputModel> Login([Body] UserInputModel loginInput);


        [Post("/Identity/Register")]
        Task<UserOutputModel> Register([Body] UserInputModel model);

    }
}
