namespace CoreWiki.Utilities
{
    using System.Text;

    public class UrlHelpers
    {
        public static string UrlFriendly(string title)
        {
            if (title == null)
            {
                return string.Empty;
            }

            const int maxLength = 80;
            var prevdash = false;
            var sb = new StringBuilder(title.Length);

            for (var i = 0; i < title.Length; i++)
            {
                var currentChar = title[i];

                if ((currentChar >= 'a' && currentChar <= 'z') || (currentChar >= '0' && currentChar <= '9'))
                {
                    sb.Append(currentChar);
                    prevdash = false;
                }
                else if (currentChar >= 'A' && currentChar <= 'Z')
                {
                    sb.Append((char)(currentChar | 32));
                    prevdash = false;
                }
                else if (currentChar == ' ' ||
                         currentChar == ',' ||
                         currentChar == '.' ||
                         currentChar == '/' ||
                         currentChar == '\\' ||
                         currentChar == '-' ||
                         currentChar == '_' ||
                         currentChar == '=')
                {
                    if (!prevdash && sb.Length > 0)
                    {
                        sb.Append('-');
                        prevdash = true;
                    }
                }
                else if ((int)currentChar >= 128)
                {
                    var prevlen = sb.Length;
                    sb.Append(RemapInternationalCharToAscii(currentChar));

                    if (prevlen != sb.Length)
                    {
                        prevdash = false;
                    }
                }

                if (i == maxLength)
                {
                    break;
                }
            }

            return prevdash
                ? sb.ToString().Substring(0, sb.Length - 1)
                : sb.ToString();
        }

        public static string RemapInternationalCharToAscii(char c)
        {
            var s = c.ToString().ToLowerInvariant();

            if ("àåáâäãåą".Contains(s))
            {
                return "a";
            }
            else if ("èéêëę".Contains(s))
            {
                return "e";
            }
            else if ("ìíîïı".Contains(s))
            {
                return "i";
            }
            else if ("òóôõöøőð".Contains(s))
            {
                return "o";
            }
            else if ("ùúûüŭů".Contains(s))
            {
                return "u";
            }
            else if ("çćčĉ".Contains(s))
            {
                return "c";
            }
            else if ("żźž".Contains(s))
            {
                return "z";
            }
            else if ("śşšŝ".Contains(s))
            {
                return "s";
            }
            else if ("ñń".Contains(s))
            {
                return "n";
            }
            else if ("ýÿ".Contains(s))
            {
                return "y";
            }
            else if ("ğĝ".Contains(s))
            {
                return "g";
            }
            else if (c == 'ř')
            {
                return "r";
            }
            else if (c == 'ł')
            {
                return "l";
            }
            else if (c == 'đ')
            {
                return "d";
            }
            else if (c == 'ß')
            {
                return "ss";
            }
            else if (c == 'Þ')
            {
                return "th";
            }
            else if (c == 'ĥ')
            {
                return "h";
            }
            else if (c == 'ĵ')
            {
                return "j";
            }
            else
            {
                return "";
            }
        }
    }
}