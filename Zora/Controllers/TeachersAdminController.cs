using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Zora.Shared.Domain.Common;
using Zora.Web.Data;
using Zora.Web.Data.Models;
using Zora.Web.Helpers;
using Zora.Web.Models.Teachers.BindingModels;
using Zora.Web.Models.Teachers.ViewModels;

namespace Zora.Web
{

    //[Authorize(Roles = Constants.AdministratorRoleName)]
    public class TeachersAdminController : Controller
    {
        private readonly ZoraDbContext _context;
        private readonly IConfiguration _configuration;

        public TeachersAdminController(ZoraDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        public IActionResult Index()
        {
            var model = _context.Teachers
              .Select(t => new TeacherViewModel()
              {
                  Id = t.Id,
                  Name = t.Name,
                  Description = t.Description,
                  FilePath = Path.Combine(_configuration.GetValue<string>("CustomSettings:TeachersPath"), t.CoverPhoto)
              })
             .ToList();
            return View(model);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeacherCreateBindingModel teacher)
        {
            if (ModelState.IsValid)
            {

                string filePath = null;

                //upload image
                if (teacher.ImageFile != null && teacher.ImageFile.Length > 0)
                {
                    filePath = await UploadImageAsync(teacher.ImageFile);
                    if (filePath == null)
                    {
                        ModelState.AddModelError("InvoiceFile", "Invalid File Type!");
                        return View();//todo
                    }
                }

                Teacher teacherToAdd = new Teacher
                {
                    Name = teacher.Name,
                    Description = teacher.Description.Replace("\r\n", "<br />").Replace("\n", "<br />"),
                    CoverPhoto = filePath
                };
                _context.Add(teacherToAdd);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(teacher);

        }

        //todo move out
        public async Task<string> UploadImageAsync(IFormFile image)
        {

            var ext = Path.GetExtension(image.FileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(ext) || !Constants.permittedExtensions.Contains(ext))
            {
                return null;

            }
            else

            {
                string fileName = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0')
               + "_" + DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0')
               + "_" + DateTime.Now.Millisecond.ToString().PadLeft(4, '0') + ext;

                string filePath = GetFilePathUpdate(fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    await image.CopyToAsync(stream);

                    UploadImageHelper.ResizeAndSaveImage(stream, GetThumbFilePathUpdate(fileName));
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

            var teacher = await _context.Teachers.Where(t => t.Id == id).FirstOrDefaultAsync();
            if (teacher == null)
            {
                return NotFound();
            }

            string imagePath = null;
            if (teacher.CoverPhoto != null)
            {
                imagePath = GetFilePathShow(teacher.CoverPhoto);
            }

            var model = new TeacherEditBindingModel
            {
                Id = teacher.Id,
                Description = teacher.Description.Replace("<br />", Environment.NewLine),
                Name = teacher.Name,
                ImagePath = GetFilePathShow(teacher.CoverPhoto),
                ImageFileFromDatabase = teacher.CoverPhoto
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TeacherEditBindingModel teacher)
        {
            if (id != teacher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string filePath = null;

                    //upload image
                    if (teacher.ImageFile != null && teacher.ImageFile.Length > 0)
                    {
                        filePath = await UploadImageAsync(teacher.ImageFile);
                        if (filePath == null)
                        {
                            ModelState.AddModelError("ImageFile", "Invalid File Type!");
                            return View();//todo
                        }
                    }
                    else
                    {
                        filePath = teacher.ImageFileFromDatabase;
                    }


                    Teacher teacherToUpdate = new Teacher
                    {
                        Id = teacher.Id,
                        Name = teacher.Name,
                        Description = teacher.Description.Replace("\r\n", "<br />").Replace("\n", "<br />"),
                        CoverPhoto = filePath
                    };

                    _context.Update(teacherToUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(teacher.Id))
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
            return View(teacher);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers.Select(t => new TeacherViewModel()
            {
                Id = t.Id,
                Description = t.Description,
                Name = t.Name,
                FilePath = Path.Combine(_configuration.GetValue<string>("CustomSettings:TeachersPath"), t.CoverPhoto)

            }).FirstOrDefaultAsync(m => m.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();

            string imageFile = GetFilePathUpdate(teacher.CoverPhoto);
            string thumbFile = GetThumbFilePathUpdate(teacher.CoverPhoto);

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


        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }


        private string GetFilePathShow(string fileName)
        {
            return Path.Combine(_configuration.GetValue<string>("CustomSettings:TeachersPath"), fileName);

        }

        private string GetFilePathUpdate(string fileName)
        {
            return Path.Combine(_configuration.GetValue<string>("CustomSettings:TeachersUploadPath"), fileName);
        }

        private string GetThumbFilePathUpdate(string fileName)
        {
            return Path.Combine(_configuration.GetValue<string>("CustomSettings:TeachersUploadPath"), "thumb", fileName);
        }
    }
}
