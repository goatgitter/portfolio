﻿using System;
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
        public static ILogger AppNotepad { get; set; }
      
        static BaseWithLog()
        {
            if (AppNotepad.IsEmpty())
            {
                InitAppNotepad();
            }
        }
        private static void InitAppNotepad()
        {
            Type logType = new StackFrame(3).GetMethod().DeclaringType;
            AppNotepad = new Logger(null, logType);
        }

        /// <summary>
        ///  Instance Notepad Property for logging purposes.
        /// </summary>
        public ILogger Notepad { get; set; }

        /// <summary>
        /// Constructor for BaseWithLog Objects.
        /// Automatically creates a Notepad for logging.
        /// </summary>
        public BaseWithLog(ILogger appNotepad = null, ILogger notepad = null) : base()
        {
            if (AppNotepad.IsEmpty())
            {
                if (appNotepad.IsEmpty())
                {
                    InitAppNotepad();
                }
                else
                {
                    AppNotepad = appNotepad;
                }                    
            }
            if (Notepad.IsEmpty())
            {
                if (notepad.IsEmpty())
                {
                    Type logType = new StackFrame(1).GetMethod().DeclaringType;
                    Notepad = new Logger(null, logType);
                }
                else
                {
                    Notepad = notepad;
                }               
            }
        }
    }
}
