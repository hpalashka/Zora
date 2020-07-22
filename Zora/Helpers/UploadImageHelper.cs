using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.IO;

namespace Zora.Web.Helpers
{
    public class UploadImageHelper
    {

        public static void ResizeAndSaveImage(Stream stream, string fileName)//, int width, int height)
        {
            //todo research for the best way to resise???
            stream.Position = 0;
            using (Image<Rgba32> image = (Image<Rgba32>)Image.Load(stream))
            {
                image.Mutate(x => x
                     .Resize(image.Width / 5, image.Height / 5)
                 );
                image.Save(fileName);
            }
        }
    }
}
