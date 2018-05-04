using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace WindAPI.Handlers
{
    /// <summary>
    /// Class to handle string modifications.
    /// </summary>
    public class StringHandler
    {
        /// <summary>
        /// Convert a string to TitleCase.
        /// </summary>
        /// <param name="toConvert">The string to convert.</param>
        /// <returns>The input string converted to TitleCase.</returns>
        public static string ToTitleCase(string toConvert)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(toConvert.ToLower());
        }
    }
}