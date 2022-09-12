using NUnit.Framework;
using System;
using goatgitter.lib.tools;

namespace goatgitter.lib.tests.tools
{
    /** 
    * MIT License
    * Copyright (c) 2022 goatgitter
    * ClassName : LoggerTest
    * Purpose : Tests the Logger Class.
    **/
    [TestFixture]
    public class LoggerTest : TestBase
    {
        private Logger testObj;

        /// <summary>
        /// Resets objects modified during the test execution.
        /// </summary>
        [TearDown]
        public void CleanupTest()
        {
            testObj = null;
        }

        /// <summary>
        /// Tests the Constructor
        /// </summary>
        [Test]
        public void ConstructorTest()
        {
            testObj = new Logger();
            Assert.IsNotNull(testObj);
            Assert.IsNotNull(testObj.Log);
            Assert.IsNotNull(testObj.LogType);
            Type currentType = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            Assert.AreEqual(currentType, testObj.LogType);
        }

        /// <inheritdoc/>
        [Test]
        public void LogInfoTest()
        {
            testObj = new Logger();
            Assert.IsNotNull(testObj);
            testObj.LogInfo(TEST_NAME);
            testObj.Log.Error(TEST_NAME);
        }
    }
}
