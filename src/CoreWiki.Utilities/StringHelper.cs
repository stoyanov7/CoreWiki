﻿namespace CoreWiki.Utilities
{
    using System;
    using System.Text.RegularExpressions;

    public static class StringHelper
    {
        /// <summary>
        /// Returns the singular or plural noun based on the value of the count argument
        /// </summary>
        /// <param name="singular">Denoting or referring to just one person or thing</param>
        /// <param name="plural">denoting more than on</param>
        /// <param name="count">The total number of items</param>
        /// <param name="prependCount"></param>
        /// <returns></returns>
        public static string Pluralize(string singular, string plural, int count, bool prependCount = false)
        {
            var noun = count == 1 ? singular : plural;

            return prependCount ? $"{count} {noun}" : noun;
        }

        /// <summary>
        /// Returns the number of words in a content.
        /// </summary>
        /// <param name="content">The string to count.</param>
        /// <returns>The number of words in the sentence.</returns>
        public static int WordCount(this string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return 0;
            }

            var matches = Regex.Matches(content, @"\b\S+\b");

            return matches.Count;
        }

        /// <summary>
        /// Returns the amount of time to read a string.
        /// </summary>
        /// <param name="content">The string we wish to calculate.</param>
        /// <returns>A TimeSpan of time required to read the string</returns>
        public static TimeSpan CalculateReadTime(this string content)
        {
            const decimal wpm = 275.0m;
            var wordCount = content.WordCount();
            var minutes = (double)(wordCount / wpm);

            return TimeSpan.FromMinutes(minutes);
        }
    }
}