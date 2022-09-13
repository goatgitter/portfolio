using System;
using goatgitter.lib.tools;

namespace goatgitter.lib.extensions
{
    /** 
   * Extension class for Objects.
   * MIT License
   * Copyright (c) 2022 goatgitter
   * */

    public static class Objects
    {
        private static Logger Notepad { get; set; }
        static Logger GetLog(this object obj)
        {
            if (obj.IsNotEmpty())
            {
                if (Notepad.IsEmpty())
                {
                    Notepad = new Logger(null, obj.GetType());
                }
            }
            return Notepad;
        }

        /// <inheritdoc/>
        public static string SafeToString(this object obj)
        {
            string result = null;
            // Is the object NOT empty?
            if (obj.IsNotEmpty())
            {
                // Is the object's type an int32?
                if (typeof(Int32).IsInstanceOfType(obj))
                {
                    // Cast object before calling toString to avoid error.
                    Int32 intObj = (Int32)obj;
                    result = intObj.ToSafeString();
                }
                else
                {
                    // Convert to string using type's toString method.
                    result = obj.ToString();
                }
            }
            return result;
        }

        /// <inheritdoc/>
        public static bool IsEmpty(this object obj)
        {
            bool result = obj == null;
            return result;
        }

        /// <inheritdoc/>
        public static bool IsNotEmpty(this object obj)
        {
            bool result = obj != null;
            return result;
        }

        /// <inheritdoc/>
        public static bool SafeEquals(this object obj, object other)
        {
            bool result = false;
            // Are both objects empty?
            if (obj.IsEmpty() && other.IsEmpty())
            {
                // Yes, then they are equal.
                result = true;
            }
            // Are both objects NOT empty?
            else if (obj.IsNotEmpty() && other.IsNotEmpty())
            {
                // Are both objects the same type?
                if (obj.GetType().Equals(other.GetType()))
                {
                    // Check equality using type's Equals method.
                    result = obj.Equals(other);
                } 
            }
            return result;
        }
    }
}
