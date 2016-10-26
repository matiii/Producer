# Producer [![NuGet Version](https://img.shields.io/badge/nuget-1.0.0-blue.svg?style=flat)](https://www.nuget.org/packages/Producer)
If you would like improve 20x times code execution in your dependency injection container this library can be for you.

# How to

```c#
public class Foo
{
    private string _a;
    public int I { get;}

    public Foo(string a, int i, long l, double d, bool b, object obj)
    {
      _a = a;
      I = i;
    }
}

Func<object[], Foo> fooBuilder = Producer.Produce<Foo>(typeof(string), typeof(int), typeof(long), typeof(double),
                typeof(bool), typeof(object));
                
Foo foo = fooBuilder(new object[] { "", 5, (long) 6, 2.2, true, ""});
Assert.AreEqual(foo.I, 5); 
```
# Benchmark

```ini

Host Process Environment Information:
BenchmarkDotNet.Core=v0.9.9.0
OS=Microsoft Windows NT 6.2.9200.0
Processor=Intel(R) Core(TM) i7-6820HQ CPU 2.70GHz, ProcessorCount=8
Frequency=2648439 ticks, Resolution=377.5809 ns, Timer=TSC
CLR=MS.NET 4.0.30319.42000, Arch=32-bit RELEASE
GC=Concurrent Workstation
JitModules=clrjit-v4.6.1586.0

Type=Benchmark  Mode=Throughput  

```
                  Method |      Median |    StdDev |
------------------------ |------------ |---------- |
 ActivatorCreateInstance | 180.5806 ms | 5.9056 ms |
         ProducerProduce |   8.6206 ms | 0.6213 ms |


# How it works

Library generate IL code to create new object. Produce generic method instead of Activator.CreateInstance first build delegate, which next you can cache to create new instances. Activator.CreateInstance has to all the time build logic to create new object. 

# License

[![License](https://img.shields.io/badge/license-MIT-blue.svg?style=plastic)](https://github.com/matiii/Dijkstra.NET/blob/master/LICENSE)

Producer is licensed under the MIT license. See [LICENSE](LICENSE) file for full license information.
