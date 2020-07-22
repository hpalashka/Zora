using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;
using Zora.Web.Data;
using Zora.Web.Models.Teachers.ViewModels;

namespace Zora.Web.Controllers
{

    public class TeachersController : Controller
    {
        private readonly ZoraDbContext _context;
        private readonly IConfiguration _configuration;

        public TeachersController(ZoraDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        public IActionResult Index()
        {
            var model = _context.Teachers
              .Select(t => new TeacherViewModel()
              {
                  Name = t.Name,
                  Description = t.Description,
                  FilePath = Path.Combine(_configuration.GetValue<string>("CustomSettings:TeachersPath"), t.CoverPhoto)
              })
             .ToList();
            return View(model);
        }

    }
}
