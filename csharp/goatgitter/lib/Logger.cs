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
        public ILog Log { get; private set; }
        public Type LogType { get; private set; }

        public Logger(Type logType)
        {
            LogType = logType;
            Log = LogManager.GetLogger(LogType);
        }

        public Logger()
        {
            LogType = new StackFrame(1).GetMethod().DeclaringType;
            Log = LogManager.GetLogger(LogType);
        }

        public void LogInfo(string info)
        {
            Log.Info(info);
        }
    }
}
