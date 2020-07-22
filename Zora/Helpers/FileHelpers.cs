using Zora.Commons.Helpers;
using System.IO;

namespace Zora.Web.Helpers
{
    public static class FileHelpers
    {
        public static string GetValidAlbumFolderName(string albumName)
        {
            string transliteratedString = albumName.Unidecode();
            string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());

            foreach (char c in invalid)
            {
                transliteratedString = transliteratedString.Replace(c.ToString(), "");
            }
        
            return transliteratedString.Trim();
        }

     

    }
}
