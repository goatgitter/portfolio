using goatgitter.lib.tools;
using System;
using System.ComponentModel;
using System.Reflection;
using static goatgitter.lib.Constants;

namespace goatgitter.lib.extensions
{

    /** 
    * Extension class for Enums.
    * MIT License
    * Copyright (c) 2022 goatgitter
    * */

    public static class Enums
    {

        /// <summary>
        /// Finds the Enum Value for a given description.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="description"></param>
        /// <param name="notepad"></param>
        /// <returns>The Enum Value with the given description, if found, otherwise the default Enum value.</returns>
        public static T GetValByDesc<T>(this string description, ILogger notepad = null) where T : Enum
        {
            T result = default;
            try
            {
                foreach (Enum enumItem in Enum.GetValues(typeof(T)))
                {
                    if (enumItem.GetDesc() == description)
                    {
                        result = (T)enumItem;
                        break;
                    }
                }
            }
            catch (Exception exception)
            {
                ILogger logger = notepad ?? description.GetLog();
                logger.LogExceptionWithData(ERR_ENUM_DESC, new object[] { description }, exception);
            }
            return result;
        }

        /// <summary>
        /// Returns the Description of an Enum Value.
        /// </summary>
        /// <param name="enumValue"></param>
        /// <param name="notepad"></param>
        /// <returns>String containing the description, if one is defined, otherwise, the field name is returned.</returns>
        public static string GetDesc(this Enum enumValue, ILogger notepad = null)
        {
            string result = "";
            try
            {
                FieldInfo fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
                if (fieldInfo.IsNotEmpty())
                {
                    DescriptionAttribute attrInfo = (DescriptionAttribute) Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute));
                    if (attrInfo.IsNotEmpty())
                    {
                        result = attrInfo.Description;
                    }
                    else
                    {
                        result = fieldInfo.Name;
                    }                   
                }                
            }
            catch (Exception exception)
            {
                ILogger logger = notepad ?? enumValue.GetLog();
                logger.LogExceptionWithData(ERR_ENUM_DESC, new object[] { enumValue }, exception);
            }
            return result;
        }

        /// <summary>
        /// Method to determine if an Enum instance contains a value.
        /// </summary>
        /// <param name="enumVal">Enum instance to evaluate.</param>
        /// <returns>bool true if enumVal does not contain a value, otherwise false.</returns>
        public static bool IsEmpty(this Enum enumVal)
        {
            bool result = true;
            string enumText = enumVal.ToString();
            if (enumText.IsNotEmpty() && !enumText.Equals(NONE))
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Method to determine if an Enum instance does not contain a value.
        /// </summary>
        /// <param name="enumVal">Enum instance to evaluate.</param>
        /// <returns>bool true if enumVal does contain a value, otherwise false.</returns>
        public static bool IsNotEmpty(this Enum enumVal)
        {
            return !enumVal.IsEmpty();
        }
    }
}
