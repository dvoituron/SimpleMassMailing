using System;

namespace SimpleMassMailing.Data
{
    static class StringExtension
    {
        public static string InnerText(this string content, string startTag, string endTag)
        {
            int startTagIndex = content.IndexOf(startTag, StringComparison.InvariantCultureIgnoreCase);
            if (startTagIndex < 0) return String.Empty;
            startTagIndex += startTag.Length;

            int endTagIndex = content.IndexOf(endTag, StringComparison.InvariantCultureIgnoreCase);
            if (endTagIndex < 0) return String.Empty;

            return content.Substring(startTagIndex, endTagIndex - startTagIndex);
        }

        public static string Left(this string value, int count)
        {
            if (value.Length > count)
                return value.Substring(0, count);
            else
                return value;
        }
    }
}
