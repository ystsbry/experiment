using BenchmarkDotNet.Running;
using BenchmarkDotNet.Attributes;

namespace ComparisonOfClosureAndClassSpeeds;

internal class Program
{
    private static void Main() => BenchmarkRunner.Run<BenchmarkTest>();
}

[MemoryDiagnoser]
public class BenchmarkTest
{

    [Benchmark]
    public int ClassBenchmark()
    {
        var TargetClass = new ComparisionTargetClass();

        for (var i = 0; i < 10; i++)
        {
            TargetClass.Increment();
        }
        return TargetClass.Count;
    }

    [Benchmark]
    public int ClosureBenchmark()
    {
        var (IncrementClosure, GetCountClosure) = ComparisionTargetClosure.Get();

        for (var i = 0; i < 10; i++)
        {
            IncrementClosure();
        }
        return GetCountClosure();
    }
}

public class ComparisionTargetClass
{
    public int Count = 0;

    public void Increment()
    {
        Count++;
    }
}

public class ComparisionTargetClosure
{
    static public (Action, Func<int>) Get()
    {
        int count = 0;
        return (
        () => 
        {
            count++;
        },
        () =>
        {
            return count;
        });
    }
}
