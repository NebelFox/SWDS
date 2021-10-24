namespace BracketsDepth
{
    public static class Sorter
    {
        public delegate int Compare<in TValue>(TValue lhs, TValue rhs);

        public static void Sort<TValue>(TValue[] array, Compare<TValue> comparer)
        {
            QuickSort(array, comparer, 0, array.Length - 1);
        }

        private static void QuickSort<TValue>(TValue[] array,
                                              Compare<TValue> compare,
                                              int left,
                                              int right)
        {
            int i = left;
            int j = right;
            int pivot = left + ((right - left + 1) >> 1);

            while (i <= j)
            {
                while (compare(array[i], array[pivot]) == -1)
                    ++i;
                while (compare(array[j], array[pivot]) == 1)
                    --j;
                if (i <= j)
                    (array[i], array[j]) = (array[j--], array[i++]);
            }
            if (j > left)
                QuickSort(array, compare, left, j);
            if (i < right)
                QuickSort(array, compare, i, right);
        }
    }
}
