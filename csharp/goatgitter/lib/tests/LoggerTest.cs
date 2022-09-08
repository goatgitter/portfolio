using NUnit.Framework;
using System;

namespace goatgitter.lib.tests
{
    /** 
    * ObjectsTest class Tests the Objects extension class.
    * MIT License
    * Copyright (c) 2022 goatgitter
    * */
    [TestFixture]
    public class LoggerTest : TestBase
    {
        private Logger testObj;

        /// <inheritdoc/>
        [TearDown]
        public void CleanupTest()
        {
            testObj = null;
        }

        /// <inheritdoc/>
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
