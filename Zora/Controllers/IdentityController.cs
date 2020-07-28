using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Zora.Web.Models.Identity;
using Zora.Web.Models.Identity.BindingModels;
using Zora.Web.Models.Students.BindingModels;
using Zora.Web.Services.Identity;
using Zora.Web.Services.Students;

namespace Zora.Web.Controllers
{
    using static Zora.Shared.Infrastructure.InfrastructureConstants;

    public class IdentityController : AdministrationController
    {
        private readonly IIdentityService identityService;
        private readonly IStudentsService _students;

        public IdentityController(
            IIdentityService identityService,
            IStudentsService students)
        {
            this.identityService = identityService;
            _students = students;
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
                                  Secure = false, //for docker
                                  MaxAge = TimeSpan.FromDays(1)
                              });
                      },

                      success: RedirectToAction("Profile", "Users"),
                      failure: RedirectToAction("Index", "Home"));


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegisterBindingModel model)
         => await this.Handle(
                async () =>
                {
                    var result = await this.identityService
                        .Register(new UserInputModel() { Email = model.Email, Password = model.Password, Name = model.Name });

                    this.Response.Cookies.Append(
                        AuthenticationCookieName,
                        result.Token,
                        new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = false, //for docker
                            MaxAge = TimeSpan.FromDays(1)
                        });
                },
                success: RedirectToAction("AddStudent", "Identity", new StudentBindingModel() { Email = model.Email, Name = model.Name }),
                failure: RedirectToAction(nameof(HomeController.Index), "Home"));

        [AllowAnonymous]
        public async Task<ActionResult> AddStudent(StudentBindingModel model)
        {
            var result = await _students.AddStudent(model);

            if (result == 0)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "Home");

        }


        // [AuthorizeAdministrator]
        public IActionResult Logout()
        {
            this.Response.Cookies.Delete(AuthenticationCookieName);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
