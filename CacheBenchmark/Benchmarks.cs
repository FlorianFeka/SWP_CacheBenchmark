using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using System;

namespace CacheBenchmark
{
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    [CategoriesColumn]
    [SimpleJob(runtimeMoniker: RuntimeMoniker.Net50, baseline: true, launchCount: 1, warmupCount: 1, targetCount: 1, invocationCount: 1, runStrategy: RunStrategy.ColdStart)]
    //[SimpleJob(runtimeMoniker: RuntimeMoniker.NetCoreApp31, launchCount: 1, warmupCount: 1, targetCount: 1, invocationCount: 1, runStrategy: RunStrategy.ColdStart)]
    public class Benchmarks
    {
        // L1 Cache 384 KB; L2 Cache 3 MB; L3 Cache 32 MB
        //private int _amdRyzen53600L1CacheSize = 32 * 1024 * 1024;
        private int _amdRyzen53600L1CacheSize = 1000;
        private int[] _simpleIntArrayCpuCacheSized { get; set; }
        private int[] _simpleIntArrayBiggerThenCpuCache { get; set; }
        private GameStruct[] _complexStructArrayCpuCacheSized { get; set; }
        private GameStruct[] _complexStructArrayBiggerThenCpuCache { get; set; }
        private GameClass[] _orbjectArrayCpuCacheSized { get; set; }

        private readonly BubbleSort _bubbleSort = new();
        private readonly MergeSort _mergeSort = new();
        private readonly BucketSort _bucketSort = new();

        [IterationSetup]
        public void BenchmarksSetup()
        {
            //var arrSize = _amdRyzen53600L1CacheSize / sizeof(int);
            var arrSize = _amdRyzen53600L1CacheSize;
            Console.WriteLine($"ArrSize: {arrSize}");
            Random ran = new();
            _simpleIntArrayCpuCacheSized = new int[arrSize];
            for (int i = 0; i < arrSize; i++)
            {
                _simpleIntArrayCpuCacheSized[i] = ran.Next();
            }

            _simpleIntArrayBiggerThenCpuCache = new int[arrSize + 90];
            for (int i = 0; i < arrSize + 90; i++)
            {
                _simpleIntArrayBiggerThenCpuCache[i] = ran.Next();
            }

            _complexStructArrayCpuCacheSized = new GameStruct[arrSize];
            _orbjectArrayCpuCacheSized = new GameClass[arrSize];
            for (int i = 0; i < arrSize; i++)
            {
                _complexStructArrayCpuCacheSized[i] = new GameStruct
                {
                    Id = ran.Next(),
                    Price = (float)ran.NextDouble()
                };

                _orbjectArrayCpuCacheSized[i] = new GameClass
                {
                    Id = ran.Next(),
                    Price = (float)ran.NextDouble()
                };
            }

            _complexStructArrayBiggerThenCpuCache = new GameStruct[arrSize + 90];
            for (int i = 0; i < arrSize + 90; i++)
            {
                _complexStructArrayBiggerThenCpuCache[i] = new GameStruct
                {
                    Id = ran.Next(),
                    Price = (float)ran.NextDouble()
                };
            }
        }


        [BenchmarkCategory("Simple int array cache sized"), Benchmark(Baseline = true)]
        public void BubbleSort_SimpleIntArraySameSizeAsCpuCache()
        {
            _bubbleSort.Sort(_simpleIntArrayCpuCacheSized, ((a,b) => a>b));
        }

        [BenchmarkCategory("Simple int array cache sized"), Benchmark]
        public void MergeSort_SimpleIntArraySameSizeAsCpuCache()
        {
            _mergeSort.Sort(_simpleIntArrayCpuCacheSized, 2, 10, (a, b) => a < b, (c, d) => c < d);
        }

        public void BucketSort_SimpleIntArraySameSizeAsCpuCache()
        {
            //_bucketSort.Sort(_simpleIntArray, _simpleIntArray.Length, (a) => a);
        }



        [BenchmarkCategory("Simple int array cache sized + 90"), Benchmark(Baseline = true)]
        public void BubbleSort_SimpleIntArrayBiggerSizeAsCpuCache()
        {
            _bubbleSort.Sort(_simpleIntArrayBiggerThenCpuCache, ((a, b) => a > b));
        }

        [BenchmarkCategory("Simple int array cache sized + 90"), Benchmark]
        public void MergeSort_SimpleIntArrayBiggerSizeAsCpuCache()
        {
            _mergeSort.Sort(_simpleIntArrayBiggerThenCpuCache, 2, 10, (a, b) => a < b, (c, d) => c < d);
        }

        public void BucketSort_SimpleIntArrayBiggerSizeAsCpuCache()
        {
            //_bucketSort.Sort(_simpleIntArray, _simpleIntArray.Length, (a) => a);
        }



        [BenchmarkCategory("Eintragsgröße"), Benchmark(Baseline = true)]
        public void BubbleSort_SimpleIntArraySameSizeAsCpuCache2()
        {
            _bubbleSort.Sort(_simpleIntArrayCpuCacheSized, ((a, b) => a > b));
        }

        [BenchmarkCategory("Eintragsgröße"), Benchmark]
        public void BubbleSort_ComplexStructArraySameSizeAsCpuCache()
        {
            _bubbleSort.Sort(_complexStructArrayCpuCacheSized, ((a, b) => a.Id < b.Id));
        }
    }
}
