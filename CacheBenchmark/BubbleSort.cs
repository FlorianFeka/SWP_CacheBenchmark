using System;

namespace CacheBenchmark
{
    class BubbleSort{
        public T[] Sort<T>(T[] list, Func<T, T, bool> sortFunc)
        {
            bool sorted;
            do
            {
                sorted = true; 

                for (int i = 0; i < list.Length - 1; i++)
                {
                    if (!sortFunc(list[i], list[i + 1]))
                    { 
                        var temp = list[i];
                        list[i] = list[i + 1];
                        list[i + 1] = temp;
			   
                        sorted = false;
                    }
                }
 
            } while (!sorted);
	 
            return list;          
        }
    }
}