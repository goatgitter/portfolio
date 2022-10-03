using System;
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
