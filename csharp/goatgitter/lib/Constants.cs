namespace goatgitter.lib
{
    /// <summary>
    /// Class of static Constants used throughout the application.
    /// </summary>
    public static class Constants
    {
        // Error Messages
        #pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public const string ERR_CREATE_DIR = "An error occurred while creating directory {0}.";

        public const string ERR_PRINT_EXCEPTION = "An error occurred while printing the exception to the log file.";
        public const string ERR_DELETE_DIR = "An error occurred while deleting directory {0} with emptyFolder set to {1}.";
        public const string ERR_OCCURRED = "An error occurred in method: ";
        public const string ERR_EXCEPTION_MSG = "Exception Message :";
        public const string ERR_CREATE_FILE = "An error occurred while creating file {0}.";
        public const string ERR_GET_PATH = "An error occurred while getting the file path for directory = {0} file = {1}.";
        public const string ERR_VALID_FILE_NAME = "An error occurred while attempting to determine if {0} contains a valid file name.";
        public const string ERR_VALID_DIR_NAME = "An error occurred while attempting to determine if {0} contains a valid directory name.";
        public const string ERR_GET_FILE_FOR_UPDATE = "An error occurred while attempting to retrieve file for update from folder -> {0} and fileName -> {1}.";
        public const string ERR_GET_FILE = "An error occurred while attempting to retrieve file from folder -> {0} with name -> {1}.";
        public const string ERR_ENUM_DESC = "An error occurred while attempting to retrieve the description for enum {0} .";

        // Validation
        public static char[] INVALID_DIR_CHRS = new char[] { '*', '+' };

        // Constants for Extensions
        public const string NONE = "None";
        public const string BEST = "Best";
        
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
