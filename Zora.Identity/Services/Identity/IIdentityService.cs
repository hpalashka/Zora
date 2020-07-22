using System.Threading.Tasks;
using Zora.Identity.Data.Models;
using Zora.Identity.Models.Identity;
using Zora.Shared.Services;

namespace Zora.Identity.Services.Identity
{

    public interface IIdentityService
    {
        Task<Result<User>> Register(UserInputModel userInput);

        Task<Result<UserOutputModel>> Login(UserInputModel userInput);

        Task<Result> ChangePassword(string userId, ChangePasswordInputModel changePasswordInput);
    }
}
