// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Cathei.LinqGen.Hidden
{
    public static class OrderByUtils
    {
        public static void PartialQuickSort<TComparer>(
                int[] indexesToSort, in TComparer comparer, int left, int right, int min, int max)
            where TComparer : IComparer<int>
        {
            do
            {
                int mid = PartitionHoare(indexesToSort, comparer, left, right);

                if (left < mid && mid >= min)
                    PartialQuickSort(indexesToSort, comparer, left, mid, min, max);

                left = mid + 1;

            } while (left < right && left <= max);
        }

        // Hoare partition scheme
        // This implementation is faster when using struct comparer (more comparison and less copy)
        private static int PartitionHoare<TComparer>(int[] indexesToSort, TComparer comparer, int left, int right)
            where TComparer : IComparer<int>
        {
            // preventing overflow of the pivot
            int pivot = left + ((right - left) >> 1);
            int pivotIndex = indexesToSort[pivot];

            int i = left - 1;
            int j = right + 1;

            while (true)
            {
                // Move the left index to the right at least once and while the element at
                // the left index is less than the pivot
                while (comparer.Compare(indexesToSort[++i], pivotIndex) < 0) { }

                // Move the right index to the left at least once and while the element at
                // the right index is greater than the pivot
                while (comparer.Compare(indexesToSort[--j], pivotIndex) > 0) { }

                // If the indices crossed, return
                if (i >= j)
                    return j;

                // Swap the elements at the left and right indices
                (indexesToSort[i], indexesToSort[j]) = (indexesToSort[j], indexesToSort[i]);
            }
        }
    }
}