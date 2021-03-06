﻿using System;
using System.Linq;

namespace StUtil.Extensions
{
    /// <summary>
    /// Extensions for Strings
    /// </summary>
    /// <remarks>
    /// 2013-06-26 - Initial version
    /// </remarks>
    public static class StringExtensions
    {
        /// <summary>
        /// The type of casing to apply
        /// </summary>
        public enum Casing
        {
            Proper,
            Sentence,
            Caps,
            Lower,
            Camel
        }

        /// <summary>
        /// Convert a string to a string of the specified casing
        /// </summary>
        /// <param name="Text">The text to change the case of</param>
        /// <param name="Case">The casing to apply</param>
        /// <returns>A string changed to the specified case</returns>
        public static string ChangeCase(this string Text, Casing Case)
        {
            switch (Case)
            {
                case Casing.Caps:
                    return Text.ToUpper();

                case Casing.Lower:
                    return Text.ToLower();

                case Casing.Proper:
                    return String.Join(" ", Text
                        .Trim()
                        .Split(' ')
                        .Where(str => str.Length > 0)
                        .Select(str => Char.ToUpper(str[0]) + str.Substring(1))
                        .ToArray());

                case Casing.Sentence:
                    return String.Join(" ", Text
                        .Trim()
                        .Split(' ')
                        .Where(str => str.Length > 0)
                        .Select((str, i) => i == 0 ? Char.ToUpper(str[0]) + str.Substring(1) : str.ToLower())
                        .ToArray());

                case Casing.Camel:
                    return String.Join("", Text
                        .Trim()
                        .Split(' ')
                        .Where(str => str.Length > 0)
                        .Select((str, i) => i != 0 ? Char.ToUpper(str[0]) + str.Substring(1) : str.ToLower())
                        .ToArray());

                default:
                    return Text;
            }
        }

        /// <summary>
        /// Gets a string between two specified strings
        /// </summary>
        /// <param name="text">The text to get the text from</param>
        /// <param name="start">The string before the string to extract</param>
        /// <param name="end">The string after the string to extract</param>
        /// <returns>The string between the start and end strings</returns>
        public static string GetStringBetween(this string text, string start, string end)
        {
            int startpos = 0;
            return GetStringBetween(text, start, end, ref startpos);
        }

        /// <summary>
        /// Gets a string between two specified strings
        /// </summary>
        /// <param name="text">The text to get the text from</param>
        /// <param name="start">The string before the string to extract</param>
        /// <param name="end">The string after the string to extract</param>
        /// <param name="startPos">
        /// A pointer to an index within the string to start searching from
        /// </param>
        /// <returns>The string between the start and end strings</returns>
        public static string GetStringBetween(this string text, string start, string end, ref int startPos)
        {
            int pos = text.IndexOf(start, startPos);
            if (pos != -1)
            {
                pos += start.Length;
                int pos2 = text.IndexOf(end, pos);
                if (pos2 != -1)
                {
                    startPos = pos2;
                    return text.Substring(pos, pos2 - pos);
                }
            }
            else
            {
                startPos = -1;
            }
            return string.Empty;
        }
    }
}