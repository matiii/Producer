namespace Producer.Benchmark
{
    using BenchmarkDotNet.Attributes;
    using System;
    using Tests.Model;

    public class Benchmark
    {

        private const int Length = 100000;

        private readonly object[] _args = { "", 5, (long) 6, 2.2, true, ""};

        [Benchmark]
        public Foo[] ActivatorCreateInstance()
        {
            var foos = new Foo[Length];

            for (int i = 0; i < Length; i++)
                foos[i] = (Foo) Activator.CreateInstance(typeof(Foo), _args);

            return foos;
        }

        [Benchmark]
        public Foo[] ProducerProduce()
        {
            var foos = new Foo[Length];

            var f = Producer.Produce<Foo>(typeof(string), typeof(int), typeof(long), typeof(double),
                typeof(bool), typeof(object));

            for (int i = 0; i < Length; i++)
                foos[i] = f(_args);

            return foos;
        }
    }
}
