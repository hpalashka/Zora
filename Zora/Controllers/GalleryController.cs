using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Zora.Shared;
using Zora.Shared.Services;
using Zora.Web.Data;
using Zora.Web.Models.Albums.ViewModels;
using Zora.Web.Models.Images.ViewModels;

namespace Zora.Web.Controllers
{
    public class GalleryController : Controller
    {

        private readonly ZoraDbContext _context;
        private readonly IConfiguration _configuration;


        public GalleryController(
            ZoraDbContext context,
                    IConfiguration configuration

            )
        {
            _context = context;
            _configuration = configuration;

        }
        public async Task<IActionResult> Index()
        {

            return View(await _context.Albums
                .Select(a => new AlbumConciseViewModel()
                {
                    Id = a.Id,
                    Title = a.Title,
                    AlbumFolerName = a.AlbumFolderName,
                    FileName = Path.Combine(_configuration.GetValue<string>("CustomSettings:ImagesPath"), a.AlbumFolderName, a.CoverPhoto)
                }).ToListAsync());
        }


        public async Task<IActionResult> Images(int? Id, int? page)
        {

            if (Id == null)
            {
                return NotFound();
            }

            var album = _context.Albums.Where(a => a.Id == Id).FirstOrDefault();
            string albumpath = Path.Combine(_configuration.GetValue<string>("CustomSettings:ImagesPath"), album.AlbumFolderName);


            var model = _context.StoreImages
                .Where(a => a.Album.Id == Id).OrderByDescending(i => i.Id)
                .Select(i => new ImageViewModel
                {
                    Id = i.Id,
                    Title = i.Title,
                    FilePath = Path.Combine(albumpath, i.FileName),
                    ThumbPath = Path.Combine(albumpath, "thumb", i.FileName),
                    AltText = i.Title
                });

            ViewData["Gallery"] = album.Title;
            ViewData["GalleryDescription"] = album.Description;
            return View(await PaginatedList<ImageViewModel>.CreateAsync(model.AsNoTracking(), page ?? 1, Constants.PageSize));
        }


    }
}