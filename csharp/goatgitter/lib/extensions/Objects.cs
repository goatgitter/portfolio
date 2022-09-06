using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goatgitter.lib.extensions
{
    /** 
   * Extension class for Objects.
   * MIT License
   * Copyright (c) 2022 goatgitter
   * */

    public static class Objects
    {
        /// <inheritdoc/>
        public static string SafeToString(this object obj)
        {
            string result = null;
            if (obj != null)
            {
                if (typeof(Int32).IsInstanceOfType(obj))
                {
                    Int32 intObj = (Int32)obj;
                    result = intObj.ToString();
                }
                else
                {
                    result = obj.ToString();
                }
            }
            return result;
        }

        /// <inheritdoc/>
        public static bool SafeEquals(this object obj, object other)
        {
            bool result = false;
            if (obj != null)
            {
                if (obj.GetType().Equals(other.GetType()))
                {
                    result = obj.Equals(other);
                } 
            }
            return result;
        }
    }
}
