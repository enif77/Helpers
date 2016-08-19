/* Helpers - (C) 2016 Premysl Fara 
 
Helpers are available under the zlib license :

This software is provided 'as-is', without any express or implied
warranty.  In no event will the authors be held liable for any damages
arising from the use of this software.

Permission is granted to anyone to use this software for any purpose,
including commercial applications, and to alter it and redistribute it
freely, subject to the following restrictions:

1. The origin of this software must not be misrepresented; you must not
   claim that you wrote the original software. If you use this software
   in a product, an acknowledgment in the product documentation would be
   appreciated but is not required.
2. Altered source versions must be plainly marked as such, and must not be
   misrepresented as being the original software.
3. This notice may not be removed or altered from any source distribution.
 
 */

namespace Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;


    /// <summary>
    /// Helper class for strings.
    /// </summary>
    public static class StringsHelper
    {
        /// <summary>
        /// Checks, the s contains any string from values.
        /// </summary>
        /// <param name="s">A tested string.</param>
        /// <param name="values">A list of values, that are expected to be in the string s.</param>
        /// <returns>True, it the s contains atleast one value from falues.</returns>
        public static bool ContainsOneOf(string s, IEnumerable<string> values)
        {
            return values.Any(s.Contains);
        }

        /// <summary>
        /// Returns true if the s is in values.
        /// </summary>
        /// <param name="values">The list of values.</param>
        /// <param name="s">A string.</param>
        /// <returns>True if the s is in values.</returns>
        public static bool IsIn(IEnumerable<string> values, string s)
        {
            return values.Any(value => value.Equals(s));
        }

        /// <summary>
        /// GetStringInBetween finds the first occurrence of the “begin” and “end” strings, 
        /// then you can use result[1] to allow you to move ahead down the html document to 
        /// find the next value.
        /// </summary>
        /// <param name="startString">A start string.</param>
        /// <param name="endString">An end string.</param>
        /// <param name="sourceString">A source string.</param>
        /// <param name="includeStartString">Should the result include the start string.</param>
        /// <param name="includeEndString">Should the result contain the end string.</param>
        /// <returns>Result[0] = found string or null. Result[1] = where to search next or null.</returns>
        public static string[] GetStringBetween(
            string startString,
            string endString,
            string sourceString,
            bool includeStartString = false,
            bool includeEndString = false)
        {
            if (String.IsNullOrEmpty(startString))
            {
                throw new ArgumentException("A start string expected.", "startString");
            }

            if (String.IsNullOrEmpty(endString))
            {
                throw new ArgumentException("An end string expected.", "endString");
            }

            // Prepare an empty result.
            string[] result = { null, null };

            // Find the first occurence of the start string.
            var indexOfStart = sourceString.IndexOf(startString, StringComparison.InvariantCulture);
            if (indexOfStart == -1)
            {
                // Stay where we are.
                result[1] = sourceString;

                // No start found. The result[0] remains null.
                return result;
            }

            // Include the Begin string if desired.
            if (includeStartString)
            {
                indexOfStart -= startString.Length;
            }

            // Create a new sourceString, cutting out the start string.
            // TODO: This should be avoided - set the "start looking for end from" variable instead.
            sourceString = sourceString.Substring(indexOfStart + startString.Length);

            // Find the first occurence of the end string after the start string.
            var indexOfEnd = sourceString.IndexOf(endString, StringComparison.InvariantCulture);
            if (indexOfEnd == -1)
            {
                // The end string can was not found. 
                // The result[0] remains null.
                // The result[1] remains null = there is nowhere to go next.
                return result;
            }

            // include the End string if desired
            if (includeEndString)
            {
                indexOfEnd += endString.Length;
            }

            // Set the result[0] to the string between the start and the end strings.
            result[0] = sourceString.Substring(0, indexOfEnd);

            // Advance beyond this segment.
            if (indexOfEnd + endString.Length < sourceString.Length)
            {
                result[1] = sourceString.Substring(indexOfEnd + endString.Length);
            }

            return result;
        }
    }
}
