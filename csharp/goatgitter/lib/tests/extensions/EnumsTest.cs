using goatgitter.lib.extensions;
using NUnit.Framework;
using System;
using static goatgitter.lib.tests.TestConstants;
using static goatgitter.lib.Constants;
using Moq;
using System.Linq;

namespace goatgitter.lib.tests.extensions
{
    /** 
    * EnumsTest class Tests the Enums extension class.
    * MIT License
    * Copyright (c) 2022 goatgitter
    * */
    [TestFixture]
    public class EnumsTest : TestBase
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

        
        /// <summary>
        /// Tests the IsEmpty Method
        /// </summary>
        /// <param name="val"></param>
        /// <param name="expectedResult"></param>
        [Test]
        [TestCase(TEST_ENUM_RATING.None, true)]
        [TestCase(TEST_ENUM_RATING.Good, false)]
        [TestCase(new TEST_ENUM_RATING(), true)]        
        public void IsEmptyTest(Enum val, bool expectedResult)
        {
            Assert.AreEqual(expectedResult, val.IsEmpty());
        }

        /// <summary>
        /// Tests the IsNotEmpty Method
        /// </summary>
        /// <param name="val"></param>
        /// <param name="expectedResult"></param>
        [Test]
        [TestCase(TEST_ENUM_RATING.None, false)]
        [TestCase(TEST_ENUM_RATING.Good, true)]
        [TestCase(new TEST_ENUM_RATING(), false)]
        public void IsNotEmptyTest(Enum val, bool expectedResult)
        {
            Assert.AreEqual(expectedResult, val.IsNotEmpty());
        }

        /// <summary>
        /// Tests the GetDesc Method
        /// </summary>
        /// <param name="val"></param>
        /// <param name="expectedDesc"></param>
        /// <param name="errorsExpected"></param>
        [Test]
        [TestCase(TEST_ENUM_GRADE.None, NONE)]
        [TestCase(TEST_ENUM_GRADE.A, BEST)]
        [TestCase(new TEST_ENUM_GRADE(), NONE)]
        [TestCase(TEST_ENUM_GRADE.B, "B")]
        public void GetDescTest(Enum val, string expectedDesc, int errorsExpected = 0)
        {
            string result = val.GetDesc(MockAppLogger.Object);
            MockAppLogger.Verify(m => m.LogExceptionWithData(
                    It.Is<string>(s => s.Equals(ERR_ENUM_DESC)),
                    It.Is<object[]>(o => o.Contains<object>(val)),
                    It.IsAny<Exception>()), Times.Exactly(errorsExpected));
            Assert.AreEqual(expectedDesc, result);
        }

        /// <summary>
        /// Tests the GetValByDesc Method
        /// </summary>
        /// <param name="expectedVal"></param>
        /// <param name="desc"></param>
        /// <param name="errorsExpected"></param>
        [Test]
        [TestCase(TEST_ENUM_GRADE.None, NONE)]
        [TestCase(TEST_ENUM_GRADE.A, BEST)]
        [TestCase(new TEST_ENUM_GRADE(), NONE)]
        [TestCase(TEST_ENUM_GRADE.B, "B")]
        [TestCase(TEST_ENUM_GRADE.None, INVALID)]
        public void GetValByDescTest<T>(T expectedVal, string desc, int errorsExpected = 0) where T : Enum
        {
            T result = Enums.GetValByDesc<T>(desc, MockAppLogger.Object);
            MockAppLogger.Verify(m => m.LogExceptionWithData(
                    It.Is<string>(s => s.Equals(ERR_ENUM_VAL_BY_DESC)),
                    It.Is<object[]>(o => o.Contains<object>(desc)),
                    It.IsAny<Exception>()), Times.Exactly(errorsExpected));
            Assert.AreEqual(expectedVal, result);
        }
    }
}
