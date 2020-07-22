using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Zora.Shared;
using Zora.Web.Data;
using Zora.Web.Data.Models;
using Zora.Web.Helpers;
using Zora.Web.Models.HomePageCovers.BindingModels;
using Zora.Web.Models.HomePageCovers.ViewModels;

namespace Zora.Web.Areas.Admin.Controllers
{
   
    //[Authorize(Roles = Constants.AdministratorRoleName)]
    public class HomePageManagementController : Controller
    {
        private readonly ZoraDbContext _context;
        private readonly IConfiguration _configuration;


        public HomePageManagementController(ZoraDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        public IActionResult Index()
        {

            List<HomePageCoversViewModel> model = _context.HomePageCovers
                .Select(c => new HomePageCoversViewModel()
                {
                    Id = c.Id,
                    Title = c.Title,
                    FilePath = Path.Combine(_configuration.GetValue<string>("CustomSettings:CoversPath"), c.FileName) //??todo memori leak error when using methods???
                }).ToList();
            return View(model);
        }


        [Authorize(Roles = Constants.AdministratorRoleName)]
        public IActionResult UploadCoverPhotos()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> UploadCoverPhotos(HomePageCoversBindingModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.UploadImage.Length > 0)
                    {
                        var ext = Path.GetExtension(model.UploadImage.FileName).ToLowerInvariant();

                        if (string.IsNullOrEmpty(ext) || !Constants.permittedExtensions.Contains(ext))
                        {

                            ModelState.AddModelError("InvoiceFile", "Invalid File Type!");
                            return View();//todo
                        }
                        else
                        {
                            string fileName = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0')
                       + "_" + DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0')
                       + "_" + DateTime.Now.Millisecond.ToString().PadLeft(4, '0') + ext;

                            using (var stream = System.IO.File.Create(GetFilePathUpdate(fileName)))
                            {
                                await model.UploadImage.CopyToAsync(stream);
                                UploadImageHelper.ResizeAndSaveImage(stream, GetThumbFilePathUpdate(fileName));
                            }

                            HomePageCover image = new HomePageCover()
                            {
                                Title = model.Title,
                                FileName = fileName
                            };

                            _context.HomePageCovers.Add(image);
                        }
                    }

                    _context.SaveChanges();
                    return RedirectToAction("Index", "HomePageManagement");

                }
                else
                {
                    return View(model);
                }
            }

            catch
            {
                throw;
            }
        }



        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = await _context.HomePageCovers.Where(c => c.Id == id).FirstOrDefaultAsync(i => i.Id == id);

            if (image == null)
            {
                return NotFound();
            }

            HomePageCoversViewModel model = new HomePageCoversViewModel()
            {
                Id = image.Id,
                Title = image.Title,
                FilePath = GetFilePathShow(image.FileName)
            };
            return View(model);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var image = await _context.HomePageCovers.Where(c => c.Id == id).FirstOrDefaultAsync(i => i.Id == id);
            _context.HomePageCovers.Remove(image);
            await _context.SaveChangesAsync();

            //delete image from folders
            string filePath = GetFilePathUpdate(image.FileName);
            string thumbFilePath = GetThumbFilePathUpdate(image.FileName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            if (System.IO.File.Exists(thumbFilePath))
            {
                System.IO.File.Delete(thumbFilePath);
            }

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var image = _context.HomePageCovers.Where(c => c.Id == Id).FirstOrDefault(i => i.Id == Id);
            if (image == null)
            {
                return NotFound();
            }


            var model = new HomePageCoversEditBindingModel()
            {
                Id = image.Id,
                Title = image.Title,
                FilePath = GetFilePathShow(image.FileName)
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, HomePageCoversEditBindingModel model)
        {
            if (Id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                var image = _context.HomePageCovers.AsNoTracking().Where(i => i.Id == Id).FirstOrDefault();
                if (image == null)
                {
                    return NotFound();
                }

                using (var stream = System.IO.File.Create(GetFilePathUpdate(model.UploadImage.FileName)))
                {
                    await model.UploadImage.CopyToAsync(stream);
                    UploadImageHelper.ResizeAndSaveImage(stream, GetThumbFilePathUpdate(model.UploadImage.FileName));
                }

                image.Title = model.Title;
                image.FileName = model.UploadImage.FileName;
                _context.Update(image);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }


        }

        private string GetFilePathShow(string fileName)
        {
            return Path.Combine(_configuration.GetValue<string>("CustomSettings:CoversPath"), fileName);

        }

        private string GetFilePathUpdate(string fileName)
        {
            return Path.Combine(_configuration.GetValue<string>("CustomSettings:CoversUploadPath"), fileName);
        }

        private string GetThumbFilePathUpdate(string fileName)
        {
            return Path.Combine(_configuration.GetValue<string>("CustomSettings:CoversUploadPath"), "thumb", fileName);
        }
    }

}


