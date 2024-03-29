﻿using goatgitter.lib.extensions;
using System;
using System.Text;
using goatgitter.lib.tools;
using System.Diagnostics;

namespace goatgitter.lib
{
    /** 
    * Base class from which other classes will inherit.
    * MIT License
    * Copyright (c) 2022 goatgitter
    * */
    public class Base
    {
        /// <summary>
        /// Static Application Path for File Input/Output purposes.
        /// </summary>
        public static string AppPath { get; set; }

        static Base()
        {
            if (AppPath.IsEmpty())
            {
                AppPath = AppDomain.CurrentDomain.BaseDirectory;
            }
        }
        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            bool result = false;
            foreach (var prop in this.GetType().GetProperties())
            {
                object valObj = prop.GetValue(this, null);
                object valOther = prop.GetValue(obj, null);
                result = valObj.SafeEquals(valOther);
            }
            return result;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int result = 0;
            foreach (var prop in this.GetType().GetProperties())
            {
                object valObj = prop.GetValue(this, null);
                result = HashCode.Combine(valObj.GetHashCode());
            }
            return result;
        }


        /// <inheritdoc/>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var prop in this.GetType().GetProperties())
            {
                sb.Append("Prop: ");
                sb.AppendIf(prop.Name);
                sb.Append(" => ");
                object valObj = prop.GetValue(this, null);
                string val = valObj.SafeToString();               
                sb.AppendIf(val);
                sb.Append(System.Environment.NewLine);
            }
            Console.WriteLine(sb.ToString());
            return sb.ToString();
        }

        /// <summary>
        /// Generic method for performing deep clone on an object that inherits from Base.
        /// </summary>
        /// <typeparam name="T">The Type of object being cloned.</typeparam>
        /// <returns>An object of the specified type with all the data from the source object set.</returns>
        public T Clone<T>()
        {
            object clone = this.MemberwiseClone();
            T result = (T)clone;
            foreach (var prop in this.GetType().GetProperties())
            {
                object valObj = prop.GetValue(this, null);
                prop.SetValue(result, valObj);
            }
            return result;
        }
    }
}
