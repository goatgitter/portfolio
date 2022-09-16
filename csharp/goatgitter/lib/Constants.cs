﻿namespace goatgitter.lib
{
    /// <summary>
    /// Class of static Constants used throughout the application.
    /// </summary>
    public static class Constants
    {
        // Error Messages
        public const string ERR_CREATE_DIR = "An error occurred while creating directory {0}.";
        public const string ERR_PRINT_EXCEPTION = "An error occurred while printing the exception to the log file.";
        public const string ERR_DELETE_DIR = "An error occurred while deleting directory {0} with emptyFolder set to {1}.";
        public const string ERR_OCCURRED = "An error occurred in method: ";
        public const string ERR_EXCEPTION_MSG = "Exception Message :";
        public const string ERR_CREATE_FILE = "An error occurred while creating file {0}.";
        public const string ERR_GET_PATH = "An error occurred while getting the file path for directory = {0} file = {1}.";
        public const string ERR_VALID_FILE_NAME = "An error occurred while attempting to determine if {1} contains a valid file name.";

        // Validation
        public static char[] INVALID_DIR_CHRS = new char[] { '*', '+' };
    }
}
