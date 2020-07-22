using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Zora.Web.Data;
using Zora.Web.Models.Posts.BindingModels;
using Zora.Web.Models.Posts.ViewModels;

namespace Zora.Web.Controllers
{
    public class PostsController : Controller
    {

        private readonly ZoraDbContext _context;
        private readonly IConfiguration _configuration;


        public PostsController(ZoraDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }

        public async Task<IActionResult> Index()
        {
            System.Text.RegularExpressions.Regex httpurlregex = new System.Text.RegularExpressions.Regex(@"(?<url>https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*))", System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled);
            var model = await _context.Posts.OrderByDescending(p => p.CreatedDate)
                .Select(p => new PostConciseIndexViewModel()
                {
                    Id = p.Id,
                    Description = httpurlregex.Replace(p.Description, " <a href=\"${url}\" target=\"_blank\">${url}</a>"),
                    Title = p.Title,
                    CreatedDate = p.CreatedDate,
                    ImagePath = Path.Combine(_configuration.GetValue<string>("CustomSettings:PostsPath"), p.ImageFile)
                })
               .ToListAsync();
            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            System.Text.RegularExpressions.Regex httpurlregex = new System.Text.RegularExpressions.Regex(@"(?<url>https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*))", System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled);

            string imagePath = null;
            if (post.ImageFile != null)
            {

                imagePath = Path.Combine(_configuration.GetValue<string>("CustomSettings:PostsPath"), post.ImageFile);
            }
            var model = new PostViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Description = httpurlregex.Replace(post.Description, " <a href=\"${url}\" target=\"_blank\">${url}</a>"),
                ImagePath = imagePath,
                ImageFileFromDatabase = post.ImageFile,
                CreatedDate = post.CreatedDate,
            };
            return View(model);
        }

    }
}
