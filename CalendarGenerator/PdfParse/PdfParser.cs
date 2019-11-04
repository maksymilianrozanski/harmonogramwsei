using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

[assembly: InternalsVisibleTo("CalGeneratorTests"), InternalsVisibleTo("ConsoleCalGenerator")]

namespace CalendarGenerator.PdfParse
{
    internal class PdfParser
    {
        internal static string[] RawTextToWords(string input)
        {
            var removedBreakLines = input.Replace("\n", " ");
            return removedBreakLines.Split(" ");
        }

        internal static List<string> WordsToDayItems(string[] words)
        {
            var stringBuilder = new StringBuilder();
            var dayItems = new List<string>();
            const string dayPattern = "Data";
            var afterTableHeader = false;

            foreach (var word in words)
            {
                var dayMatcher = Regex.Match(word, dayPattern);
                if (dayMatcher.Success)
                {
                    afterTableHeader = true;
                    if (stringBuilder.Length != 0) dayItems.Add(stringBuilder.ToString().Trim());
                    stringBuilder.Clear();
                    stringBuilder.Append(word + " ");
                }
                else if (afterTableHeader)
                {
                    stringBuilder.Append(word + " ");
                }
            }

            dayItems.Add(stringBuilder.ToString().Trim());
            return dayItems;
        }
    }
}