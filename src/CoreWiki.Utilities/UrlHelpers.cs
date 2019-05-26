namespace CoreWiki.Utilities
{
    using System.Globalization;
    using System.Text;
    using System.Text.RegularExpressions;

    public class UrlHelpers
    {
        private static readonly Regex slugCharacters = new Regex(@"([\s,.//\\-_=])+");

        /// <summary>
        /// Produces optional, URL-friendly version of a Topic, "like-this-one". 
        /// </summary>
        public static string UrlFriendly(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return string.Empty;
            }

            var newTitle = title.ToLowerInvariant();
            newTitle = slugCharacters.Replace(newTitle, "-");

            return RemoveDiacritics(newTitle);
        }

        private static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);

                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            return sb
                .ToString()
                .Normalize(NormalizationForm.FormC);
        }

    }
}