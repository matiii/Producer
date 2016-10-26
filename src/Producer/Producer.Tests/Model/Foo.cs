namespace Producer.Tests.Model
{
    public class Foo
    {
        private string _a;
        public int I { get; }

        public Foo(string a, int i, long l, double d, bool b, object obj)
        {
            _a = a;
            I = i;
        }

        public Foo()
        {
            
        }
    }
}
