using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Zora.Shared.Infrastructure;
using Zora.Web.Models.Identity;
using Zora.Web.Services.Identity;

namespace Zora.Web.Controllers
{
    using static Zora.Shared.Infrastructure.InfrastructureConstants;

    public class IdentityController : AdministrationController
    {
        private readonly IIdentityService identityService;


        public IdentityController(
            IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginFormModel model)
            => await this.Handle(
                async () =>
                {
                    UserInputModel user = new UserInputModel()
                    {
                        Email = model.Email,
                        Password = model.Password
                    };

                    var result = await this.identityService
                        .Login(user);

                    this.Response.Cookies.Append(
                        AuthenticationCookieName,
                        result.Token,
                        new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            MaxAge = TimeSpan.FromDays(1)
                        });
                },
              
                success: RedirectToAction("Index", "AdministrationTasks"),
                failure: View("../Home/Index", model));

        [AuthorizeAdministrator]
        public IActionResult Logout()
        {
            this.Response.Cookies.Delete(AuthenticationCookieName);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
