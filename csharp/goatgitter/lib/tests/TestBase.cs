using NUnit.Framework;
using System;

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
