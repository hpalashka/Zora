using Microsoft.AspNetCore.Mvc;
using Refit;
using System.Threading.Tasks;
using Zora.Shared.Infrastructure;
using Zora.Shared.Services.Identity;
using Zora.Web.Services.Payments;
using Zora.Web.Services.Students;

namespace Zora.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IStudentsService _students;
        private readonly IPaymentsService _payments;
        private readonly ICurrentUserService _user;

        public UsersController(IStudentsService students, IPaymentsService payments, ICurrentUserService user)
        {
            _students = students;
            _payments = payments;
            _user = user;
        }


        public IActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Profile");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            return View(await _students.Student(_user.Email));
        }

        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> Payments(int id)
        
        {
            return View(await _payments.Payments(id));
        }


        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Logout()
        {
            this.Response.Cookies.Delete(InfrastructureConstants.AuthenticationCookieName);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }



    }
}
