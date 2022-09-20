using goatgitter.lib.extensions;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using static goatgitter.lib.Constants;
using static goatgitter.lib.tests.TestConstants;

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

        /// <summary>
        /// Tests the IsValidFileName method
        /// </summary>
        /// <param name="val">A string containing the value to be validated.</param>
        /// <param name="expectedResult">A bool representing the expected result from the method.</param>
        /// <param name="errorsExpected">An integer representing the number of expected errors to result from calling the method.</param>
        [Test]
        [TestCase(null, false)]
        [TestCase(TEST_ALL_SPACES_STR, false)]
        [TestCase(TEST_LEADING_SPACES_STR, true)]
        [TestCase(TEST_TRAILING_SPACES_STR, true)]
        [TestCase(TEST_HAS_PERIODS_STR, true)]
        [TestCase(TEST_FILE_NAME, true)]
        [TestCase(TEST_FILE_INVALID, false)]
        [TestCase(TEST_INVALID_FILE_NAME_ERR_VAL, false, 1)]
        public void IsValidFileNameTest(string val, bool expectedResult = false, int errorsExpected = 0)
        {
            
            bool result = val.IsValidFileName(MockAppLogger.Object);
            MockAppLogger.Verify(m => m.LogExceptionWithData(
                    It.Is<string>(s => s.Equals(ERR_VALID_FILE_NAME)),
                    It.Is<object[]>(o => o.Contains<object>(val)),
                    It.IsAny<Exception>()), Times.Exactly(errorsExpected));
            Assert.AreEqual(expectedResult, result);
        }

        /// <summary>
        /// Tests the IsValidDirName method
        /// </summary>
        /// <param name="val">A string containing the value to be validated.</param>
        /// <param name="expectedResult">A bool representing the expected result from the method.</param>
        /// <param name="errorsExpected">An integer representing the number of expected errors to result from calling the method.</param>
        [Test]
        [TestCase(null, false)]
        [TestCase(TEST_ALL_SPACES_STR, false)]
        [TestCase(TEST_LEADING_SPACES_STR, true)]
        [TestCase(TEST_TRAILING_SPACES_STR, true)]
        [TestCase(TEST_HAS_PERIODS_STR, true)]
        [TestCase(TEST_DIR_NAME, true)]
        [TestCase(TEST_DIR_INVALID, false)]
        public void IsValidDirNameTest(string val, bool expectedResult = false, int errorsExpected = 0)
        {
            bool result = val.IsValidDirName(MockAppLogger.Object);
            MockAppLogger.Verify(m => m.LogExceptionWithData(
                    It.Is<string>(s => s.Equals(ERR_VALID_DIR_NAME)),
                    It.Is<object[]>(o => o.Contains<object>(val)),
                    It.IsAny<Exception>()), Times.Exactly(errorsExpected));
            Assert.AreEqual(expectedResult, result);
        }
    }
}
