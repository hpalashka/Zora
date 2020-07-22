using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Zora.Shared;
using Zora.Web.Data;
using Zora.Web.Data.Models;
using Zora.Web.Helpers;
using Zora.Web.Models.Posts.BindingModels;
using Zora.Web.Models.Posts.ViewModels;

namespace Zora.Web
{
    
    //[Authorize(Roles = Constants.AdministratorRoleName)]
    public class PostsAdminController : Controller
    {
        private readonly ZoraDbContext _context;
        private readonly IConfiguration _configuration;

        public PostsAdminController(ZoraDbContext context, IConfiguration configuration)
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
                    CreatedDate = p.CreatedDate.Date,
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
                CreatedDate = post.CreatedDate.Date
            };
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostBindingModel postData)
        {
            if (ModelState.IsValid)
            {

                string filePath = null;

                //upload image
                if (postData.ImageFile != null && postData.ImageFile.Length > 0)
                {
                    filePath = await UploadImageAsync(postData);
                    if (filePath == null)
                    {
                        ModelState.AddModelError("InvoiceFile", "Invalid File Type!");
                        return View();//todo
                    }
                }

                Post PostToAdd = new Post
                {
                    Title = postData.Title,
                    Description = postData.Description.Replace("\r\n", "<br />").Replace("\n", "<br />"),
                    ImageFile = filePath,
                    CreatedDate = DateTime.Now,
                };
                _context.Add(PostToAdd);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(postData);

        }


        public async Task<string> UploadImageAsync(PostBindingModel postData)
        {

            var ext = Path.GetExtension(postData.ImageFile.FileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(ext) || !Constants.permittedExtensions.Contains(ext))
            {
                return null;

            }
            else

            {
                string fileName = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0')
               + "_" + DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0')
               + "_" + DateTime.Now.Millisecond.ToString().PadLeft(4, '0') + ext;

                string filePath = Path.Combine(_configuration.GetValue<string>("CustomSettings:PostsUploadPath"), fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    await postData.ImageFile.CopyToAsync(stream);

                    UploadImageHelper.ResizeAndSaveImage(stream, Path.Combine(_configuration.GetValue<string>("CustomSettings:PostsUploadPath"), "thumb", fileName));
                }

                return fileName;
            }
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (post == null)
            {
                return NotFound();
            }

            string imagePath = null;
            if (post.ImageFile != null)
            {

                imagePath = Path.Combine(_configuration.GetValue<string>("CustomSettings:PostsPath"), post.ImageFile);
            }
            System.Text.RegularExpressions.Regex httpurlregex = new System.Text.RegularExpressions.Regex(@"(?<url>https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*))", System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled);


            var model = new PostBindingModel
            {
                Id = post.Id,
                Title = post.Title,
                Description = httpurlregex.Replace(post.Description, " <a href=\"${url}\" target=\"_blank\">${url}</a>"),
                ImagePath = imagePath,
                AltText = (post.Title),
                ImageFileFromDatabase = post.ImageFile,
                CreatedDate = post.CreatedDate,
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PostBindingModel postData)
        {
            if (id != postData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string filePath = null;

                    //upload image
                    if (postData.ImageFile != null && postData.ImageFile.Length > 0)
                    {
                        filePath = await UploadImageAsync(postData);
                        if (filePath == null)
                        {
                            ModelState.AddModelError("ImageFile", "Invalid File Type!");
                            return View();//todo
                        }
                    }
                    else if (postData.ConfirmDelete == true)
                    {
                        filePath = null;
                    }
                    else
                    {
                        filePath = postData.ImageFileFromDatabase;
                    }

                    Post oldpost = _context.Posts.AsNoTracking().Where(p => p.Id == postData.Id).FirstOrDefault();

                    Post PostToAdd = new Post
                    {
                        Id = postData.Id,
                        Title = postData.Title,
                        Description = postData.Description.Replace("\r\n", "<br />").Replace("\n", "<br />"),
                        ImageFile = filePath,
                        CreatedDate = oldpost.CreatedDate
                    };

                    _context.Update(PostToAdd);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(postData.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(postData);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            System.Text.RegularExpressions.Regex httpurlregex = new System.Text.RegularExpressions.Regex(@"(?<url>https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*))", System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled);

            var post = await _context.Posts.Select(p => new PostConciseIndexViewModel()
            {
                Id = p.Id,
                Description = httpurlregex.Replace(p.Description, " <a href=\"${url}\" target=\"_blank\">${url}</a>"),
                Title = p.Title,
                CreatedDate = p.CreatedDate,
                ImagePath = Path.Combine(_configuration.GetValue<string>("CustomSettings:PostsPath"), p.ImageFile)
            }).FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            string imageFile = GetFilePathUpdate(post.ImageFile);
            string thumbFile = GetThumbFilePathUpdate(post.ImageFile);

            if (System.IO.File.Exists(imageFile))
            {
                System.IO.File.Delete(imageFile);
            }

            if (System.IO.File.Exists(thumbFile))
            {
                System.IO.File.Delete(thumbFile);
            }
            return RedirectToAction(nameof(Index));
        }

        private string GetFilePathShow(string fileName)
        {
            return Path.Combine(_configuration.GetValue<string>("CustomSettings:PostsPath"), fileName);

        }

        private string GetFilePathUpdate(string fileName)
        {
            return Path.Combine(_configuration.GetValue<string>("CustomSettings:PostsUploadPath"), fileName);
        }

        private string GetThumbFilePathUpdate(string fileName)
        {
            return Path.Combine(_configuration.GetValue<string>("CustomSettings:PostsUploadPath"), "thumb", fileName);
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
