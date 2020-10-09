using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
using Zora.Web.Models.Albums.BindingModels;
using Zora.Web.Models.Albums.ViewModels;
using Zora.Web.Models.Images.BindingModels;
using Zora.Web.Models.Images.ViewModels;

namespace Zora.Web
{

    //[Authorize(Roles = Constants.AdministratorRoleName)]
    public class AlbumsController : Controller
    {
        private readonly ZoraDbContext _context;
        private readonly IConfiguration _configuration;


        public AlbumsController(ZoraDbContext context, IConfiguration configuration)
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


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums.FirstOrDefaultAsync(m => m.Id == id);

            if (album == null)
            {
                return NotFound();
            }

            var albumImagesCount = _context.StoreImages.Where(i => i.Album.Id == album.Id).Count();

            var AlbumViewModel = new AlbumDetailsViewModel()
            {
                Id = album.Id,
                Title = album.Title,
                ImagesCount = albumImagesCount,
                Description = album.Description,
                FileName = Path.Combine(_configuration.GetValue<string>("CustomSettings:ImagesPath"), album.AlbumFolderName, album.CoverPhoto)
            };

            return View(AlbumViewModel);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AlbumCreateBindingModel album)//*
        {
            if (ModelState.IsValid)
            {
                //generate folder name, remove any chatacters that are not allowed
                string albumFolderName = FileHelpers.GetValidAlbumFolderName(album.Title.Trim());

                //create album folder
                string albumPath = GetAlbumPath(albumFolderName);
                if (!Directory.Exists(albumPath))
                    Directory.CreateDirectory(albumPath);

                //create thumbs folder
                string albumPathThumb = Path.Combine(albumPath, "thumb");
                if (!Directory.Exists(albumPathThumb))
                    Directory.CreateDirectory(albumPathThumb);

                string fileName;
                if (album.UploadImage != null)
                {
                    fileName = await PrepareImage.CoverPhotoAsync(album.UploadImage, albumPath, albumPathThumb, "AlbumCover");
                    if (fileName == null)
                    {
                        ModelState.AddModelError("InvoiceFile", "Invalid File Type!");
                        return View();
                    }
                }
                else
                {
                    fileName = "AlbumCover.jpg";
                    System.IO.File.Copy(Path.Combine(_configuration.GetValue<string>("CustomSettings:ImagesRootPath", ".\\Images"), fileName),
                      Path.Combine(albumPath, fileName));

                }


                Album newAlbum = new Album()
                {
                    AlbumFolderName = albumFolderName,
                    Title = album.Title,
                    CoverPhoto = fileName,
                    Description = album.Description
                };

                _context.Add(newAlbum);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(album);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (album == null)
            {
                return NotFound();
            }

            var model = new AlbumEditBindingModel
            {
                Id = album.Id,
                Title = album.Title,
                Description = album.Description,
                FileName = Path.Combine(_configuration.GetValue<string>("CustomSettings:ImagesPath"), FileHelpers.GetValidAlbumFolderName(album.Title.Trim()), album.CoverPhoto)
            };
            return View(model);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AlbumEditBindingModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Album oldAlbum = _context.Albums.AsNoTracking().Where(a => a.Id == model.Id).FirstOrDefault();

                    string folderName = FileHelpers.GetValidAlbumFolderName(model.Title.Trim());
                    string albumFolder = GetAlbumPath(folderName);
                    string albumThumbFolder = Path.Combine(albumFolder, "thumb");

                    //if the album name has been changed, then update folder name
                    if (oldAlbum.Title.Trim() != model.Title.Trim())
                    {
                        string sourcePath = GetAlbumPath(oldAlbum.AlbumFolderName);

                        if (sourcePath != albumFolder)
                        {
                            Directory.Move(sourcePath, albumFolder);
                        }
                    }

                    //replace  image if changed
                    string fileName = string.Empty;
                    if (model.UploadImage != null)
                    {
                        fileName = await PrepareImage.CoverPhotoAsync(model.UploadImage, albumFolder, albumThumbFolder, "AlbumCover");
                    }

                    Album newAlbum = new Album()
                    {
                        Id = model.Id,
                        Title = model.Title.Trim(),
                        CoverPhoto = string.IsNullOrEmpty(fileName) ? oldAlbum.CoverPhoto : fileName,
                        AlbumFolderName = folderName,
                        Description = model.Description
                    };

                    _context.Update(newAlbum);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumExists(model.Id))
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
            return View(model);
        }

