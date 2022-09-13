using goatgitter.lib.extensions;
using goatgitter.lib.tools;
using log4net;
using Moq;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Text;

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
        public const int TEST_ID = 999;
        public const string TEST_NAME = "Pippi Longstocking";
        public const string TEST_ALL_SPACES_STR = "         ";
        public const string TEST_LEADING_SPACES_STR = "         Longstocking";
        public const string TEST_TRAILING_SPACES_STR = "Pippi         ";
        public const string TEST_HAS_PERIODS_STR = "Pippi.Longstocking";
        public const string TEST_HAS_QUESTIONS_STR = "Pippi?Longstocking";
        public const string TEST_EXCEPTION_MSG = "TestExceptionMessage";
        public const string TEST_EXCEPTION_SRC = "TestExceptionSource";
        public const string TEST_INNER_EXCEPTION_MSG = "TestInnerExceptionMessage";
        public const string TEST_INNER_EXCEPTION_SRC = "TestInnerExceptionSource";
        public const string TEST_FILE_NAME_DNE = "dne.txt";
        public const string TEST_FILE_NAME = "testFile.log";
        public const string TEST_DIR_NAME = "testData";
        public const string TEST_DIR_INVALID = "*+";

        // Base Logging
        protected Mock<ILogger> MockAppLogger = null;
        protected Mock<ILogger> MockLogger = null;
        protected Mock<ILog> MockLog = null;
        protected Type TestLogType = null;

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

            MockAppLogger = new Mock<ILogger>();
            MockAppLogger.Setup(s => s.Log).Returns(MockLog.Object);
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

        protected void ResetMocks()
        {
            MockLog.Reset();
            MockLogger.Reset();
            MockAppLogger.Reset();
        }

        protected void VerifyStarted(Type logType, int numTimes)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Started logging for LogType -> ");
            sb.AppendIf(logType.Name);
            sb.AppendLine(String.Format(" At -> {0}", DateTime.Now.ToString()));
            MockLog.Verify(m => m.Info(It.Is<string>(s => s.Equals(sb.ToString())))
            , Times.Exactly(numTimes));
        }

        public void CreateTestInnerException()
        {
            throw new Exception(TEST_INNER_EXCEPTION_MSG)
            {
                Source = TEST_INNER_EXCEPTION_SRC,
            };
        }

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

        public void CreateTestException()
        {
            throw new Exception(TEST_EXCEPTION_MSG)
            {
                Source = TEST_EXCEPTION_SRC
            };
        }

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
