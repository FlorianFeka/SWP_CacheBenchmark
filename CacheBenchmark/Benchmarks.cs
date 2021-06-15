using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheBenchmark
{
    public class Benchmarks
    {
        private int[] intArray{get;set;}

        private BubbleSort bubbleSort = new();

        public Benchmarks()
        {
            this.intArray = new int[] { 2, 7, 1 };
        }

        [Benchmark]
        public void DoBenchmarkTests()
        {          
            bubbleSort.Sort(this.intArray);
        }
    }
}
