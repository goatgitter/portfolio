using goatgitter.lib.extensions;
using NUnit.Framework;
using System;
using static goatgitter.lib.tests.TestConstants;

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
    }
}
