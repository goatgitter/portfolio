using goatgitter.lib.extensions;
using NUnit.Framework;

namespace goatgitter.lib.tests
{
    /** 
    * ObjectsTest class Tests the Objects extension class.
    * MIT License
    * Copyright (c) 2022 goatgitter
    * */
    [TestFixture]
    public class StringsTest : TestBase
    {
        private string testStr;

        /// <inheritdoc/>
        [SetUp]
        public void SetupTest()
        {
            testStr = TEST_NAME.Clone().ToString();
        }

        /// <inheritdoc/>
        [TearDown]
        public void CleanupTest()
        {
            testStr = null;
        }

        /// <inheritdoc/>
        [Test]
        public void IsEmptyTest()
        {
            Assert.IsFalse(testStr.IsEmpty());
            testStr = null;
            Assert.IsTrue(testStr.IsEmpty());
        }

        /// <inheritdoc/>
        [Test]
        public void IsNotEmptyTest()
        {
            Assert.IsTrue(testStr.IsNotEmpty());
            testStr = null;
            Assert.IsFalse(testStr.IsNotEmpty());
        }

        /// <inheritdoc/>
        [Test]
        public void WordCountTest()
        {
            // String is two words
            int result = testStr.WordCount();
            Assert.AreEqual(2, result);

            // String is null.
            testStr = null;
            result = testStr.WordCount();
            Assert.AreEqual(0, result);

            // String is all spaces.    
            testStr = TEST_ALL_SPACES_STR;
            result = testStr.WordCount();
            Assert.AreEqual(0, result);

            // String has leading spaces.          
            testStr = TEST_LEADING_SPACES_STR;
            result = testStr.WordCount();
            Assert.AreEqual(1, result);

            // String has trailing spaces.          
            testStr = TEST_TRAILING_SPACES_STR;
            result = testStr.WordCount();
            Assert.AreEqual(1, result);

            // String has periods.          
            testStr = TEST_HAS_PERIODS_STR;
            result = testStr.WordCount();
            Assert.AreEqual(2, result);

            // String has questions.          
            testStr = TEST_HAS_QUESTIONS_STR;
            result = testStr.WordCount();
            Assert.AreEqual(2, result);
        }
        

    }
}
