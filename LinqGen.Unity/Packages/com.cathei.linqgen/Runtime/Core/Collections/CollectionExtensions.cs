// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections.Generic;

namespace Cathei.LinqGen
{
    public static class CollectionExtensions
    {
        public static int RemoveAll<T, TPredicate>(this List<T> list, TPredicate predicate)
            where TPredicate : IStructFunction<T, bool>
        {
            int count = list.Count;
            int free = 0;

            while (free < count && !predicate.Invoke(list[free]))
                ++free;

            // nothing to remove
            if (free >= count)
                return 0;

            int current = free + 1;

            while (current < count)
            {
                // skip this element
                if (predicate.Invoke(list[current]))
                {
                    ++current;
                    continue;
                }

                // preserve current element
                list[free++] = list[current++];
            }

            int result = count - free;

            list.RemoveRange(free, result);

            return result;
        }
    }
}