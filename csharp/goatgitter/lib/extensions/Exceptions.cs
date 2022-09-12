using System;
using System.Text;
using static goatgitter.lib.Constants;
using static goatgitter.lib.Base;

namespace goatgitter.lib.extensions
{
    /** 
     * Extension class for Exceptions.
     * MIT License
     * Copyright (c) 2022 goatgitter
     * */
    public static class Exceptions
    {
        /// <summary>
        /// Prints details about an Exception for Logging purposes.
        /// </summary>
        /// <param name="exception">The exception to inspect for details to log.</param>
        /// <returns>A String containing the relevant details from the exception object.</returns>
        public static string LogPrint(this Exception exception)
        {
            StringBuilder sb = new StringBuilder(); 
            try
            {
                sb.AppendLineIf(exception?.Message);
                sb.AppendLineIf(exception?.StackTrace);
                sb.AppendLineIf(exception?.InnerException?.Message);
                sb.AppendLineIf(exception?.InnerException?.StackTrace);
                sb.AppendLineIf(exception?.Source);
            }
            catch(Exception inner)
            {
                Notepad.LogException(ERR_PRINT_EXCEPTION, inner);
            }            
            return sb.ToString();
        }
    }
}
