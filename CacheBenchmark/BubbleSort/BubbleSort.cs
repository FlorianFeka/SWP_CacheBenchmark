namespace CacheBenchmark.BubbleSort
{
    class BubbleSort{
        public int[] DoBubbleSort(int[] list)
        {
            bool sorted;
	 
            //solange nicht alle paare bei jedem  Durchlauf     
            //sortiert sind, Alg. wiederholen. 
            //->BubbleSort verfahren

            do
            {
                sorted = true; 

                for (int i = 0; i < list.Length - 1; i++)
                {
                    if (list[i] > list[i + 1])
                    { 
			 
                        //zahlen tauschen (nur ein Paar)
                        int temp = list[i];
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