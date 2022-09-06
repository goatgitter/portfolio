using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goatgitter.lib.extensions
{
    /** 
    * Extension class for Ints.
    * MIT License
    * Copyright (c) 2022 goatgitter
    * */

    public static class Ints
    {
        /// <inheritdoc/>
        public static bool IsPositive(this int num)
        {
            return num != Int32.MinValue && num > 0;
        }

        /// <inheritdoc/>
        public static string ToString(this int num)
        {
            string result = null;
            if (num.IsPositive())
            {
                result = num.ToString();
            }
            return result;
        }
    }
}
