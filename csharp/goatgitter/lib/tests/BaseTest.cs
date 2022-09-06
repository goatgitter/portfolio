using NUnit.Framework;

namespace goatgitter.lib.tests
{
    [TestFixture]
    /// <inheritdoc/>
    public class BaseTest
    {
        /// <inheritdoc/>
        public class TestClass: Base
        {
            public int Id { get; set; }
            public string Name { get; set; }

        }

        public TestClass testClassObj;
        public const int TEST_ID = 999;
        public const string TEST_NAME = "Pippi Longstocking";

        /// <inheritdoc/>
        [SetUp]
        public void SetupTest()
        {
            testClassObj = new TestClass()
            {
                Id = TEST_ID,
                Name = TEST_NAME
            };
        }

        /// <inheritdoc/>
        [TearDown]
        public void CleanupTest()
        {
            testClassObj = null;
        }

        /// <inheritdoc/>
        [Test]
        public void EqualsTest()
        {
            TestClass other = new TestClass()
            {
                Id = TEST_ID,
                Name = TEST_NAME
            };
            Assert.AreEqual(testClassObj, other);
        }

        /// <inheritdoc/>
        [Test]
        public void ToStringTest()
        {
            string expected = "Prop: Id => " + TEST_ID + "\r\nProp: Name => " + TEST_NAME + "\r\n";
            Assert.AreEqual(expected, testClassObj.ToString());
        }

        /// <inheritdoc/>
        [Test]
        public void GetHashCodeTest()
        {
            TestClass other = new TestClass()
            {
                Id = TEST_ID,
                Name = TEST_NAME
            };
            int expected = other.GetHashCode();
            Assert.AreEqual(expected, testClassObj.GetHashCode());
        }
    }
}
