using Microsoft.AspNetCore.Http;
using Zora.Commons.Helpers;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Zora.Shared;

namespace Zora.Web.Helpers
{
    public static class PrepareImage
    {
        public static async Task<string> CoverPhotoAsync(IFormFile file, string mainFolder, string thumbsFolder, string coverPhotoName)
        {
            string fileName = string.Empty;

            if (file.Length > 0)
            {
                var ext = Path.GetExtension(file.FileName).ToLowerInvariant();

                if (string.IsNullOrEmpty(ext) || !Constants.permittedExtensions.Contains(ext))
                {

                    return null;
                }
                else
                {
                    fileName = coverPhotoName + ext;

                    using (var stream = System.IO.File.Create(Path.Combine(mainFolder, fileName)))
                    {
                        await file.CopyToAsync(stream);

                        UploadImageHelper.ResizeAndSaveImage(stream, Path.Combine(thumbsFolder, fileName));
                    }

                    return fileName;
                }
            }
            else
            {
                return null;
            }

        }

    }
}
