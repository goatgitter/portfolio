using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using goatgitter.lib.tools;
using goatgitter.lib.extensions;
using System.Diagnostics;

namespace goatgitter.lib
{
    /** 
    * Base class with Logging from which other classes will inherit.
    * MIT License
    * Copyright (c) 2022 goatgitter
    * */
    public class BaseWithLog : Base
    {
        /// <summary>
        /// Static Application Notepad Property for logging purposes.
        /// </summary>
        public static Logger AppNotepad { get; set; }
        static BaseWithLog()
        {
            if (AppNotepad.IsEmpty())
            {
                AppNotepad = new Logger();
            }
        }

        /// <summary>
        ///  Instance Notepad Property for logging purposes.
        /// </summary>
        public ILogger Notepad { get; set; }

        /// <summary>
        /// Constructor for BaseWithLog Objects.
        /// Automatically creates a Notepad for logging.
        /// </summary>
        public BaseWithLog(ILogger notepad = null) : base()
        {
            if (AppNotepad.IsEmpty())
            {
                AppNotepad = new Logger();
            }
            if (Notepad.IsEmpty())
            {
                if (notepad.IsEmpty())
                {
                    Type logType = new StackFrame(1).GetMethod().DeclaringType;
                    Notepad = new Logger(logType);
                }
                else
                {
                    Notepad = notepad;
                }
               
            }
        }
    }
}
