using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Zora.Shared.Infrastructure;
using Zora.Web.Models;

namespace Zora.Web.Controllers
{
    public class AdministrationTasksController : Controller
    {
        public IActionResult Index()
        {
            if (this.User.IsAdministrator())
            {
                return this.RedirectToAction("All");
            }

            return View();
        }

        public IActionResult All()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier
            });
    }
}
