using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace goatgitter.lib
{
    /** 
     * Utility class Logger handles exceptions gracefully.
     * MIT License
     * Copyright (c) 2022 goatgitter
     * */
    public class Logger
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
    }
}
