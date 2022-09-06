using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goatgitter.lib.extensions
{
    /** 
   * Extension class for StringBuilders.
   * MIT License
   * Copyright (c) 2022 goatgitter
   * */
    public static class StringBuilders
    {
        /// <inheritdoc/>
        public static StringBuilder AppendIf(this StringBuilder sb, string str)
        {
            if (str.IsNotEmpty())
                sb.Append(str);
            return sb;
        }

        /// <inheritdoc/>
        public static StringBuilder AppendLineIf(this StringBuilder sb, string str)
        {
            if (str.IsNotEmpty())
                sb.AppendLine(str);
            return sb;
        }
    }
}
