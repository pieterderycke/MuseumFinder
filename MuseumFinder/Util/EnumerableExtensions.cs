using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumFinder.Util
{
    public static class EnumerableExtensions
    {
        public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            foreach(TSource item in source)
            {
                action(item);
            }
        }
    }
}