        public void DeleteDirectory(string folder)
        {
            if (Directory.Exists(folder))
            {
                Directory.Delete(folder, true);
            }
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var album = await _context.Albums
                .Select(a => new AlbumConciseViewModel()
                {
                    Id = a.Id,
                    Title = a.Title
                })
                .FirstOrDefaultAsync(a => a.Id == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var album = await _context.Albums.FindAsync(id);
            _context.Albums.Remove(album);
            await _context.SaveChangesAsync();

            string albumFolder = GetAlbumPath(album.AlbumFolderName);

            if (Directory.Exists(albumFolder))
            {
                DeleteDirectory(albumFolder);
            }

            return RedirectToAction(nameof(Index));
        }


        private bool AlbumExists(int id)
        {
            return _context.Albums.Any(e => e.Id == id);
        }


        public IActionResult UploadImage(int Id)
        {
            var model = new UploadImageBindingModel()
            {
                AlbumId = Id
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> UploadImage(UploadImageBindingModel model)
        {
            try
            {
                if (ModelState.IsValid)

                {

                    //get current album name
                    var album = _context.Albums.Where(a => a.Id == model.AlbumId).FirstOrDefault();

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

                            string filePath = Path.Combine(_configuration.GetValue<string>("CustomSettings:UploadPath"), album.AlbumFolderName, fileName);

                            using (var stream = System.IO.File.Create(filePath))
                            {
                                await model.UploadImage.CopyToAsync(stream);

                                UploadImageHelper.ResizeAndSaveImage(stream, Path.Combine(_configuration.GetValue<string>("CustomSettings:UploadPath"), album.AlbumFolderName, "thumb", fileName));
                            }


                            StoreImage image = new StoreImage()
                            {
                                AlbumId = album.Id,
                                Title = model.Title,
                                FileName = fileName,
                                UploadDate = DateTime.Now,
                            };

                            _context.StoreImages.Add(image);
                        }

                    }

                    _context.SaveChanges();
                    return RedirectToAction("Images", "Albums", new { id = album.Id });

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


        public void GetAlbums()
        {
            var albums = _context.Albums.ToList();

            ViewData["Albums"] = new SelectList(albums, "Id", "Title");
        }


        public IActionResult Images(int Id)
        {
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
                }).ToList();

            ViewData["Album"] = album.Title;

            return View(model);
        }


        public IActionResult EditImage(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var image = _context.StoreImages.Where(i => i.Id == Id).FirstOrDefault();
            if (image == null)
            {
                return NotFound();
            }


            var album = _context.Albums.Where(a => a.Id == image.AlbumId).FirstOrDefault();
            string AlbumPath = Path.Combine(_configuration.GetValue<string>("CustomSettings:ImagesPath"), album.AlbumFolderName);
            string FilePath = Path.Combine(AlbumPath, image.FileName);

            var model = new EditImageBindingModel()
            {
                Id = image.Id,
                Title = image.Title,
                Album = image.Album,
                AlbumId = image.Album.Id,
                FilePath = FilePath,
                AltText = image.Title
            };


            //load albums
            GetAlbums();

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditImage(int Id, EditImageBindingModel model)
        {
            if (Id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var image = _context.StoreImages.AsNoTracking().Where(i => i.Id == Id).FirstOrDefault();
                    if (image == null)
                    {
                        return NotFound();
                    }

                    //move file to the new album's folder
                    if (image.AlbumId != model.AlbumId)

                    {

                        var destAlbum = _context.Albums.Where(a => a.Id == model.AlbumId).FirstOrDefault();
                        string destAlbumPath = Path.Combine(_configuration.GetValue<string>("CustomSettings:UploadPath"), destAlbum.AlbumFolderName);
                        string destFilePath = Path.Combine(destAlbumPath, image.FileName);
                        string destThubmsFilePath = Path.Combine(destAlbumPath, "thumb", image.FileName);

                        var sourceAlbum = _context.Albums.Where(a => a.Id == image.AlbumId).FirstOrDefault();
                        string sourceAlbumPath = Path.Combine(_configuration.GetValue<string>("CustomSettings:UploadPath"), sourceAlbum.AlbumFolderName);
                        string sourceFilePath = Path.Combine(sourceAlbumPath, image.FileName);
                        string sourceThumbFilePath = Path.Combine(sourceAlbumPath, "thumb", image.FileName);

                        System.IO.File.Move(sourceFilePath, destFilePath);

                        System.IO.File.Move(sourceThumbFilePath, destThubmsFilePath);

                    };

                    image.Title = model.Title;
                    image.AlbumId = model.AlbumId;
                    _context.Update(image);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumExists(model.Id))//todo 
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction("Images", new { id = model.AlbumId });
            }
            return View(model);
        }


        public async Task<IActionResult> DeleteImage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = await _context.StoreImages.FirstOrDefaultAsync(i => i.Id == id);

            if (image == null)
            {
                return NotFound();
            }


            Album album = _context.Albums.Where(a => a.Id == image.AlbumId).FirstOrDefault();
            string FilePath = Path.Combine(_configuration.GetValue<string>("CustomSettings:ImagesPath"), album.AlbumFolderName, image.FileName);

            ImageConciseViewModel model = new ImageConciseViewModel()
            {
                Id = image.Id,
                Title = image.Title,
                AltText = image.Title,
                FilePath = FilePath
            };


            return View(model);
        }


        [HttpPost, ActionName("DeleteImage")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteImageConfirmed(int id)
        {

            var image = await _context.StoreImages.FirstOrDefaultAsync(i => i.Id == id);
            _context.StoreImages.Remove(image);
            await _context.SaveChangesAsync();

            //delete image from folders
            var album = _context.Albums.Where(a => a.Id == image.AlbumId).FirstOrDefault();
            string albumPath = Path.Combine(_configuration.GetValue<string>("CustomSettings:UploadPath"), album.AlbumFolderName);
            string filePath = Path.Combine(albumPath, image.FileName);
            string thubmsFilePath = Path.Combine(albumPath, "thumb", image.FileName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            if (System.IO.File.Exists(thubmsFilePath))
            {
                System.IO.File.Delete(thubmsFilePath);
            }


            return RedirectToAction(nameof(Index));
        }


        public string GetAlbumPath(string albumName)
        {
            return Path.Combine(_configuration.GetValue<string>("CustomSettings:UploadPath"), albumName);

        }

    }
}
