namespace Producer.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model;

    [TestClass]
    public class ProducerTest
    {
        [TestMethod]
        public void Producer_Can_Create_Instance_With_Custom_Ctr()
        {
            Func<object[], Foo> fooBuilder = Producer.Produce<Foo>(typeof(string), typeof(int), typeof(long), typeof(double),
                typeof(bool), typeof(object));

            Foo foo = fooBuilder(new object[] { "", 5, (long) 6, 2.2, true, ""});
            Assert.AreEqual(foo.I, 5);
        }

        [TestMethod]
        public void Producer_Can_Create_Instance_With_Default_Ctr()
        {
            Func<object[], Foo> fooBuilder = Producer.Produce<Foo>();

            Foo foo = fooBuilder(new object[]{});
            Assert.AreEqual(foo.I, 0);
        }
    }
}
