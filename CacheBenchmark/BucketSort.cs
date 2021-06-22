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

            if (n <= 0)
            {
                return;
            }
            
            // 1) Create n empty buckets
            List<T>[] buckets = new List<T>[n];

            for (int i = 0; i < n; i++)
            {
                buckets[i] = new List<T>();
            }

            float idx = 0;
            // 2) Put array elements in different buckets
            for (int i = 0; i < n; i++)
            {
                dynamic value = (dynamic)selectItem(arr[i]);
                idx = (value * n >= n) ? value / n : value * n;
                buckets[(int)idx].Add(arr[i]);
            }

            // 3) Sort individual buckets
            for (int i = 0; i < n; i++)
            {
                //sortFunc(list[i], list[i + 1])

                for (int j = 0; j < buckets[i].Count-1; j++)
                {
                    var a = selectItem(buckets[i][j]);
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
    }
}
