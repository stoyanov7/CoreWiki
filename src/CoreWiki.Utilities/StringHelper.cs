namespace CoreWiki.Utilities
{
    using System;

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
            if (content == null)
            {
                return 0;
            }

            var wordCount = 0;
            var letterCount = 0;

            foreach (var c in content)
            {
                if (c == '\'')
                {
                    continue;
                }

                if (char.IsLetterOrDigit(c))
                {
                    letterCount++;
                }
                else if (letterCount > 0)
                {
                    letterCount = 0;
                    wordCount++;
                }
            }

            if (letterCount > 0)
            {
                wordCount++;
            }

            return wordCount;
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