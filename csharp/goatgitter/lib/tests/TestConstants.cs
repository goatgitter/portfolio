using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goatgitter.lib.tests
{
    /// <summary>
    /// A class of constant data used in Unit Tests.
    /// </summary>
    public static class TestConstants
    {
        #pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
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
        public const string TEST_DIR_DNE = "doesNotExist";
        public const string TEST_DIR_INVALID = "*+";
        public const string TEST_FILE_INVALID = "testFile*+";
        public const string TEST_ERROR_MSG = "Test Error Message";
        public const string TEST_ERROR_MSG_WITH_DATA = "Test Error Message data1-> {0}, data2-> {1}";
        public const string TEST_INVALID_FILE_NAME_ERR_VAL = "<doesNotExist/blah.txt";
        #pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
