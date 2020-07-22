using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;
using Zora.Web.Data;
using Zora.Web.Models.HomePageCovers.BindingModels;

namespace Zora.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly ZoraDbContext _context;
               private readonly IConfiguration _configuration;
   
        public HomeController(ZoraDbContext context,IConfiguration configuration)
        {
            _context = context;
                      _configuration = configuration;
                  }

        public IActionResult Index()
        {
            string folderPath = _configuration.GetValue<string>("CustomSettings:CoversPath");

            var coverImages = _context.HomePageCovers
                .Select(image => new HomePageCoversEditBindingModel()
            {
                Id = image.Id,
                Title = image.Title,
               FilePath = Path.Combine(folderPath, image.FileName)
            }).ToList();
        
            return View(coverImages);
        }

        //todo move admin area
        public IActionResult Administration()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}


        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }





}
