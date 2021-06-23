using System;

namespace CacheBenchmark
{
    public class MergeSort
    {

        // Merges two subarrays of []arr.
        // First subarray is arr[l..m]
        // Second subarray is arr[m+1..r]
        private void Merge<T>(T[] arr, int l, int m, int r, Func<T,T,bool> sortFunc)
        {
            // Find sizes of two
            // subarrays to be merged
            int n1 = m - l + 1;
            int n2 = r - m;

            // Create temp arrays
            T[] L = new T[n1];
            T[] R = new T[n2];

            int i, j;
            // Copy data to temp arrays
            for (i = 0; i < n1; ++i)
                L[i] = arr[l + i];
            for (j = 0; j < n2; ++j)
                R[j] = arr[m + 1 + j];

            // Initial index of merged
            // subarry array
            i = j = 0;
            int k = l;
            while (i < n1 && j < n2)
            {
                if (sortFunc(L[i], R[j]))
                {
                    arr[k] = L[i];
                    i++;
                }
                else
                {
                    arr[k] = R[j];
                    j++;
                }
                k++;
            }

            // Copy remaining elements
            // of L[] if any
            while (i < n1)
            {
                arr[k] = L[i];
                i++;
                k++;
            }

            // Copy remaining elements
            // of R[] if any
            while (j < n2)
            {
                arr[k] = R[j];
                j++;
                k++;
            }
        }

        // Main function that
        // sorts arr[l..r] using
        // merge()
        //public T[] Sort<T>(T[] arr, int l, int r)
        public T[] Sort<T>(T[] list, int l, int r, Func<T,T,bool> mergeFunc)
        {
            if (l < r)
            {
                // Find the middle
                // point
                int m = l + (r - l) / 2;
                
                // Sort first and
                // second halves
                Sort(list, l, m, mergeFunc);
                Sort(list, m + 1, r, mergeFunc);

                // Merge the sorted halves
                Merge(list, l, m, r, mergeFunc);
            }

            return list;
        }

    }
}
