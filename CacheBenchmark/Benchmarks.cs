using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;

namespace CacheBenchmark
{
    [SimpleJob(runtimeMoniker: RuntimeMoniker.Net50, baseline: true, launchCount: 1, warmupCount: 1, targetCount: 1, invocationCount: 1, runStrategy: RunStrategy.ColdStart)]
    //[SimpleJob(runtimeMoniker: RuntimeMoniker.NetCoreApp31, launchCount: 1, warmupCount: 1, targetCount: 1, invocationCount: 1, runStrategy: RunStrategy.ColdStart)]
    //[SimpleJob(runtimeMoniker: RuntimeMoniker.Net48, launchCount: 1, warmupCount: 1, targetCount: 1, invocationCount: 1, runStrategy: RunStrategy.ColdStart)]
    public class Benchmarks
    {
        private int[] intArray{get;set;}

        private BubbleSort bubbleSort = new();

        public Benchmarks()
        {
            this.intArray = new int[] { 2, 7, 1 };
        }

        [Benchmark(Baseline = true)]
        public void BubbleSort()
        {          
            bubbleSort.Sort(this.intArray);
        }
    }
}
