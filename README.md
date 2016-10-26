# Producer
If you would like improve 80x times code execution in your dependency injection container this library can be for you.

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
# How it works

Library generate IL code to create new object. Produce generic method instead of Activator.CreateInstance first build delegate, which next you can cache to create new instances. Activator.CreateInstance has to all the time build logic to create new object. 
