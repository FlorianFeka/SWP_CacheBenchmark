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
            PrintArray(bubbleArray);

            // Merge Sort
            int[] mergeArray = { 12, 11, 13, 5, 6, 7 };

            MergeSort ob = new MergeSort();
            ob.Sort(mergeArray, 0, mergeArray.Length - 1);
            Console.WriteLine("\nMerge Sort:");
            PrintArray(mergeArray);
  
            // Bucket Sort
            var bucketSort = new BucketSort();
            float[] bucketArray = { (float)0.897, (float)1, (float)0.565,
                   (float)0.656, (float)0.1234,
                   (float)0.665, (float)0.3434 };

            int n = bucketArray.Length;
            bucketSort.Sort(bucketArray, n);

            Console.WriteLine("\nBucket Sort: ");
            PrintArray(bucketArray);
        }

        public static void PrintArray(int[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n; ++i)
                Console.Write(arr[i] + " ");
            Console.WriteLine();
        }

        public static void PrintArray(float[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n; ++i)
                Console.Write(arr[i] + " ");
            Console.WriteLine();
        }
    }
}
