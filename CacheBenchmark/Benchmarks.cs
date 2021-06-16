using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using System;

namespace CacheBenchmark
{
    [SimpleJob(runtimeMoniker: RuntimeMoniker.Net50, baseline: true, launchCount: 1, warmupCount: 1, targetCount: 1, invocationCount: 1, runStrategy: RunStrategy.ColdStart)]
    //[SimpleJob(runtimeMoniker: RuntimeMoniker.NetCoreApp31, launchCount: 1, warmupCount: 1, targetCount: 1, invocationCount: 1, runStrategy: RunStrategy.ColdStart)]
    //[SimpleJob(runtimeMoniker: RuntimeMoniker.Net48, launchCount: 1, warmupCount: 1, targetCount: 1, invocationCount: 1, runStrategy: RunStrategy.ColdStart)]
    public class Benchmarks
    {
        private int _amdRyzen53600L1CacheSize = 384 * 1024;
        private int[] _simpleIntArray { get; set; }
        //[Params(100, 200, 300)]
        //public int _simpleIntArraySize;

        private readonly BubbleSort _bubbleSort = new();

        [GlobalSetup]
        public void BenchmarksSetup()
        {
            var arrSize = _amdRyzen53600L1CacheSize / sizeof(int);
            Console.WriteLine($"ArrSize: {arrSize}");
            Random ran = new();
            _simpleIntArray = new int[arrSize];
            for (int i = 0; i < arrSize; i++)
            {
                _simpleIntArray[i] = ran.Next();
            }
        }

        [Benchmark(Baseline = true)]
        public void BubbleSort_SimpleIntArraySameSizeAsCpuCache()
        {
            _bubbleSort.Sort(_simpleIntArray);
        }
    }
}
