using goatgitter.lib.extensions;
using goatgitter.lib.tools;
using log4net;
using Moq;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Text;
using static goatgitter.lib.tests.TestConstants;

namespace goatgitter.lib.tests
{
    /** 
    * TestBase class from which other Test classes will inherit.
    * MIT License
    * Copyright (c) 2022 goatgitter
    * */
    [TestFixture]
    public class TestBase
    {

        /// <summary>
        /// Mock App Logger
        /// </summary>
        protected Mock<ILogger> MockAppLogger = null;
        /// <summary>
        /// Mock Logger
        /// </summary>
        protected Mock<ILogger> MockLogger = null;
        /// <summary>
        /// Mock Log
        /// </summary>
        protected Mock<ILog> MockLog = null;
        /// <summary>
        /// Mock App Log
        /// </summary>
        protected Mock<ILog> MockAppLog = null;
        /// <summary>
        /// Type to be used to the Test Loggers.
        /// </summary>
        protected Type TestLogType = null;

        /// <summary>
        /// A common method to be called to setup common mock objects for unit tests.
        /// </summary>
        protected void SetupMocks()
        {
            MockLog = new Mock<ILog>();
            MockLog.Setup(o => o.Info(It.IsAny<string>())).Verifiable();
            MockLog.Setup(o => o.Error(It.IsAny<string>())).Verifiable();
            MockLog.Setup(o => o.Warn(It.IsAny<string>())).Verifiable();
            MockLog.Setup(o => o.Error(It.IsAny<string>())).Verifiable();
            MockLog.Setup(o => o.ErrorFormat(It.IsAny<string>(), It.IsAny<object[]>())).Verifiable();

            MockLogger = new Mock<ILogger>();
            MockLogger.Setup(s => s.Log).Returns(MockLog.Object);
            if (TestLogType.IsEmpty())
            {
                Type logType = new StackFrame(1).GetMethod().DeclaringType;
                MockLogger.Setup(s => s.LogType).Returns(logType);
            }
            else
            {
                MockLogger.Setup(s => s.LogType).Returns(TestLogType);
            }
            MockLogger.Setup(o => o.LogInfo(It.IsAny<string>())).Verifiable();
            MockLogger.Setup(o => o.LogError(It.IsAny<string>())).Verifiable();
            MockLogger.Setup(o => o.LogWarn(It.IsAny<string>())).Verifiable();
            MockLogger.Setup(o => o.LogError(It.IsAny<string>())).Verifiable();
            MockLogger.Setup(o => o.LogException(It.IsAny<string>(), It.IsAny<Exception>())).Verifiable();
            MockLogger.Setup(o => o.LogExceptionWithData(It.IsAny<string>(), It.IsAny<object[]>(),
                It.IsAny<Exception>())).Verifiable();

            MockAppLog = new Mock<ILog>();
            MockAppLog.Setup(o => o.Info(It.IsAny<string>())).Verifiable();
            MockAppLog.Setup(o => o.Error(It.IsAny<string>())).Verifiable();
            MockAppLog.Setup(o => o.Warn(It.IsAny<string>())).Verifiable();
            MockAppLog.Setup(o => o.Error(It.IsAny<string>())).Verifiable();
            MockAppLog.Setup(o => o.ErrorFormat(It.IsAny<string>(), It.IsAny<object[]>())).Verifiable();

            MockAppLogger = new Mock<ILogger>();
            MockAppLogger.Setup(s => s.Log).Returns(MockAppLog.Object);
            if (TestLogType.IsEmpty())
            {
                Type logType = new StackFrame(1).GetMethod().DeclaringType;
                MockAppLogger.Setup(s => s.LogType).Returns(logType);
            }
            else
            {
                MockAppLogger.Setup(s => s.LogType).Returns(TestLogType);
            }
            MockAppLogger.Setup(o => o.LogInfo(It.IsAny<string>())).Verifiable();
            MockAppLogger.Setup(o => o.LogError(It.IsAny<string>())).Verifiable();
            MockAppLogger.Setup(o => o.LogWarn(It.IsAny<string>())).Verifiable();
            MockAppLogger.Setup(o => o.LogError(It.IsAny<string>())).Verifiable();
            MockAppLogger.Setup(o => o.LogException(It.IsAny<string>(), It.IsAny<Exception>())).Verifiable();
            MockAppLogger.Setup(o => o.LogExceptionWithData(It.IsAny<string>(), It.IsAny<object[]>(),
                It.IsAny<Exception>())).Verifiable();
        }

        /// <summary>
        /// A common method to be called to reset common mock objects for unit tests.
        /// </summary>
        protected void ResetMocks()
        {
            MockLog.Reset();
            MockLogger.Reset();
            MockAppLog.Reset();
            MockAppLogger.Reset();
        }

        /// <summary>
        /// Verifies that the Started Message was logged at the info level.
        /// </summary>
        /// <param name="logType">Type of the log.</param>
        /// <param name="numTimes">Integer representing the number of times the log should have been updated.</param>
        protected void VerifyStarted(Type logType, int numTimes)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Started logging for LogType -> ");
            sb.AppendIf(logType.Name);
            sb.AppendLine(String.Format(" At -> {0}", DateTime.Now.ToString()));
            MockLog.Verify(m => m.Info(It.Is<string>(s => s.Equals(sb.ToString())))
            , Times.Exactly(numTimes));
        }

        /// <summary>
        /// A method to create a Test Exception with the Inner Exception Message and Source.
        /// </summary>
        public void CreateTestInnerException()
        {
            throw new Exception(TEST_INNER_EXCEPTION_MSG)
            {
                Source = TEST_INNER_EXCEPTION_SRC,
            };
        }

        /// <summary>
        /// A method to create a Test Exception with an Inner Exception.
        /// </summary>
        public void CreateTestExceptionWithInner()
        {
            try
            {
                this.CreateTestInnerException();
            }
            catch (Exception inner)
            {
                throw new Exception(TEST_EXCEPTION_MSG, inner)
                {
                    Source = TEST_EXCEPTION_SRC
                };
            }
        }

        /// <summary>
        /// A method to generate a Test Exception with an Inner Exception.
        /// </summary>
        /// <returns>An Exception</returns>
        public Exception GetTestExceptionWithInner()
        {
            Exception outer = null;
            try
            {
                this.CreateTestExceptionWithInner();
            }
            catch (Exception exception)
            {
                outer = exception;
            }
            return outer;
        }

        /// <summary>
        /// A method to create a Test Exception without an Inner Exception.
        /// </summary>
        public void CreateTestException()
        {
            throw new Exception(TEST_EXCEPTION_MSG)
            {
                Source = TEST_EXCEPTION_SRC
            };
        }

        /// <summary>
        /// A method to generate a Test Exception without an Inner Exception.
        /// </summary>
        /// <returns>An Exception</returns>
        public Exception GetTestException()
        {
            Exception outer = null;
            try
            {
                this.CreateTestException();
            }
            catch (Exception exception)
            {
                outer = exception;
            }
            return outer;
        }
    }
}
