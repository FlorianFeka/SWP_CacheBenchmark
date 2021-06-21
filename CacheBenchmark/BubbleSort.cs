using System;

namespace CacheBenchmark
{
    class BubbleSort{
        public T[] Sort<T>(T[] list, Func<T, T, bool> sortFunc)
        {
            bool sorted;
	 
            //solange nicht alle Paare bei jedem  Durchlauf     
            //sortiert sind, Alg. wiederholen. 
            //->BubbleSort Verfahren

            do
            {
                sorted = true; 

                for (int i = 0; i < list.Length - 1; i++)
                {
                    if (sortFunc(list[i], list[i + 1]))
                    { 
			 
                        //zahlen tauschen (nur ein Paar)
                        var temp = list[i];
                        list[i] = list[i + 1];
                        list[i + 1] = temp;
			   
                        //nicht sortiert
                        sorted = false;
                    }
                }
 
            } while (!sorted);
	 
            //Zurückgeben der sortieren Liste
            return list;          
        }
    }
}