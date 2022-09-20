using NUnit.Framework;

namespace goatgitter.lib.tests
{
    /** 
    * BaseTest class Tests the Base class.
    * MIT License
    * Copyright (c) 2022 goatgitter
    * */
    [TestFixture]
    public class BaseTest : TestBase
    {
        /// <inheritdoc/>
        public class TestClass: Base
        {
            /// <summary>
            /// An int representing a unique record.
            /// </summary>
            public int Id { get; set; }
            /// <summary>
            /// A string containing a name for this class.
            /// </summary>
            public string Name { get; set; }
        }

        /// <summary>
        /// An instance of the Test Class to be tested.
        /// </summary>
        public TestClass testClassObj;

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

        /// <summary>
        /// Resets objects modified during the test execution.
        /// </summary>
        [TearDown]
        public void CleanupTest()
        {
            testClassObj = null;
        }

        /// <summary>
        /// Tests the Constructor
        /// </summary>
        [Test]
        public void ConstructorTest()
        {
            Assert.IsNotNull(testClassObj);
            Assert.AreEqual(TEST_ID, testClassObj.Id);
            Assert.AreEqual(TEST_NAME, testClassObj.Name);
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

        /// <inheritdoc/>
        [Test]
        public void CloneTest()
        {
            TestClass clone = testClassObj.Clone<TestClass>();
            Assert.AreEqual(testClassObj, clone);
            Assert.AreEqual(testClassObj.GetHashCode(), clone.GetHashCode());
        }
    }
}
