using goatgitter.lib.extensions;
using Moq;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using static goatgitter.lib.Constants;

namespace goatgitter.lib.tests.extensions
{
    /** 
    * ObjectsTest class Tests the Objects extension class.
    * MIT License
    * Copyright (c) 2022 goatgitter
    * */
    [TestFixture]
    public class StringsTest : TestBase
    {
        /// <inheritdoc/>
        [SetUp]
        public void SetupTest()
        {
            SetupMocks();
        }

        /// <inheritdoc/>
        [TearDown]
        public void CleanupTest()
        {
            ResetMocks();
        }

        /// <inheritdoc/>
        [Test]
        [TestCase(null, true)]
        [TestCase(TEST_NAME, false)]
        [TestCase(TEST_ALL_SPACES_STR, true)]
        [TestCase(TEST_LEADING_SPACES_STR, false)]
        [TestCase(TEST_TRAILING_SPACES_STR, false)]
        public void IsEmptyTest(string val, bool expectedResult)
        {
            Assert.AreEqual(expectedResult, val.IsEmpty());
        }

        /// <inheritdoc/>
        [Test]
        [TestCase(null, false)]
        [TestCase(TEST_NAME, true)]
        [TestCase(TEST_ALL_SPACES_STR, false)]
        [TestCase(TEST_LEADING_SPACES_STR, true)]
        [TestCase(TEST_TRAILING_SPACES_STR, true)]
        public void IsNotEmptyTest(string val, bool expectedResult)
        {
            Assert.AreEqual(expectedResult, val.IsNotEmpty());
        }

        /// <inheritdoc/>
        [Test]
        [TestCase(null, 0)]
        [TestCase(TEST_NAME, 2)]
        [TestCase(TEST_ALL_SPACES_STR, 0)]
        [TestCase(TEST_LEADING_SPACES_STR, 1)]
        [TestCase(TEST_TRAILING_SPACES_STR, 1)]
        [TestCase(TEST_HAS_PERIODS_STR, 2)]
        [TestCase(TEST_HAS_QUESTIONS_STR, 2)]
        public void WordCountTest(string val, int expectedResult)
        {
            int result = val.WordCount();
            Assert.AreEqual(expectedResult, result);
        }

        /// <inheritdoc/>
        [Test]
        [TestCase(null, false)]
        [TestCase(TEST_ALL_SPACES_STR, false)]
        [TestCase(TEST_LEADING_SPACES_STR, true)]
        [TestCase(TEST_TRAILING_SPACES_STR, true)]
        [TestCase(TEST_HAS_PERIODS_STR, true)]
        [TestCase(TEST_FILE_NAME, true)]
        [TestCase(TEST_FILE_INVALID, false)]        
        public void IsValidFileNameTest(string val, bool expectedResult)
        {
            
            bool result = val.IsValidFileName(MockAppLogger.Object);
            MockAppLogger.Verify(m => m.LogExceptionWithData(
                    It.Is<string>(s => s.Equals(ERR_VALID_FILE_NAME)),
                    It.Is<object[]>(o => o.Contains<object>(val)),
                    It.IsAny<Exception>()), Times.Exactly(0));
            Assert.AreEqual(expectedResult, result);
        }

        /// <inheritdoc/>
        [Test]
        [TestCase(TEST_INVALID_FILE_NAME_ERR_VAL)]
        public void IsValidFileNameTestForError(string val)
        {
            bool result = val.IsValidFileName(MockAppLogger.Object);
            MockAppLogger.Verify(m => m.LogExceptionWithData(
                    It.Is<string>(s => s.Equals(ERR_VALID_FILE_NAME)),
                    It.Is<object[]>(o => o.Contains<object>(val)),
                    It.IsAny<Exception>()), Times.Exactly(1));          
            Assert.IsFalse(result);
        }
    }
}
