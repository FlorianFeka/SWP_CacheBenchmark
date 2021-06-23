using System;
using System.Collections.Generic;

namespace CacheBenchmark
{
    public class BucketSort
    {
        // Function to sort arr[] of size n
        // using bucket sort
        public void Sort<T, G>(T[] arr, int n, Func<T, G> selectItem, Func<G, G, bool> sortFunc) where G : struct
        {
            if(typeof(G) != typeof(int) && typeof(G) != typeof(float))
            {
                throw new Exception("Can only sort numbers.");
            }

            if (n <= 0 || n < arr.Length)
            {
                throw new ArgumentException(nameof(n));
            }
            
            // 1) Create n empty buckets
            List<T>[] buckets = new List<T>[n];


            for (int i = 0; i < n; i++)
            {
                buckets[i] = new List<T>();
            }

            //float idx = 0;

            var max = GetMax(arr, selectItem);
            var min = GetMin(arr, selectItem);
            var elementRange = (int)(max - min) / n + 1;

            // 2) Put array elements in different buckets
            for (int i = 0; i < n; i++)
            {
                dynamic value = (dynamic)selectItem(arr[i]);
                var difference = value - min;
                var bucketIndex = (int)difference / elementRange;
                buckets[bucketIndex].Add(arr[i]);
                //idx = (value * n >= n) ? value / n : value * n;
                //buckets[(int)idx].Add(arr[i]);
            }

            // 3) Sort individual buckets
            for (int i = 0; i < n; i++)
            {
                //sortFunc(list[i], list[i + 1])

                for (int j = 0; j < buckets[i].Count-1; j++)
                {
                    if (!sortFunc((dynamic)selectItem(buckets[i][j]), (dynamic)selectItem(buckets[i][j + 1])))
                    {
                        //zahlen tauschen (nur ein Paar)
                        var temp = buckets[i][j];
                        buckets[i][j] = buckets[i][j + 1];
                        buckets[i][j + 1] = temp;
                    }
                }
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

        public dynamic GetMax<T, G>(T[] arr, Func<T, G> selectItem)
        {
            dynamic max = 0;

            foreach (var item in arr)
            {
                var value = selectItem(item);
                if (max < value)
                {
                    max = value;
                }
            }

            return max;
        }

        public dynamic GetMin<T, G>(T[] arr, Func<T, G> selectItem)
        {
            dynamic min = 0;

            foreach (var item in arr)
            {
                var value = selectItem(item);
                if (min > value)
                {
                    min = value;
                }
            }

            return min;
        }
    }
}
