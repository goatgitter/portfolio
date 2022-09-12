using goatgitter.lib.extensions;
using log4net;
using System;
using System.Diagnostics;

namespace goatgitter.lib.tools
{
    /** 
     * Utility class Logger handles exceptions gracefully.
     * MIT License
     * Copyright (c) 2022 goatgitter
     * */
    public class Logger : Base
    {
        /// <inheritdoc/>
        public ILog Log { get; private set; }
        /// <inheritdoc/>
        public Type LogType { get; private set; }

        /// <inheritdoc/>
        public Logger(Type logType)
        {
            LogType = logType;
            Init();
        }

        /// <inheritdoc/>
        public Logger()
        {
            LogType = new StackFrame(1).GetMethod().DeclaringType;
            Init();
        }

        private void Init()
        {
            Log = LogManager.GetLogger(LogType);
        }

        /// <inheritdoc/>
        public void LogInfo(string info)
        {
            Log.Info(info);
        }

        /// <inheritdoc/>
        public void LogError(string error)
        {
            Log.Error(error);
        }

        /// <inheritdoc/>
        public void LogWarn(string warning)
        {
            Log.Warn(warning);
        }

        /// <inheritdoc/>
        public void LogException(string message, Exception exception)
        {
            Log.Error(message);
            Log.Error(exception?.LogPrint());
        }

        /// <inheritdoc/>
        public void LogExceptionFormat(string message, object[] messageParams, Exception exception)
        {
            Log.ErrorFormat(message, messageParams);
            Log.Error(exception?.LogPrint());
        }
    }
}
