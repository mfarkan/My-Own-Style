using NUnit.Framework;

namespace HasTextile.Tests
{
    public class FirstUnitTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.AreEqual(1, 1, "one equals to one <3");
        }
    }
}