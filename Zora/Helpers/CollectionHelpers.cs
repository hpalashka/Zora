using System.Collections.Generic;
using System.Linq;
using Zora.Web.Models.Images.ViewModels;

namespace Zora.Web.Helpers
{
    public static class CollectionHelpers
    {
        public static IEnumerable<IEnumerable<ImageViewModel>> Chunk<T>(this IEnumerable<ImageViewModel> source, int chunksize)
        {
            return source.Select((x, i) => new { x, i })
             .GroupBy(xi => xi.i / chunksize, xi => xi.x)
             .Select(g => g.ToArray());
        }
    }
}
