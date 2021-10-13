﻿using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace _0_Framework.Application.Extensions
{
    public static class GenerateSlug
    {
        public static string ToSlug(this string phrase)
        {
            var s = phrase.RemoveDiacritics().ToLower();

            // remove invalid characters
            s = Regex.Replace(s, @"[^\u0600-\u06FF\uFB8A\u067E\u0686\u06AF\u200C\u20Fa-z0-9\s-]",
                "");
            // single space
            s = Regex.Replace(s, @"\s+", " ").Trim();
            // cut and trim
            s = s.Substring(0, s.Length <= 100 ? s.Length : 45).Trim();
            // insert hyphens
            s = Regex.Replace(s, @"\s", "-");
            // half space
            s = Regex.Replace(s, @"", "-");

            return s.ToLower();
        }

        private static string RemoveDiacritics(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            var normalizedString = text.Normalize(NormalizationForm.FormKC);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

    }
}