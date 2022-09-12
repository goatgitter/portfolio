using NUnit.Framework;

namespace goatgitter.lib.tests
{
    /** 
    * BaseWithLogTest class Tests the BaseWithLog class.
    * MIT License
    * Copyright (c) 2022 goatgitter
    * */
    [TestFixture]
    public class BaseWithLogTest : TestBase
    {
        /// <inheritdoc/>
        public class TestClass : BaseWithLog
        {
            public int Id { get; set; }
            public string Name { get; set; }

        }

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
            Assert.IsNotNull(testClassObj.Notepad);
            Assert.IsNotNull(testClassObj.Notepad.Log);
            Assert.IsNotNull(testClassObj.Notepad.LogType);
            Assert.AreEqual(testClassObj.GetType(), testClassObj.Notepad.LogType);
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
            string expected = "Prop: Id => " + TEST_ID 
                + "\r\nProp: Name => " + TEST_NAME 
                + "\r\nProp: Notepad => Prop: Log => log4net.Core.LogImpl\r\nProp: LogType => goatgitter.lib.tests.BaseWithLogTest+TestClass\r\n\r\n";
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
