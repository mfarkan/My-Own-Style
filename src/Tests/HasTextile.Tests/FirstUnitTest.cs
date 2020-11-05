using NUnit.Framework;

namespace HasTextile.Tests
{
    // we should keep focus on writing tests.Especially domain services and repos.
    [TestFixture]
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