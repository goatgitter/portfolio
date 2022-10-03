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
