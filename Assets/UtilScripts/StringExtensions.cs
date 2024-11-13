using System;

namespace UtilScripts
{
    public static class StringExtensions
    {
        public static string ReplaceFirst(this string source, string find, string replace)
        {
            var index = source.IndexOf(find, StringComparison.Ordinal);
            return index < 0 ? source : source[..index] + replace + source[(index + find.Length)..];
        }
    }
}