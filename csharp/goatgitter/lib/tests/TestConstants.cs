using System.ComponentModel;
using static goatgitter.lib.Constants;

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

        public enum TEST_ENUM_RATING { None = 0, Best = 1, Good = 2, Fair = 3, Poor = 4, Incomplete = 5 };
        public enum TEST_ENUM_GRADE
        {
            [Description(NONE)]
            None = 0,
            [Description(BEST)]
            A,
            B,
            [Description("Fair")]
            C,
            [Description("Poor")]
            D
        }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
