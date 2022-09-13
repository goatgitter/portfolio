using goatgitter.lib.extensions;
using log4net;
using System;
using System.Diagnostics;
using System.Text;

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
        /// Constructor for the Logger class that accepts the Log Type as a parameter.
        /// </summary>
        /// <param name="logType">They Type to be printed in the Log4Net Log Messages.</param>
        public Logger(Type logType)
        {
            LogType = logType;
            Init();
        }

        /// <summary>
        /// Constructor for the Logger class that automatically sets the Log Type to the caller's type.
        /// </summary>
        public Logger()
        {
            LogType = new StackFrame(1).GetMethod().DeclaringType;
            Init();
        }

        private void Init()
        {
            Log = LogManager.GetLogger(LogType);
            Started();
        }

        /// <summary>
        /// Logs a message to the Log at the Info Level that <> Started.
        /// </summary>
        /// <param name="info">A string containing the message to be logged.</param>
        public void Started()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Started logging for LogType -> ");
            sb.AppendIf(LogType.Name);
            sb.AppendLine(String.Format(" At -> {0}", DateTime.Now.ToString()));
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
            Log.ErrorFormat(message, messageData);
            Log.Error(exception?.LogPrint());
        }
    }
}
