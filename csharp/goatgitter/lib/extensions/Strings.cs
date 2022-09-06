using System;

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
            return str.Split(new char[] { ' ', '.', '?' },
                             StringSplitOptions.RemoveEmptyEntries).Length;
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
    }
}
