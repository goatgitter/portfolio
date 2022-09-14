using goatgitter.lib.extensions;
using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using static goatgitter.lib.Constants;

namespace goatgitter.lib.tools
{
    public interface ILogger
    {
        ILog Log { get; set; }
        Type LogType { get; set; }
        void Started();
        void LogInfo(string info);
        void LogError(string error);
        void LogWarn(string warning);
        void LogException(string message, Exception exception);
        void LogExceptionWithData(string message, object[] messageData, Exception exception);
    }

    /** 
     * Utility class Logger handles exceptions gracefully.
     * MIT License
     * Copyright (c) 2022 goatgitter
     * */
    public class Logger : Base, ILogger
    {
        
        /// <summary>
        /// The Log4Net Log
        /// </summary>
        public ILog Log { get; set; }

        /// <summary>
        /// The Type that will be printed in the Log4Net Log Messages.
        /// </summary>
        public Type LogType { get; set; }

        /// <summary>
        /// Constructor for the Logger class that automatically sets the Log Type to the caller's type.
        /// </summary>
        /// <param name="log">The log4net log to use, or null.  If Null, automatically created.</param>
        /// <param name="logType">They Type to be printed in the Log4Net Log Messages.</param>
        public Logger(ILog log = null, Type logType = null)
        {
            Log = log;
            LogType = logType;
            if (LogType.IsEmpty())
            {
                LogType = new StackFrame(1).GetMethod().DeclaringType;
            }

            if (Log.IsEmpty())
            {
                Log = LogManager.GetLogger(LogType);
            }
        }

        /// <summary>
        /// Logs a message to the Log at the Info Level that <> Started.
        /// </summary>
        /// <param name="info">A string containing the message to be logged.</param>
        public void Started()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("");
            sb.AppendLine("\t\t Started logging for :");
            sb.Append("\t\t LogType -> ");
            sb.AppendIf(LogType.Name);
            sb.AppendLine("");
            sb.AppendLine(String.Format("\t\t At -> {0}", DateTime.Now.ToString()));
            Log.Info(sb.ToString());
        }

        /// <summary>
        /// Logs a message to the Log at the Info Level.
        /// </summary>
        /// <param name="info">A string containing the message to be logged.</param>
        public void LogInfo(string info)
        {
            Log.Info(info);
        }

        /// <summary>
        /// Logs a message to the Log at the Error Level.
        /// </summary>
        /// <param name="error">A string containing the message to be logged.</param>
        public void LogError(string error)
        {
            Log.Error(error);
        }

        /// <summary>
        /// Logs a message to the Log at the Warning Level.
        /// </summary>
        /// <param name="warning">A string containing the message to be logged.</param>
        public void LogWarn(string warning)
        {
            Log.Warn(warning);
        }

        /// <summary>
        /// Logs a message to the Log at the Error Level.
        /// </summary>
        /// <param name="message">A string containing the message to be logged.</param>
        /// <param name="exception">An exception contianing information to be logged.</param>
        public void LogException(string message, Exception exception)
        {
            Log.Error(message);
            Log.Error(exception?.LogPrint());
        }


        /// <summary>
        /// Logs a message with data to the Log at the Error Level.
        /// </summary>
        /// <param name="message">A string containing the message to be logged.</param>
        /// <param name="messageData">An array of objects containing the data for the message to be logged.</param>
        /// <param name="exception">An exception contianing information to be logged.</param>
        public void LogExceptionWithData(string message, object[] messageData, Exception exception)
        {
            string callingMethod = new StackFrame(1).GetMethod().Name;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(ERR_OCCURRED);
            sb.AppendIf(callingMethod);
            sb.AppendLine("");
            sb.AppendLine(ERR_EXCEPTION_MSG);
            sb.AppendLineIf(exception?.LogPrint());
            Log.ErrorFormat(message, messageData);
            Log.Error(sb.ToString());
        }
    }
}
