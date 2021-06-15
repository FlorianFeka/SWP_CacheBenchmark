using System;

namespace CacheBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            // Bubble Sort
            int[] bubbleArray =  {100, 9, 34, 434, 34, 382, 50, 2, 7};
            var bubbleSort = new BubbleSort.BubbleSort();
            var list = bubbleSort.DoBubbleSort(bubbleArray);

            Console.WriteLine("Bubble Sort: ");
            foreach (var item in list)
            {
                Console.Write(item + " ");
            }

            // Bucket Sort
            var bucketSort = new BucketSort();
            float[] arr = { (float)0.897, (float)0.565,
                   (float)0.656, (float)0.1234,
                   (float)0.665, (float)0.3434 };

            int n = arr.Length;
            bucketSort.Sort(arr, n);

            Console.WriteLine("\n\nBucket Sort: ");
            foreach (var item in arr)
            {
                Console.Write(item + " ");
            }
        }
    }
}
