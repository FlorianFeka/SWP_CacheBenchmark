using System.Collections.Generic;

namespace CacheBenchmark
{
    public class BucketSort
    {
        // Function to sort arr[] of size n
        // using bucket sort
        public void Sort(float[] arr, int n)
        {
            if (n <= 0)
            {
                return;
            }
            
            // 1) Create n empty buckets
            List<float>[] buckets = new List<float>[n];

            for (int i = 0; i < n; i++)
            {
                buckets[i] = new List<float>();
            }

            float idx = 0;
            // 2) Put array elements in different buckets
            for (int i = 0; i < n; i++)
            {
                idx = (arr[i] * n >= n) ? (idx = arr[i] / n) : (idx = arr[i] * n);

                buckets[(int)idx].Add(arr[i]);
            }

            // 3) Sort individual buckets
            for (int i = 0; i < n; i++)
            {
                buckets[i].Sort();
            }

            // 4) Concatenate all buckets into arr[]
            int index = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < buckets[i].Count; j++)
                {
                    arr[index++] = buckets[i][j];
                }
            }
        }
    }
}
