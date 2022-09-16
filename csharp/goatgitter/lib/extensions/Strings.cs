using System;
using System.IO;
using System.Linq;
using static goatgitter.lib.Constants;

namespace goatgitter.lib.extensions
{

    /** 
    * Extension class for Strings.
    * MIT License
    * Copyright (c) 2022 goatgitter
    * */

    public static class Strings
    {
        /// <inheritdoc/>
        public static int WordCount(this string str)
        {
            int result = 0;
            if (str.IsNotEmpty()) 
            {
                result = str.Split(new char[] { ' ', '.', '?' },
                             StringSplitOptions.RemoveEmptyEntries).Length;
            }
            return result;
        }

        /// <inheritdoc/>
        public static bool IsEmpty(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <inheritdoc/>
        public static bool IsNotEmpty(this string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }

        /// <inheritdoc/>
        public static bool IsValidFileName(this string str)
        {
            bool isValid = false;
            if (str.IsNotEmpty() )
            {
                try
                {
                    string fileName = Path.GetFileName(str);
                    char[] invalidChars = Path.GetInvalidFileNameChars();
                    foreach (char c in invalidChars)
                    {
                        if (fileName.Contains(c))
                        {
                            isValid = false;
                            break;
                        }
                        isValid = true;
                    }
                }
                catch (Exception exception)
                {
                    BaseWithLog.AppNotepad.LogExceptionWithData(ERR_VALID_FILE_NAME, new object[] {str}, exception);
                }
            }
            return isValid;
        }

        /// <inheritdoc/>
        public static bool IsValidDirName(this string str)
        {
            bool isValid = false;
            if (str.IsNotEmpty())
            {
                try
                {
                    char[] defaultInvalidChars = Path.GetInvalidPathChars();
                    char[] allInvalidChars = defaultInvalidChars.Concat(INVALID_DIR_CHRS).ToArray();
                    
                    foreach (char c in allInvalidChars)
                    {
                        if (str.Contains(c))
                        {
                            isValid = false;
                            break;
                        }
                        isValid = true;
                    }
                }
                catch (Exception exception)
                {
                    BaseWithLog.AppNotepad.LogExceptionWithData(ERR_VALID_FILE_NAME, new object[] { str }, exception);
                }
            }
            return isValid;
        }
    }
}
