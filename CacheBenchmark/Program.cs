using System;

namespace CacheBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] bubbleArray =  {100, 9, 34, 434, 34, 382, 50, 2, 7};
            var bubbleSort = new BubbleSort.BubbleSort();
            var list = bubbleSort.DoBubbleSort(bubbleArray);
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }
    }
}
