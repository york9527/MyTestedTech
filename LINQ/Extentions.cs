using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ
{
    public static class Extentions
    {
        public static IEnumerable<string> IsImageFile(
            this IEnumerable<string> files, Predicate<string> isMatch)
        {
            foreach (var file in files)
            {
                if (isMatch(file))
                    yield return file;
            }
        }

        public static IEnumerable<string> IsImageFile(
            this IEnumerable<string> files)
        {
            foreach (string file in files)
            {
                if (file.Contains(".jpg") || file.Contains(".png") || file.Contains(".bmp"))
                    yield return file;
            }
        }
    }
}
