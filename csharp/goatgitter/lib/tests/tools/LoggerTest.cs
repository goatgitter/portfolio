using NUnit.Framework;
using System;
using goatgitter.lib.tools;
using Moq;
using System.Text;
using goatgitter.lib.extensions;
using static goatgitter.lib.Constants;

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
        /// Creates initial state of the objects for test execution.
        /// </summary>
        [SetUp]
        public void SetupTest()
        {
            SetupMocks();
        }

        /// <summary>
        /// Resets objects modified during the test execution.
        /// </summary>
        [TearDown]
        public void CleanupTest()
        {
            ResetMocks();
            testObj = null;
        }

        /// <summary>
        /// Tests the Constructor
        /// </summary>
        [Test]
        public void ConstructorTest()
        {
            testObj = new Logger(MockLog.Object);
            Assert.IsNotNull(testObj);
            Assert.IsNotNull(testObj.Log);
            Assert.IsNotNull(testObj.LogType);
            Type currentType = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            Assert.AreEqual(currentType, testObj.LogType);
        }

        /// <summary>
        /// Tests the LogInfo Method
        /// </summary>
        [Test]
        public void LogInfoTest()
        {
            testObj = new Logger(MockLog.Object);
            Assert.IsNotNull(testObj);
            testObj.LogInfo(TEST_NAME);
            MockLog.Verify(m => m.Info(It.Is<string>(s => s.Equals(TEST_NAME)))
           , Times.Exactly(1));
        }

        /// <summary>
        /// Tests the LogError Method
        /// </summary>
        [Test]
        public void LogErrorTest()
        {
            testObj = new Logger(MockLog.Object);
            Assert.IsNotNull(testObj);
            testObj.LogError(TEST_NAME);
            MockLog.Verify(m => m.Error(It.Is<string>(s => s.Equals(TEST_NAME)))
           , Times.Exactly(1));
        }

        /// <summary>
        /// Tests the LogException Method
        /// </summary>
        [Test]
        public void LogExceptionTest()
        {
            testObj = new Logger(MockLog.Object);
            Exception testException = GetTestExceptionWithInner();            
            testObj.LogException(TEST_ERROR_MSG, testException);
            MockLog.Verify(m => m.Error(It.Is<string>(s => s.Equals(TEST_ERROR_MSG)))
           , Times.Exactly(1));
            MockLog.Verify(m => m.Error(It.Is<string>(s => s.Equals(testException.LogPrint())))
          , Times.Exactly(1));
        }

        /// <summary>
        /// Tests the LogException Method
        /// </summary>
        [Test]
        public void LogExceptionWithDataTest()
        {
            testObj = new Logger(MockLog.Object);
            Exception testException = GetTestExceptionWithInner();
            object[] messageData = new object[] {TEST_NAME, TEST_ID};
            testObj.LogExceptionWithData(TEST_ERROR_MSG_WITH_DATA, messageData, testException);
            MockLog.Verify(m => m.ErrorFormat(It.Is<string>(s => s.Equals(TEST_ERROR_MSG_WITH_DATA)),
                It.Is<object[]>(arr => arr.Equals(messageData)))
           , Times.Exactly(1));            
            MockLog.Verify(m => m.Error(
                It.Is<string>(s => s.Contains(ERR_OCCURRED)
                && s.Contains(ERR_EXCEPTION_MSG)
                && s.Contains(testException.LogPrint())))
          , Times.Exactly(1));
        }
    }
}
