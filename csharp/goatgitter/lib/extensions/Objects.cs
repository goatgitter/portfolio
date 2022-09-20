using System;
using goatgitter.lib.tools;
using log4net;

namespace goatgitter.lib.extensions
{
    /** 
   * Extension class for Objects.
   * MIT License
   * Copyright (c) 2022 goatgitter
   * */

    public static class Objects
    {
        private static ILogger Notepad { get; set; }
        /// <summary>
        /// Gets the static logger.
        /// If the Notepad is empty, it is created.
        /// </summary>
        /// <param name="obj">An object to retrieve the log for.</param>
        /// <param name="log">An optional ILog to retrieve the log for.</param>
        /// <param name="logger">An optional ILoggger to retrieve the log for.</param>
        /// <returns></returns>
        public static ILogger GetLog(this object obj, ILog log = null, ILogger logger = null)
        {
            if (obj.IsNotEmpty())
            {
                if (Notepad.IsEmpty())
                {
                    if (logger.IsNotEmpty())
                    {
                        Notepad = logger;
                    }
                    else
                    {
                        Notepad = new Logger(log, obj.GetType());
                    }
                }
            }
            return Notepad;
        }

        /// <summary>
        /// Logs detailed information about an exception to the log.
        /// </summary>
        /// <param name="obj">An object with a log to be updated.</param>
        /// <param name="msg">An application message to assist with support debugging.</param>
        /// <param name="data">And array of objects containing data for the message.</param>
        /// <param name="exception">An Exception with details to log.</param>
        /// <param name="log">An optional ILog to update.</param>
        public static void LogExceptionWithData(this object obj, string msg, object[] data, Exception exception, ILog log = null)
        {
            ILogger notepad = GetLog(obj, log);
            notepad.LogExceptionWithData(msg, data, exception);
        }

        /// <inheritdoc/>
        public static string SafeToString(this object obj)
        {
            string result = null;
            // Is the object NOT empty?
            if (obj.IsNotEmpty())
            {
                // Is the object's type an int32?
                if (typeof(Int32).IsInstanceOfType(obj))
                {
                    // Cast object before calling toString to avoid error.
                    Int32 intObj = (Int32)obj;
                    result = intObj.ToSafeString();
                }
                else
                {
                    // Convert to string using type's toString method.
                    result = obj.ToString();
                }
            }
            return result;
        }

        /// <inheritdoc/>
        public static bool IsEmpty(this object obj)
        {
            bool result = obj == null;
            return result;
        }

        /// <inheritdoc/>
        public static bool IsNotEmpty(this object obj)
        {
            bool result = obj != null;
            return result;
        }

        /// <inheritdoc/>
        public static bool SafeEquals(this object obj, object other)
        {
            bool result = false;
            // Are both objects empty?
            if (obj.IsEmpty() && other.IsEmpty())
            {
                // Yes, then they are equal.
                result = true;
            }
            // Are both objects NOT empty?
            else if (obj.IsNotEmpty() && other.IsNotEmpty())
            {
                // Are both objects the same type?
                if (obj.GetType().Equals(other.GetType()))
                {
                    // Check equality using type's Equals method.
                    result = obj.Equals(other);
                } 
            }
            return result;
        }
    }
}
