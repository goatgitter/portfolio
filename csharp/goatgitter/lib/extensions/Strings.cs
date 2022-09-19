using goatgitter.lib.tools;
using log4net;
using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
        public static bool IsValidFileName(this string str, ILogger notepad = null)
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
                    ILogger logger = notepad ?? str.GetLog();
                    logger.LogExceptionWithData(ERR_VALID_FILE_NAME, new object[] { str }, exception);
                }
            }
            return isValid;
        }
        
        /// <summary>
        /// Method to check if a string contains a valid directory name.
        /// </summary>
        /// <param name="str">The string to be validated.</param>
        /// <param name="notepad">A logger to log any exceptions encountered.</param>
        /// <returns>A boolean indicating if the string is a valid directory name.</returns>
        public static bool IsValidDirName(this string str, ILogger notepad = null)
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
                    ILogger logger = notepad ?? str.GetLog();
                    logger.LogExceptionWithData(ERR_VALID_DIR_NAME, new object[] { str }, exception);
                }
            }
            return isValid;
        }
        /// <summary>
        /// Method that checks if a string contains a valid directory and file name.
        /// </summary>
        /// <param name="str">A string to validate.</param>
        /// <param name="notepad">Optional logger to log exceptions.</param>
        /// <returns>bool</returns>
        public static bool IsValidDirAndFileName(this string str, ILogger notepad = null)
        {
            bool isValid = IsValidDirName(str, notepad) && IsValidFileName(str, notepad);
            return isValid;
        }
    }
}
