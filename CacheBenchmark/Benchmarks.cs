using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using System;

namespace CacheBenchmark
{
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    [CategoriesColumn]
    [SimpleJob(runtimeMoniker: RuntimeMoniker.Net50)]
    [SimpleJob(runtimeMoniker: RuntimeMoniker.NetCoreApp31)]
    public class Benchmarks
    {
        // L1 Cache 384 KB; L2 Cache 3 MB; L3 Cache 32 MB
        private int _amdRyzen53600L3CacheSize = 32 * 1024 * 1024;
        //private int _amdRyzen53600L3CacheSize = 9000;
        private int[] _simpleIntArrayCpuCacheSized { get; set; }
        private int[] _simpleIntArrayBiggerThenCpuCache { get; set; }
        private GameStruct[] _complexStructArrayCpuCacheSized { get; set; }
        private GameStruct[] _complexStructArrayBiggerThenCpuCache { get; set; }
        private GameClass[] _objectArrayCpuCacheSized { get; set; }

        private readonly BubbleSort _bubbleSort = new();
        private readonly MergeSort _mergeSort = new();
        private readonly BucketSort _bucketSort = new();

        [IterationSetup]
        public void BenchmarksSetup()
        {
            var arrSize = _amdRyzen53600L3CacheSize / sizeof(int);
            //var arrSize = _amdRyzen53600L3CacheSize;
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
            _objectArrayCpuCacheSized = new GameClass[arrSize];
            for (int i = 0; i < arrSize; i++)
            {
                _complexStructArrayCpuCacheSized[i] = new GameStruct
                {
                    Id = ran.Next(),
                    Price = (float)ran.NextDouble()
                };

                _objectArrayCpuCacheSized[i] = new GameClass
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


        [BenchmarkCategory("Gesamtes Datenarray passt in den CPU-Cache"), Benchmark(Baseline = true)]
        public void BubbleSort_SimpleIntArraySameSizeAsCpuCache()
        {
            _bubbleSort.Sort(_simpleIntArrayCpuCacheSized, (a,b) => a <= b);
        }

        [BenchmarkCategory("Gesamtes Datenarray passt in den CPU-Cache"), Benchmark]
        public void MergeSort_SimpleIntArraySameSizeAsCpuCache()
        {
            _mergeSort.Sort(_simpleIntArrayCpuCacheSized, 2, 10, (a, b) => a < b, (a, b) => a <= b);
        }

        [BenchmarkCategory("Gesamtes Datenarray passt in den CPU-Cache"), Benchmark]
        public void BucketSort_SimpleIntArraySameSizeAsCpuCache()
        {
            _bucketSort.Sort(_simpleIntArrayCpuCacheSized, _simpleIntArrayCpuCacheSized.Length, (a) => a, (a, b) => a <= b);
        }



        [BenchmarkCategory("Array ist um 90 Items zu groß für den Cache"), Benchmark(Baseline = true)]
        public void BubbleSort_SimpleIntArrayBiggerSizeAsCpuCache()
        {
            _bubbleSort.Sort(_simpleIntArrayBiggerThenCpuCache, (a, b) => a <= b);
        }

        [BenchmarkCategory("Array ist um 90 Items zu groß für den Cache"), Benchmark]
        public void MergeSort_SimpleIntArrayBiggerSizeAsCpuCache()
        {
            _mergeSort.Sort(_simpleIntArrayBiggerThenCpuCache, 2, 10, (a, b) => a < b, (a, b) => a <= b);
        }

        [BenchmarkCategory("Array ist um 90 Items zu groß für den Cache"), Benchmark]
        public void BucketSort_SimpleIntArrayBiggerSizeAsCpuCache()
        {
            _bucketSort.Sort(_simpleIntArrayBiggerThenCpuCache, _simpleIntArrayBiggerThenCpuCache.Length, (a) => a, (a, b) => a <= b);
        }



        [BenchmarkCategory("Array Eintrag ist ein simpler Integer"), Benchmark(Baseline = true)]
        public void BubbleSort_SimpleIntArraySameSizeAsCpuCache2()
        {
            _bubbleSort.Sort(_simpleIntArrayCpuCacheSized, (a, b) => a <= b);
        }

        [BenchmarkCategory("Array Eintrag ist ein simpler Integer"), Benchmark]
        public void MergeSort_SimpleIntArraySameSizeAsCpuCache2()
        {
            _mergeSort.Sort(_simpleIntArrayCpuCacheSized, 2, 10, (a, b) => a < b, (a, b) => a <= b);
        }

        [BenchmarkCategory("Array Eintrag ist ein simpler Integer"), Benchmark]
        public void BucketSort_SimpleIntArrayBiggerSizeAsCpuCache2()
        {
            _bucketSort.Sort(_simpleIntArrayCpuCacheSized, _simpleIntArrayCpuCacheSized.Length, (a) => a, (a, b) => a <= b);
        }




        [BenchmarkCategory("Array Eintrag ist ein komplexes Struct"), Benchmark(Baseline = true)]
        public void BubbleSort_ComplexStructArraySameSizeAsCpuCache2()
        {
            _bubbleSort.Sort(_complexStructArrayCpuCacheSized, (a, b) => a.Id <= b.Id);
        }

        [BenchmarkCategory("Array Eintrag ist ein komplexes Struct"), Benchmark]
        public void MergeSort_ComplexStructArraySameSizeAsCpuCache()
        {
            _mergeSort.Sort(_complexStructArrayCpuCacheSized, 2, 10, (a, b) => a < b, (a, b) => a.Id <= b.Id);
        }

        [BenchmarkCategory("Array Eintrag ist ein komplexes Struct"), Benchmark]
        public void BucketSort_ComplexStructArrayBiggerSizeAsCpuCache()
        {
            _bucketSort.Sort(_complexStructArrayCpuCacheSized, _complexStructArrayCpuCacheSized.Length, (a) => a.Id, (a, b) => a <= b);
        }



        [BenchmarkCategory("Array enthält direkt die konkreten Inhalte"), Benchmark(Baseline = true)]
        public void BubbleSort_SimpleIntArraySameSizeAsCpuCache3()
        {
            _bubbleSort.Sort(_simpleIntArrayCpuCacheSized, (a, b) => a <= b);
        }

        [BenchmarkCategory("Array enthält direkt die konkreten Inhalte"), Benchmark]
        public void MergeSort_SimpleIntArraySameSizeAsCpuCache3()
        {
            _mergeSort.Sort(_simpleIntArrayCpuCacheSized, 2, 10, (a, b) => a < b, (a, b) => a <= b);
        }

        [BenchmarkCategory("Array enthält direkt die konkreten Inhalte"), Benchmark]
        public void BucketSort_SimpleIntArraySameSizeAsCpuCache3()
        {
            _bucketSort.Sort(_simpleIntArrayCpuCacheSized, _simpleIntArrayCpuCacheSized.Length, (a) => a, (a, b) => a <= b);
        }




        [BenchmarkCategory("Jeder einzelne Inhalt wird separat erzeugt (allokiert)"), Benchmark(Baseline = true)]
        public void BubbleSort_ObjectArraySameSizeAsCpuCache()
        {
            _bubbleSort.Sort(_objectArrayCpuCacheSized, (a, b) => a.Id <= b.Id);
        }

        [BenchmarkCategory("Jeder einzelne Inhalt wird separat erzeugt (allokiert)"), Benchmark]
        public void MergeSort_ObjectArraySameSizeAsCpuCache()
        {
            _mergeSort.Sort(_objectArrayCpuCacheSized, 2, 10, (a, b) => a < b, (a, b) => a.Id <= b.Id);
        }

        [BenchmarkCategory("Jeder einzelne Inhalt wird separat erzeugt (allokiert)"), Benchmark]
        public void BucketSort_ObjectArrayBiggerSizeAsCpuCache()
        {
            _bucketSort.Sort(_objectArrayCpuCacheSized, _objectArrayCpuCacheSized.Length, (a) => a.Id, (a, b) => a <= b);
        }
    }
}
